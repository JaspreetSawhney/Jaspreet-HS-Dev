<?php
require_once CONTROLLERS_PATH . 'AppController.controller.php';
/**
 * Front controller
 *
 * @package ebc
 * @subpackage ebc.app.controllers
 */
class Front extends AppController
{
/**
 * Define layout used by controller
 *
 * @access public
 * @var string
 */
	var $layout = 'front';
/**
 * Define name of session variable used by Captcha component
 *
 * @access public
 * @var string
 */
	var $default_captcha = 'SBScript_Captcha';
/**
 * Constructor
 */
	function Front()
	{
		ob_start();
	}
/**
 * (non-PHPdoc)
 * @see core/framework/Controller::beforeFilter()
 * @access public
 * @return void
 */
	function beforeFilter()
	{
		if (isset($_GET['cid']) && (int) $_GET['cid'] > 0)
		{
			Object::import('Model', 'Option');
			$OptionModel = new OptionModel();
			$this->option_arr = $OptionModel->getPairs($_GET['cid']);
			$this->tpl['option_arr'] = $this->option_arr;
			
			if (isset($this->tpl['option_arr']['timezone']))
			{
				$offset = $this->option_arr['timezone'] / 3600;
				if ($offset > 0)
				{
					$offset = "-".$offset;
				} elseif ($offset < 0) {
					$offset = "+".abs($offset);
				} elseif ($offset === 0) {
					$offset = "+0";
				}
			
				AppController::setTimezone('Etc/GMT'.$offset);
				if (strpos($offset, '-') !== false)
				{
					$offset = str_replace('-', '+', $offset);
				} elseif (strpos($offset, '+') !== false) {
					$offset = str_replace('+', '-', $offset);
				}
				AppController::setMySQLServerTime($offset . ":00");
			}
		}
	}
/**
 * (non-PHPdoc)
 * @see core/framework/Controller::beforeRender()
 * @access public
 */
	function beforeRender()
	{
		
	}
/**
 * Show booking form via AJAX
 *
 * @access public
 * @return void
 */
	function bookingForm()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			if (in_array($this->option_arr['bf_include_country'], array(2, 3)))
			{
				Object::import('Model', 'Country');
				$CountryModel = new CountryModel();
				$this->tpl['country_arr'] = $CountryModel->getAll(array('t1.status' => 'T', 'col_name' => 't1.country_title', 'direction' => 'asc'));
			}
			$this->tpl['amount'] = AppController::getCartTotal($_GET['cid'], $this->cartName, $this->option_arr);
		}
	}
/**
 * Show booking summary via AJAX
 *
 * @access public
 * @return void
 */
	function bookingSummary()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{			
			$this->tpl['amount'] = AppController::getCartTotal($_GET['cid'], $this->cartName, $this->option_arr);
			if (in_array($this->option_arr['bf_include_country'], array(2, 3)))
			{
				Object::import('Model', 'Country');
				$CountryModel = new CountryModel();
				$this->tpl['country_arr'] = $CountryModel->getAll(array('t1.status' => 'T', 'col_name' => 't1.country_title', 'direction' => 'asc'));
			}
		}
	}
/**
 * Save booking via AJAX
 *
 * @access public
 * @return json
 */
	function bookingSave()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			$opts = AppController::getCartTotal($_GET['cid'], $this->cartName, $this->option_arr);
			$data = array();
			if ($this->option_arr['payment_disable'] == 'Yes')
			{
				$data['booking_status'] = $this->option_arr['booking_status'];
			} else {
				$data['booking_status'] = $this->option_arr['booking_status']; //$this->option_arr['payment_status']
			}
			$data['booking_total']   = $opts['total'];
			$data['booking_deposit'] = $opts['deposit'];
			$data['booking_tax']     = $opts['tax'];
			if (isset($_POST['payment_method']) && $_POST['payment_method'] == 'creditcard')
			{
				$data['cc_exp'] = $_POST['cc_exp_year'] . '-' . $_POST['cc_exp_month'];
			}
			
			$payment = 'none';
			$paid = 'none';
			if (isset($_POST['payment_method']) && isset($_POST['payment_option']))
			{
				$payment = $_POST['payment_method'];
				$paid = $_POST['payment_option'];
			}
						
			Object::import('Model', array('Booking'));
			$BookingModel = new BookingModel();
			$insert_id = $BookingModel->save(array_merge($_POST, $data));
			if ($insert_id !== false && (int) $insert_id > 0)
			{
				Object::import('Model', array('BookingDetail', 'Service', 'Employee'));
				$BookingDetailModel = new BookingDetailModel();
				$ServiceModel = new ServiceModel();
				$EmployeeModel = new EmployeeModel();
				$details = array();
				$details['booking_id'] = $insert_id;
				$service_arr = AppController::getCartPrices($_GET['cid'], $this->cartName);
				foreach ($_SESSION[$this->cartName] as $cid => $date_arr)
				{
					if ($cid != $_GET['cid'])
					{
						continue;
					}
					foreach ($date_arr as $date => $services)
					{
						$details['date'] = Util::formatDate($date, $this->option_arr['date_format']);
						foreach ($services as $service_id => $time_arr)
						{
							$details['service_id'] = $service_id;
							$details['price'] = $service_arr[$service_id]['price'];
							$details['total'] = $service_arr[$service_id]['total'];
							foreach ($time_arr as $time => $employee_id)
							{
								list($details['start_ts'], $end_ts) = explode("|", $time);
								$details['start'] = date("H:i:s", $details['start_ts']);
								$details['employee_id'] = $employee_id;
								$BookingDetailModel->save($details);							
							}
						}
					}
				}
				
				$booking_arr = $BookingModel->get($insert_id);
				if (count($booking_arr) > 0)
				{
					$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
					$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name', 'TE.send_email', 'TE.email.employee_email'));
					$booking_arr['details_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $booking_arr['id']));
				}
				Front::confirmSend($this->option_arr, $booking_arr, $this->salt, 2);
				
				$_SESSION[$this->cartName] = array();
				$json = "{'code':200,'text':'','booking_id': $insert_id, 'payment':'".@$_POST['payment_method']."'}";
			} else {
				$json = "{'code':100,'text':''}";
			}
			header("Content-type: text/json");
			echo $json;
		}
	}
/**
 * Cancel booking
 *
 * @access public
 * @return void
 */
	function cancel()
	{
		$this->layout = 'empty';
		
		Object::import('Model', 'Booking');
		$BookingModel = new BookingModel();
				
		if (isset($_POST['booking_cancel']))
		{
			$arr = $BookingModel->get($_POST['id']);
			if (count($arr) > 0)
			{
				$BookingModel->update(array('booking_status' => 'cancelled'), array("SHA1(CONCAT(`id`, `created`, '".$this->salt."'))" => array("'" . $_POST['hash'] . "'", '=', 'null'), 'limit' => 1));
				Util::redirect($_SERVER['PHP_SELF'] . '?controller=Front&action=cancel&err=200');
			}
		} else {
			if (isset($_GET['hash']) && isset($_GET['id']))
			{
				Object::import('Model', array('Country'));
				$CountryModel = new CountryModel();
				
				$BookingModel->addJoin($BookingModel->joins, $CountryModel->getTable(), 'TC', array('TC.id' => 't1.customer_country'), array('TC.country_title'));
				$arr = $BookingModel->get($_GET['id']);
				if (count($arr) == 0)
				{
					$this->tpl['status'] = 2;
				} else {
					if ($arr['booking_status'] == 'cancelled')
					{
						$this->tpl['status'] = 4;
					} else {
						$hash = sha1($arr['id'] . $arr['created'] . $this->salt);
						if ($_GET['hash'] != $hash)
						{
							$this->tpl['status'] = 3;
						} else {
							
							Object::import('Model', array('BookingDetail', 'Service', 'Employee'));
							$BookingDetailModel = new BookingDetailModel();
							$ServiceModel = new ServiceModel();
							$EmployeeModel = new EmployeeModel();
							$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
							$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name'));
							$arr['details_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $arr['id']));
							
							$this->tpl['arr'] = $arr;
						}
					}
				}
			} elseif (!isset($_GET['err'])) {
				$this->tpl['status'] = 1;
			}
			$this->css[] = array('file' => 'install.css', 'path' => CSS_PATH);
		}
	}
/**
 * Display captcha
 *
 * @param mixed $renew
 * @access public
 * @return binary
 */
	function captcha($renew=null)
	{
		$this->isAjax = true;
		
		Object::import('Component', 'Captcha');
		$Captcha = new Captcha(WEB_PATH . 'obj/Anorexia.ttf', $this->default_captcha, 6);
		$Captcha->setImage(IMG_PATH . 'button.png');
		$Captcha->init($renew);
	}
/**
 * Check captcha via AJAX
 *
 * @access public
 * @return json
 */
	function checkCaptcha()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			if (isset($_SESSION[$this->default_captcha]) && strtoupper($_GET['captcha']) == $_SESSION[$this->default_captcha])
			{
				$json = "{'code':200,'text':''}";
			} else {
				$json = "{'code':100,'text':''}";
			}
			header("Content-type: text/json");
			echo $json;
		}
	}
	
	function getService()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			Object::import('Model', array('Employee', 'EmployeeService', 'BookingDetail', 'WorkingTime', 'Service', 'Booking'));
			$ServiceModel = new ServiceModel();
			$EmployeeModel = new EmployeeModel();
			$EmployeeServiceModel = new EmployeeServiceModel();
			$BookingDetailModel = new BookingDetailModel();
			$BookingModel = new BookingModel();
			$WorkingTimeModel = new WorkingTimeModel();
			
			$_date_ = Util::formatDate($_GET['date'], $this->option_arr['date_format']);
			
			$EmployeeServiceModel->addJoin($EmployeeServiceModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name'));
			$arr = $EmployeeServiceModel->getAll(array('t1.service_id' => $_GET['service_id'], 'col_name' => 't1.service_id', 'direction' => 'asc'));
			
			$BookingDetailModel->addJoin($BookingDetailModel->joins, $BookingModel->getTable(), 'TB', array('TB.id' => 't1.booking_id', 'TB.booking_status' => "'confirmed'"), array('TB.booking_status'), 'inner');
			foreach ($arr as $k => $v)
			{
				$arr[$k]['booking_arr'] = $BookingDetailModel->getAll(array('t1.employee_id' => $v['employee_id'], 't1.date' => $_date_));
			}
			$this->tpl['arr'] = $arr;
			$this->tpl['service_arr'] = $ServiceModel->get($_GET['service_id']);
			$s_arr = $ServiceModel->getAll(array('t1.calendar_id' => $_GET['cid'], 'col_name' => 't1.name', 'direction' => 'asc'));
			$services_arr = array();
			foreach ($s_arr as $item)
			{
				$services_arr[$item['id']] = $item;
			}
			$this->tpl['services_arr'] = $services_arr;
			$this->tpl['t_arr'] = AppController::getRawSlots($_GET['cid'], $_date_, $this->option_arr);
		}
	}
	
	function getServices()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			$_date_ = Util::formatDate($_GET['date'], $this->option_arr['date_format']);
			
			list($y, $n, $j) = explode("-", date("Y-n-j"));
			if (strtotime($_date_) < mktime(0, 0, 0, $n, $j, $y))
			{
				$this->tpl['isPastDate'] = true;
				return;
			}
			Object::import('Model', array('Service', 'BookingDetail', 'Booking', 'EmployeeService'));
			$ServiceModel = new ServiceModel();
			$BookingDetailModel = new BookingDetailModel();
			$BookingModel = new BookingModel();
			$EmployeeServiceModel = new EmployeeServiceModel();
			
			$t_arr = AppController::getRawSlots($_GET['cid'], $_date_, $this->option_arr);
			if ($t_arr === false)
			{
				# It's Day off
				$this->tpl['dayoff'] = true;
				return;
			}
			
			$opts = array();
			
			$stack = array();
			$step = $this->option_arr['step'] * 60;
			$service_arr = $ServiceModel->getAll(array('t1.calendar_id' => $_GET['cid']));
			foreach ($service_arr as $service)
			{
				$service_length = $service['total'] * 60;
				$employee_arr = $EmployeeServiceModel->getAll(array('t1.service_id' => $service['id'], 'col_name' => 't1.service_id', 'direction' => 'asc'));
				foreach ($employee_arr as $k => $v)
				{
					$employee_arr[$k]['booking_arr'] = $BookingDetailModel->getAll(array('t1.employee_id' => $v['employee_id'], 't1.service_id' => $v['service_id'], 't1.date' => $_date_));
				}
				foreach ($employee_arr as $v)
				{
					foreach (range($t_arr['start_ts'], $t_arr['end_ts'] - $step, $step) as $i)
					{
						$is_free = true;
						$class = "t-available";
						foreach ($v['booking_arr'] as $item)
						{
							if ($i >= $item['start_ts'] && $i < $item['start_ts'] + $item['total'] * 60)
							{
								$is_free = false;
								$class = "t-booked";
								break;
							}
						}
						if ($is_free)
						{
							foreach ($v['booking_arr'] as $item)
							{
								if ($i + $service_length > $item['start_ts'] && $i + $service_length < $item['start_ts'] + $item['total'] * 60)
								{
									$class = "t-unavailable";
									break;
								}
							}
							if ($i + $service_length > $t_arr['end_ts'])
							{
								$class = "t-unavailable";
							}
						}
						$stack[$service['id']][$v['employee_id']][] = $class;
					}
				}
			}
			$s_arr = array();			
			foreach ($stack as $service_id => $e_arr)
			{
				foreach ($e_arr as $employee_id => $availability_arr)
				{
					if (in_array('t-available', $availability_arr))
					{
						$s_arr[] = $service_id;
						break;
					}
				}
			}
			
			if (count($s_arr) > 0)
			{
				$opts['t1.id'] = array("('".join("','", $s_arr)."')", 'IN', 'null');
			} else {
				$opts['t1.id'] = -999;
			}
			
			$opts['t1.calendar_id'] = $_GET['cid'];
			$this->tpl['service_arr'] = $ServiceModel->getAll(array_merge($opts, array('col_name' => 't1.name', 'direction' => 'asc')));
		}
	}
/**
 * (non-PHPdoc)
 * @see core/framework/Controller::index()
 */
	function index()
	{
		
	}
/**
 * Init calendar
 *
 * @access public
 * @return void
 */
	function load()
	{
	
	}
/**
 * Load payment form
 *
 * @access public
 * @return void
 */
	function paymentForm()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			Object::import('Model', array('Booking', 'BookingDetail', 'Service'));
			$BookingModel = new BookingModel();
			$BookingDetailModel = new BookingDetailModel();
			$ServiceModel = new ServiceModel();
			
			$arr = $BookingModel->get($_POST['id']);
			
			$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
			$arr['detail_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $_POST['id']));
			$s_arr = array();
			foreach ($arr['detail_arr'] as $item)
			{
				$s_arr[] = $item['service_name'];
			}
			$arr['detail_str'] = "";
			if (count($s_arr) > 0)
			{
				$arr['detail_str'] = join(", ", array_map("stripslashes", $s_arr));
			}
			$this->tpl['arr'] = $arr;
		}
	}
/**
 * Authorize.NET confirmation: Send email and redirect to "Thank you" page
 *
 * @access public
 * @return void
 */
	function confirmAuthorize()
	{
		$this->isAjax = true;
		
		Object::import('Model', array('Booking', 'Option', 'Employee', 'Service', 'BookingDetail'));
		$BookingModel = new BookingModel();
		$OptionModel = new OptionModel();
		$ServiceModel = new ServiceModel();
		$EmployeeModel = new EmployeeModel();
		$BookingDetailModel = new BookingDetailModel();
		
		$option_arr = $OptionModel->getPairs($_POST['x_custom_calendar_id']);
		$booking_arr = $BookingModel->get($_POST['x_invoice_num']);
		if (count($booking_arr) > 0)
		{
			$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
			$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name', 'TE.send_email', 'TE.email.employee_email'));
			$booking_arr['details_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $booking_arr['id']));
		}
		if (count($booking_arr) == 0)
		{
			Util::redirect($option_arr['thank_you_page']);
		}
		
		if (intval($_POST['x_response_code']) == 1)
		{
			$BookingModel->update(array('id' => $_POST['x_invoice_num'], 'booking_status' => $option_arr['payment_status']));
			Front::confirmSend($option_arr, $booking_arr, $this->salt, 3);
		}
		Util::redirect($option_arr['thank_you_page']);
	}
/**
 * PayPal confirmation: Send email and redirect to "Thank you" page
 * Use as IPN too
 *
 * @access public
 * @return void
 */
	function confirmPaypal()
	{
		$this->isAjax = true;
		
		$url = TEST_MODE ? 'sandbox.paypal.com' : 'www.paypal.com';
		$log = '';
		Front::log("\nPayPal - " . date("Y-m-d"));
		
		Object::import('Model', array('Booking', 'Option'));
		$BookingModel = new BookingModel();
		$OptionModel = new OptionModel();
		
		$option_arr = $OptionModel->getPairs($_POST['custom']);
		$booking_arr = $BookingModel->get($_POST['invoice']);
		if (count($booking_arr) == 0)
		{
			Front::log("No such booking");
			Util::redirect($option_arr['thank_you_page']);
		}
		
		$req = 'cmd=_notify-validate';
		foreach ($_POST as $key => $value)
		{
			$value = urlencode(stripslashes($value));
			$req .= "&$key=$value";
		}
		
		// post back to PayPal system to validate
		$header  = "POST /cgi-bin/webscr HTTP/1.0\r\n";
		$header .= "Content-Type: application/x-www-form-urlencoded\r\n";
		$header .= "Content-Length: " . strlen($req) . "\r\n\r\n";
		$fp = fsockopen($url, 80, $errno, $errstr, 30);
		
		// assign posted variables to local variables
		$invoice = $_POST['invoice'];
		$payment_status = $_POST['payment_status'];
		$payment_amount = $_POST['mc_gross'];
		$payment_currency = $_POST['mc_currency'];
		$txn_id = $_POST['txn_id'];
		$receiver_email = $_POST['receiver_email'];

		if (!$fp)
		{
			// HTTP ERROR
			Front::log("HTTP error");
		} else {
			fputs ($fp, $header . $req);
			while (!feof($fp))
			{
				$res = fgets ($fp, 1024);
				if (strcmp ($res, "VERIFIED") == 0)
				{
					Front::log("VERIFIED");
					if ($payment_status == "Completed")
					{
						Front::log("Completed");
						if ($booking_arr['txn_id'] != $txn_id)
						{
							Front::log("TXN_ID is OK");
							if ($receiver_email == $option_arr['paypal_address'])
							{
								Front::log("EMAIL address is OK");
								$booking_amount = $booking_arr['payment_option'] == 'deposit' ? $booking_arr['booking_deposit'] : $booking_arr['booking_total'];
								if ($payment_amount == $booking_amount && $payment_currency == $option_arr['currency'])
								{
									Front::log("AMOUNT is OK, proceed with booking update");
									$BookingModel->update(array('id' => $invoice, 'booking_status' => $option_arr['payment_status'], 'txn_id' => $txn_id, 'processed_on' => array('NOW()')));
									
									if (count($booking_arr) > 0)
									{
										Object::import('Model', array('BookingDetail', 'Employee', 'Service'));
										$BookingDetailModel = new BookingDetailModel();
										$EmployeeModel = new EmployeeModel();
										$ServiceModel = new ServiceModel();
										$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
										$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name', 'TE.send_email', 'TE.email.employee_email'));
										$booking_arr['details_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $booking_arr['id']));
									}									
									Front::confirmSend($option_arr, $booking_arr, $this->salt, 3);
									Util::redirect($option_arr['thank_you_page']);
								}
							}
						}
					}
					Util::redirect($option_arr['thank_you_page']);
			    } elseif (strcmp ($res, "INVALID") == 0) {
			    	Front::log("INVALID");
			  		Util::redirect($option_arr['thank_you_page']);
			  	}
			}
			fclose($fp);
		}
	}
/**
 * Write given $content to file
 *
 * @param string $content
 * @param string $filename If omitted use 'payment.log'
 * @access public
 * @return void
 * @static
 */
	function log($content, $filename=null)
	{
		if (TEST_MODE)
		{
			$filename = is_null($filename) ? 'payment.log' : $filename;
			@file_put_contents($filename, $content . "\n", FILE_APPEND|FILE_TEXT);
		}
	}
/**
 * Send email
 *
 * @param array $option_arr
 * @param array $booking_arr
 * @param string $salt
 * @param int $opt
 * @access public
 * @return void
 * @static
 */
	function confirmSend($option_arr, $booking_arr, $salt, $opt)
	{
		if (!in_array((int) $opt, array(2, 3)))
		{
			return false;
		}
		Object::import('Component', 'Email');
		$Email = new Email();

		$country = NULL;
		if (!empty($booking_arr['customer_country']))
		{
			Object::import('Model', 'Country');
			$CountryModel = new CountryModel();
			$country_arr = $CountryModel->get($booking_arr['customer_country']);
			if (count($country_arr) > 0)
			{
				$country = $country_arr['country_title'];
			}
		}
		
		$row = array();
		foreach ($booking_arr['details_arr'] as $v)
		{
			$cell = stripslashes($v['service_name']) . ": ".
			date($option_arr['date_format'], strtotime($v['date'])). ", ".
			date($option_arr['time_format'], $v['start_ts']). " - ".
			date($option_arr['time_format'], $v['start_ts'] + $v['total'] * 60);
			$row[] = $cell;
		}
		$booking_data = count($row) > 0 ? join("\n", $row) : NULL;
		
		$cancelURL = INSTALL_URL . 'index.php?controller=Front&action=cancel&cid='.$booking_arr['calendar_id'].'&id='.$booking_arr['id'].'&hash='.sha1($booking_arr['id'].$booking_arr['created'].$salt);
		$search = array('{Name}', '{Email}', '{Phone}', '{Country}', '{City}', '{State}', '{Zip}', '{Address}', '{Notes}', '{CCType}', '{CCNum}', '{CCExp}', '{CCSec}', '{PaymentMethod}', '{PaymentOption}', '{Deposit}', '{Total}', '{Tax}', '{BookingID}', '{Services}', '{CancelURL}');
		$replace = array($booking_arr['customer_name'], $booking_arr['customer_email'], $booking_arr['customer_phone'], $country, $booking_arr['customer_city'], $booking_arr['customer_state'], $booking_arr['customer_zip'], $booking_arr['customer_address'], $booking_arr['customer_notes'], $booking_arr['cc_type'], $booking_arr['cc_num'], ($booking_arr['payment_method'] == 'creditcard' ? $booking_arr['cc_exp'] : NULL), $booking_arr['cc_code'], $booking_arr['payment_method'], $booking_arr['payment_option'], $booking_arr['booking_deposit'] . " " . $option_arr['currency'], $booking_arr['booking_total'] . " " . $option_arr['currency'], $booking_arr['booking_tax'] . " " . $option_arr['currency'], $booking_arr['id'], $booking_data, $cancelURL);
				
		# Payment email
		if ($option_arr['email_payment'] == $opt)
		{
			$message = str_replace($search, $replace, $option_arr['email_payment_message']);
			# Send to ADMIN
			$Email->send($option_arr['email_address'], $option_arr['email_payment_subject'], $message, $option_arr['email_address']);
			//send sms to admin
			sendSMS($message, $this->option_arr['admin_phone']);
			
			# Send to CLIENT
			$Email->send($booking_arr['customer_email'], $option_arr['email_payment_subject'], $message, $option_arr['email_address']);
			//send sms to customer
			sendSMS($message, $booking['customer_phone']);				
				
		}
		
		# Confirmation email
		if ($option_arr['email_confirmation'] == $opt)
		{
			$message = str_replace($search, $replace, $option_arr['email_confirmation_message']);
			# Send to ADMIN
			$Email->send($option_arr['email_address'], $option_arr['email_confirmation_subject'], $message, $option_arr['email_address']);
			//send sms to admin
			sendSMS($message, $this->option_arr['admin_phone']);
			# Send to CLIENT
			$Email->send($booking_arr['customer_email'], $option_arr['email_confirmation_subject'], $message, $option_arr['email_address']);
			//send sms to customer
			sendSMS($message, $booking['customer_phone']);				
		}
		
		# Email to Employees
		$cache = array();
		foreach ($booking_arr['details_arr'] as $v)
		{
			if ($v['send_email'] == 1 && !in_array($v['employee_email'], $cache))
			{
				$cache[] = $v['employee_email'];
				$message = str_replace($search, $replace, $option_arr['email_confirmation_message']);
				$Email->send($v['employee_email'], $option_arr['email_confirmation_subject'], $message, $option_arr['email_address']);
				//send sms to customer
				sendSMS($message, $v['phone']);				
			}
		}
	}
	
	function loadCss()
	{
		$arr = array(
			array('file' => 'index.php?controller=Front&action=css&cid='.$_GET['cid'], 'path' => INSTALL_URL),
			array('file' => 'calendar.css', 'path' => LIBS_PATH . 'calendarJS/themes/light/'),
			array('file' => 'calendar.css', 'path' => CSS_PATH)
		);
		header("Content-type: text/css");
		foreach ($arr as $item)
		{
			echo str_replace(array('../img/'), array(IMG_PATH), @file_get_contents($item['path'] . $item['file'])) . "\n";
		}
		exit;
	}
	
	function loadJs()
	{
		$arr = array(
			array('file' => 'jabb-0.3.js', 'path' => JS_PATH),
			array('file' => 'calendar.min.js', 'path' => LIBS_PATH . 'calendarJS/'),
			array('file' => 'StivaApp.js', 'path' => JS_PATH)
		);
		header("Content-type: text/javascript");
		foreach ($arr as $item)
		{
			echo @file_get_contents($item['path'] . $item['file']) . "\n";
		}
		exit;
	}
}