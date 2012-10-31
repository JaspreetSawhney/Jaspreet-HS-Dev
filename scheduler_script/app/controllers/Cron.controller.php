<?php
require_once CONTROLLERS_PATH . 'AppController.controller.php';
class Cron extends AppController
{
	var $layout = 'empty';
	
	function index()
	{
		Object::import('Model', array('Calendar', 'Option', 'Booking', 'BookingDetail', 'Service', 'Country'));
		$CalendarModel = new CalendarModel();
		$OptionModel = new OptionModel();
		$BookingDetailModel = new BookingDetailModel();
		$BookingModel = new BookingModel();
		$ServiceModel = new ServiceModel();
		$CountryModel = new CountryModel();
		
		$country_arr = $CountryModel->getAll();
		
		Object::import('Component', 'Email');
		$Email = new Email();
		include_once("smslib.php");
		
		//$BookingDetailModel->debug=1;
		$calendar_arr = $CalendarModel->getAll();
		foreach ($calendar_arr as $calendar)
		{
			$option_arr = $OptionModel->getPairs($calendar['id']);
			
			# Emails
			if ($option_arr['reminder_enable'] == 'Yes')
			{
				$BookingModel->joins = array();	
				$BookingModel->subqueries = array();	

				$BookingDetailModel->joins = array();	
				$BookingDetailModel->subqueries = array();	
				
				$hours_email = (int) $option_arr['reminder_email_before'];
				$BookingDetailModel->addJoin($BookingDetailModel->joins, $BookingModel->getTable(), 'TB', array('TB.id' => 't1.booking_id', 'TB.calendar_id' => $calendar['id'], 'TB.booking_status' => "'confirmed'"), 
					array('TB.calendar_id', 'TB.customer_name', 'TB.customer_email', 'TB.customer_phone', 'TB.customer_country', 'TB.customer_city', 'TB.customer_state', 'TB.customer_zip', 'TB.customer_address', 'TB.customer_notes', 'TB.cc_type', 'TB.cc_num', 'TB.cc_exp', 'TB.cc_code', 'TB.payment_method', 'TB.payment_option', 'TB.booking_deposit', 'TB.booking_tax', 'TB.booking_total', 'TB.created'), 'inner');
				$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
				$booking_arr = $BookingDetailModel->getAll(array(
					't1.id > 0 AND (UNIX_TIMESTAMP()' => array(sprintf("t1.start_ts AND (t1.start_ts + %1\$u))", $hours_email * 3600), 'BETWEEN', 'null'), 
					't1.reminder_email' => array(1, '<', 'int')));
				foreach ($booking_arr as $booking)
				{
					$country = NULL;
					if (!empty($booking['customer_country']))
					{
						foreach ($country_arr as $item)
						{
							if ($booking['customer_country'] == $item['id'])
							{
								$country = $item['country_title'];
								break;
							}						
						}
					}
			
					$booking_data = stripslashes($booking['service_name']) . ": ".
					date($option_arr['date_format'], strtotime($booking['date'])). ", ".
					date($option_arr['time_format'], $booking['start_ts']). " - ".
					date($option_arr['time_format'], $booking['start_ts'] + $booking['total'] * 60);
			
					$cancelURL = INSTALL_URL . 'index.php?controller=Front&action=cancel&cid='.$booking['calendar_id'].'&id='.$booking['booking_id'].'&hash='.sha1($booking['booking_id'].$booking['created'].$this->salt);
					$search = array('{Name}', '{Email}', '{Phone}', '{Country}', '{City}', '{State}', '{Zip}', '{Address}', '{Notes}', '{CCType}', '{CCNum}', '{CCExp}', '{CCSec}', '{PaymentMethod}', '{PaymentOption}', '{Deposit}', '{Total}', '{Tax}', '{BookingID}', '{Services}', '{CancelURL}');
					$replace = array($booking['customer_name'], $booking['customer_email'], $booking['customer_phone'], $country, $booking['customer_city'], $booking['customer_state'], $booking['customer_zip'], $booking['customer_address'], $booking['customer_notes'], $booking['cc_type'], $booking['cc_num'], ($booking['payment_method'] == 'creditcard' ? $booking['cc_exp'] : NULL), $booking['cc_code'], $booking['payment_method'], $booking['payment_option'], $booking['booking_deposit'] . " " . $option_arr['currency'], $booking['booking_total'] . " " . $option_arr['currency'], $booking['booking_tax'] . " " . $option_arr['currency'], $booking['booking_id'], $booking_data, $cancelURL);
					$message = str_replace($search, $replace, $option_arr['reminder_body']);
					
					$Email->send($booking['customer_email'], $option_arr['reminder_subject'], $message, $option_arr['email_address']);
					
					$BookingDetailModel->update(array('reminder_email' => 1, 'id' => $booking['id']));
				}
				$BookingDetailModel->joins = array();
			}
			# SMS
			if (!empty($option_arr['reminder_sms_api']))
			{
				$hours_sms = (int) $option_arr['reminder_sms_hours'];
				$BookingModel->joins = array();	
				$BookingModel->subqueries = array();	

				$BookingDetailModel->joins = array();	
				$BookingDetailModel->subqueries = array();	

				$BookingDetailModel->addJoin($BookingDetailModel->joins, $BookingModel->getTable(), 'TB', array('TB.id' => 't1.booking_id', 'TB.calendar_id' => $calendar['id'], 'TB.booking_status' => "'confirmed'"), 
					array('TB.calendar_id', 'TB.customer_name', 'TB.customer_email', 'TB.customer_phone', 'TB.customer_country', 'TB.customer_city', 'TB.customer_state', 'TB.customer_zip', 'TB.customer_address', 'TB.customer_notes', 'TB.cc_type', 'TB.cc_num', 'TB.cc_exp', 'TB.cc_code', 'TB.payment_method', 'TB.payment_option', 'TB.booking_deposit', 'TB.booking_tax', 'TB.booking_total', 'TB.created'), 'inner');
				$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
				$booking_arr = $BookingDetailModel->getAll(array(
					't1.id > 0 AND (UNIX_TIMESTAMP()' => array(sprintf("t1.start_ts AND (t1.start_ts + %1\$u))", $hours_sms * 3600), 'BETWEEN', 'null'), 
					't1.reminder_sms' => array(1, '<', 'int')));
				foreach ($booking_arr as $booking)
				{
					if (empty($booking['customer_phone']))
					{
						continue;
					}
					$country = NULL;
					if (!empty($booking['customer_country']))
					{
						foreach ($country_arr as $item)
						{
							if ($booking['customer_country'] == $item['id'])
							{
								$country = $item['country_title'];
								break;
							}						
						}
					}
			
					$booking_data = stripslashes($booking['service_name']) . ": ".
					date($option_arr['date_format'], strtotime($booking['date'])). ", ".
					date($option_arr['time_format'], $booking['start_ts']). " - ".
					date($option_arr['time_format'], $booking['start_ts'] + $booking['total'] * 60);
			
					$cancelURL = INSTALL_URL . 'index.php?controller=Front&action=cancel&cid='.$booking['calendar_id'].'&id='.$booking['booking_id'].'&hash='.sha1($booking['booking_id'].$booking['created'].$this->salt);
					$search = array('{Name}', '{Email}', '{Phone}', '{Country}', '{City}', '{State}', '{Zip}', '{Address}', '{Notes}', '{CCType}', '{CCNum}', '{CCExp}', '{CCSec}', '{PaymentMethod}', '{PaymentOption}', '{Deposit}', '{Total}', '{Tax}', '{BookingID}', '{Services}', '{CancelURL}');
					$replace = array($booking['customer_name'], $booking['customer_email'], $booking['customer_phone'], $country, $booking['customer_city'], $booking['customer_state'], $booking['customer_zip'], $booking['customer_address'], $booking['customer_notes'], $booking['cc_type'], $booking['cc_num'], ($booking['payment_method'] == 'creditcard' ? $booking['cc_exp'] : NULL), $booking['cc_code'], $booking['payment_method'], $booking['payment_option'], $booking['booking_deposit'] . " " . $option_arr['currency'], $booking['booking_total'] . " " . $option_arr['currency'], $booking['booking_tax'] . " " . $option_arr['currency'], $booking['booking_id'], $booking_data, $cancelURL);
					$message = str_replace($search, $replace, $option_arr['reminder_sms_message']);
					
					$SMSLIB["phone"] = $booking['customer_phone'];
					$SMSLIB["key"] = $option_arr['reminder_sms_api'];
					sendSMS($message, $SMSLIB["phone"]); 
					
					$BookingDetailModel->update(array('reminder_sms' => 1, 'id' => $booking['id']));
				}
			}
		}
		exit;
	}
}
?>