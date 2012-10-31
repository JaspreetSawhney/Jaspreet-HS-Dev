<?php
require_once CONTROLLERS_PATH . 'Admin.controller.php';
/**
 * AdminBookings controller
 *
 * @package ebc
 * @subpackage ebc.app.controllers
 */
class AdminBookings extends Admin
{
/**
 * Create booking
 *
 * @access public
 * @return void
 */
	function create()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				$err = NULL;
				if (isset($_POST['booking_create']))
				{
					if ($this->isDemo())
					{
						$err = 7;
					} else {
					
						Object::import('Model', array('Booking', 'Price'));
						$BookingModel = new BookingModel();
						$PriceModel = new PriceModel();
						
						$p = $PriceModel->calcPrice($_POST);
						
						$people = 0;
						foreach ($p['arr'] as $v)
						{
							$people += $v['cnt'];
						}
			
						$data = array();
						$data['calendar_id'] = $this->getCalendarId();
						$data['customer_people'] = $people;
						if ($_POST['payment_method'] == 'creditcard')
						{
							$data['cc_exp'] = $_POST['cc_exp_year'] . '-' . $_POST['cc_exp_month'];
						}
						
						$insert_id = $BookingModel->save(array_merge($_POST, $data));
						if ($insert_id !== false && (int) $insert_id > 0)
						{
							Object::import('Model', 'BookingDetail');
							$BookingDetailModel = new BookingDetailModel();
							$details = array();
							$details['booking_id'] = $insert_id;
							foreach ($p['arr'] as $v)
							{
								if ((int) $v['cnt'] > 0)
								{
									$details['price_id'] = $v['id'];
									$details['price'] = $v['price'];
									$details['cnt'] = $v['cnt'];
									$BookingDetailModel->save($details);
								}
							}
	            			$err = 1;
						} else {
							$err = 2;
						}
					}
				}
				
				Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=index&err=$err");
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
	
	function deleteBookedService()
	{
		$this->isAjax = true;
	
		if ($this->isXHR())
		{
			Object::import('Model', 'BookingDetail');
			$BookingDetailModel = new BookingDetailModel();
			$BookingDetailModel->delete($_POST['id']);
		}
	}
	
	function getBookedServices()
	{
		$this->isAjax = true;
	
		if ($this->isXHR())
		{
			Object::import('Model', array('BookingDetail', 'Service', 'Employee'));
			$BookingDetailModel = new BookingDetailModel();
			$ServiceModel = new ServiceModel();
			$EmployeeModel = new EmployeeModel();
			
			$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
			$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name'));
			$this->tpl['booking_detail_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $_GET['booking_id'], 'col_name' => 't1.price', 'direction' => 'asc'));
		}
	}
/**
 * Delete booking, support AJAX too
 *
 * @access public
 * @return void
 */
	function delete()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				if ($this->isDemo())
				{
					$_GET['err'] = 7;
					$this->index();
					return;
				}
				
				if ($this->isXHR())
				{
					$this->isAjax = true;
					$id = $_POST['id'];
				} else {
					$id = $_GET['id'];
				}
				
				Object::import('Model', array('Booking', 'Calendar'));
				$BookingModel = new BookingModel();
				$CalendarModel = new CalendarModel();
				
				$BookingModel->addJoin($BookingModel->joins, $CalendarModel->getTable(), 'TC', array('TC.id' => 't1.calendar_id'), array('TC.user_id'));
				$arr = $BookingModel->get($id);
				if (count($arr) == 0)
				{
					if ($this->isXHR())
					{
						$_GET['err'] = 8;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=index&err=8");
					}
				} elseif ($this->isOwner() && $arr['user_id'] != $this->getUserId()) {
					if ($this->isXHR())
					{
						$_GET['err'] = 9;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=index&err=9");
					}
				}
				
				if ($BookingModel->delete($id))
				{
					Object::import('Model', 'BookingDetail');
					$BookingDetailModel = new BookingDetailModel();
					$BookingDetailModel->delete(array('booking_id' => $id));
					
					if ($this->isXHR())
					{
						$_GET['err'] = 3;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=index&err=3");
					}
				} else {
					if ($this->isXHR())
					{
						$_GET['err'] = 4;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=index&err=4");
					}
				}
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}

	function getEmployees()
	{
		$this->isAjax = true;
	
		if ($this->isXHR())
		{
			Object::import('Model', array('Employee', 'EmployeeService'));
			$EmployeeModel = new EmployeeModel();
			$EmployeeServiceModel = new EmployeeServiceModel();
			$linked = Object::getLinked($EmployeeServiceModel->getTable(), 'service_id', 'employee_id', $_GET['service_id']);
			
			$arr = array();
			if (count($linked) > 0)
			{
				$arr = $EmployeeModel->getAll(array('t1.id' => array("('".join("','", $linked)."')", 'IN', 'null'), 'col_name' => 't1.name', 'direction' => 'asc'));
			}			
			$this->tpl['employee_arr'] = $arr;
		}
	}
	
	function getServices()
	{
		$this->isAjax = true;
	
		if ($this->isXHR())
		{
			Object::import('Model', array('Service', 'EmployeeService'));
			$ServiceModel = new ServiceModel();
			$EmployeeServiceModel = new EmployeeServiceModel();
			$linked = Object::getLinked($EmployeeServiceModel->getTable(), 'employee_id', 'service_id', $_GET['employee_id']);
			
			$arr = array();
			if (count($linked) > 0)
			{
				$arr = $ServiceModel->getAll(array('t1.id' => array("('".join("','", $linked)."')", 'IN', 'null'), 'col_name' => 't1.name', 'direction' => 'asc'));
			}			
			$this->tpl['service_arr'] = $arr;
		}
	}
	
	function export()
	{
		$this->isAjax = true;
		
		Object::import('Model', array('Booking', 'BookingDetail', 'Country', 'Calendar', 'Service', 'Employee'));
		$BookingModel = new BookingModel();
		$BookingDetailModel = new BookingDetailModel();
		$CountryModel = new CountryModel();
		$CalendarModel = new CalendarModel();
		$ServiceModel = new ServiceModel();
		$EmployeeModel = new EmployeeModel();
		
		require_once APP_PATH . 'locale/' . $this->getLanguage() . '.php';
		
		$csv_header = array();
		$fields = array();
		foreach ($BookingDetailModel->schema as $column)
		{
			if (!in_array($column['name'], array('start_ts'))) {
				$csv_header[] = @$SBS_LANG['booking_export_map'][$column['name']];
			}
		}
		foreach ($BookingModel->schema as $column)
		{
			if (!in_array($column['name'], array('id', 'created')))
			{
				$fields[] = 'TB.' . $column['name'];
				$csv_header[] = @$SBS_LANG['booking_export_map'][$column['name']];
			}
		}
		$opts = array();
		if (isset($_POST['date_from']) && !empty($_POST['date_from']) && isset($_POST['date_to']) && !empty($_POST['date_to']))
		{
			$dateFrom = Util::formatDate($_POST['date_from'], $this->option_arr['date_format']);
			$dateTo = Util::formatDate($_POST['date_to'], $this->option_arr['date_format']);
			$opts['(t1.date'] = array("'".Object::escapeString($dateFrom)."' AND '".Object::escapeString($dateTo)."')", 'BETWEEN', 'null');
		} else {
			if (isset($_POST['date_to']) && !empty($_POST['date_to']))
			{
				$dateTo = Util::formatDate($_POST['date_to'], $this->option_arr['date_format']);
				$opts['t1.date'] = array("'".Object::escapeString($dateTo)."'", '<=', 'null');
			} elseif (isset($_POST['date_from']) && !empty($_POST['date_from'])) {
				$dateFrom = Util::formatDate($_POST['date_from'], $this->option_arr['date_format']);
				$opts['t1.date'] = array("'".Object::escapeString($dateFrom)."'", '>=', 'null');
			}
		}	

		$calendar_id = isset($_POST['calendar_id']) && (int) $_POST['calendar_id'] > 0 ? (int) $_POST['calendar_id'] : $this->getCalendarId();
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $BookingModel->getTable(), 'TB', array('TB.id' => 't1.booking_id', 'TB.calendar_id' => $calendar_id), $fields, 'inner');
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $CalendarModel->getTable(), 'TC', array('TC.id' => $calendar_id), array('TC.calendar_title'));
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $CountryModel->getTable(), 'TCO', array('TCO.id' => 'TB.customer_country'), array('TCO.country_title'));
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name'));
		$arr = $BookingDetailModel->getAll(array_merge($opts, array('col_name' => 't1.booking_id DESC, t1.date DESC, t1.start_ts', 'direction' => 'asc')));

		switch (strtolower($_POST['format']))
		{
			case 'csv':
				$mime_type = 'text/csv';
				$ext = '.csv';
				$row = array();
				$row[] = join(',', $csv_header);
				foreach ($arr as $booking)
				{
					$cell = array();
					foreach ($BookingDetailModel->schema as $column)
					{
						if ($column['name'] == 'date') 
						{
							$cell[] = date($this->option_arr['date_format'], strtotime($booking[$column['name']]));
						} elseif (in_array($column['name'], array('start'))) {
							$cell[] = date($this->option_arr['time_format'], strtotime($booking[$column['name']]));
						} elseif ($column['name'] == 'service_id') {
							$cell[] = $booking['service_name'];
						} elseif ($column['name'] == 'employee_id') {
							$cell[] = $booking['employee_name'];
						} elseif (!in_array($column['name'], array('start_ts'))) {
							$cell[] = $booking[$column['name']];
						}
					}
					foreach ($BookingModel->schema as $column)
					{
						if ($column['name'] == 'customer_country')
						{
							$cell[] = $booking['country_title'];
						} elseif (!in_array($column['name'], array('id', 'created'))) {
							$cell[] = $booking[$column['name']];
						}
					}
					$row[] = join(',', array_map('_clean', $cell));
				}
				$content = join("\n", $row);
				break;
			case 'xml':
				$mime_type = 'text/xml';
				$ext = '.xml';
				$row[] = '<bookings>';
				foreach ($arr as $booking)
				{
					$cell = array();
					$cell[] = "\t<booking>";
					foreach ($BookingDetailModel->schema as $column)
					{
						if ($column['name'] == 'date') 
						{
							$cell[] = "\t\t<". $column['name'] . ">" . htmlspecialchars(date($this->option_arr['date_format'], strtotime($booking[$column['name']]))) . "</" . $column['name'] . ">";
						} elseif (in_array($column['name'], array('start_time', 'end_time'))) {
							$cell[] = "\t\t<". $column['name'] . ">" . htmlspecialchars(date($this->option_arr['time_format'], strtotime($booking[$column['name']]))) . "</" . $column['name'] . ">";
						} elseif ($column['name'] == 'service_id') {
							$cell[] = "\t\t<". $column['name'] . ">" . htmlspecialchars($booking[$column['name']]) . "</" . $column['name'] . ">";
							$cell[] = "\t\t<service_name>" . htmlspecialchars($booking['service_name']) . "</service_name>";
						} elseif ($column['name'] == 'employee_id') {
							$cell[] = "\t\t<". $column['name'] . ">" . htmlspecialchars($booking[$column['name']]) . "</" . $column['name'] . ">";
							$cell[] = "\t\t<employee_name>" . htmlspecialchars($booking['employee_name']) . "</employee_name>";
						} elseif (!in_array($column['name'], array('start_ts', 'end_ts'))) {
							$cell[] = "\t\t<". $column['name'] . ">" . htmlspecialchars($booking[$column['name']]) . "</" . $column['name'] . ">";
						}
					}
					foreach ($BookingModel->schema as $column)
					{
						if ($column['name'] == 'customer_country')
						{
							$cell[] = "\t\t<" . $column['name'] . ">" . htmlspecialchars($booking['country_title']) . "</" . $column['name'] . ">";
						} elseif (!in_array($column['name'], array('id', 'created'))) {						
							$cell[] = "\t\t<" . $column['name'] . ">" . htmlspecialchars($booking[$column['name']]) . "</" . $column['name'] . ">";
						}
					}
					$cell[] = "\t</booking>";
					$row[] = join("\n", $cell);
				}
				$row[] = "</bookings>";
				$content = join("\n", $row);
				break;
			case 'icalendar':
				$mime_type = 'text/calendar';
				$ext = '.ics';
				
				$timezone = Util::getTimezone($this->option_arr['timezone']);
				
				$row[] = "BEGIN:VCALENDAR";
				$row[] = "VERSION:2.0";
				$row[] = "PRODID:-//Service Booking Script//NONSGML Foobar//EN";
				$row[] = "METHOD:UPDATE"; // requied by Outlook
				foreach ($arr as $booking)
				{
					$cell = array();
					$cell[] = "BEGIN:VEVENT";
					$cell[] = "UID:".md5($booking["id"]); // required by Outlok
					$cell[] = "SEQUENCE:1";
					$cell[] = "DTSTAMP;TZID=$timezone:".date('Ymd',$booking["start_ts"])."T000000Z"; // required by Outlook
					$cell[] = "DTSTART;TZID=$timezone:".date('Ymd',$booking["start_ts"])."T".date('His',$booking["start_ts"])."Z"; 
					$cell[] = "DTEND;TZID=$timezone:".date('Ymd', $booking["start_ts"])."T".date('His', $booking["start_ts"] + $booking['total']*60)."Z";
					$cell[] = "SUMMARY:Booking";
					$cell[] = "DESCRIPTION: Name: ".stripslashes($booking["customer_name"])."; Email: ".stripslashes($booking["customer_email"])."; Phone: ".stripslashes($booking["customer_phone"])."; Price: ".stripslashes($booking["booking_total"])."; Notes: ".stripslashes(preg_replace('/\n|\r|\r\n/', ' ', $booking["customer_notes"]))."; Status: ".stripslashes($booking["booking_status"]);
					$cell[] = "ATTENDEE;CN=\"".stripslashes($booking["customer_name"])."\";PARTSTAT=NEEDS-ACTION;ROLE=REQ-PARTICIPANT;RSVP=TRUE:MAILTO:".stripslashes($booking["customer_email"]);
					$cell[] = "END:VEVENT";
					$row[] = join("\n", $cell);
				}
				$row[] = "END:VCALENDAR";
				$content = join("\n", $row);
				break;
		}
		AppController::download($content, 'SBS_Bookings_' . time() . $ext, $mime_type);
	}

	function schedule()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				$this->__schedule();
					
				$this->js[] = array('file' => 'jquery.ui.datepicker.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->css[] = array('file' => 'jquery.ui.datepicker.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
					
				$this->js[] = array('file' => 'jquery.metadata.js', 'path' => LIBS_PATH . 'jquery/plugins/metadata/js/');
				$this->js[] = array('file' => 'adminBookings.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
	
	function __schedule()
	{
		Object::import('Model', array('Booking', 'BookingDetail', 'Service', 'Calendar', 'Employee'));
		$BookingModel = new BookingModel();
		$CalendarModel = new CalendarModel();
		$BookingDetailModel = new BookingDetailModel();
		$ServiceModel = new ServiceModel();
		$EmployeeModel = new EmployeeModel();

		$opts = array();
		if (isset($_GET['date']) && !empty($_GET['date']))
		{
			$opts['t1.date'] = Util::formatDate($_GET['date'], $this->option_arr['date_format']);
		} else {
			$opts['t1.date'] = date("Y-m-d");
		}
		
		if ($this->isOwner())
		{
			$opts['TC.user_id'] = $this->getUserId();
		}
		
		#$BookingDetailModel->debug = 1;
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $BookingModel->getTable(), 'TB', array('TB.id' => 't1.booking_id', 'TB.calendar_id' => $this->getCalendarId()), array('TB.customer_name', 'TB.booking_status'), 'inner');
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $CalendarModel->getTable(), 'TC', array('TC.id' => 'TB.calendar_id'), array('TC.calendar_title'), 'inner');
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name'));
		$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
		$_arr = $BookingDetailModel->getAll(array_merge($opts, array('col_name' => 't1.start_ts', 'direction' => 'asc')));
		
		$arr = array();
		foreach ($_arr as $k => $item)
		{
			$index = $item['employee_id'] .":^:". $item['employee_name'];
			if (!isset($arr[$index]))
			{
				$arr[$index] = array();
			}
			$arr[$index][] = $item;
		}
		$this->tpl['arr'] = $arr;
	}
	
	function getSchedule()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			$this->__schedule();
		}
	}
	
	function index()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				Object::import('Model', array('Booking', 'BookingDetail', 'Service', 'Calendar', 'Country', 'Employee'));
				$BookingModel = new BookingModel();
				$CalendarModel = new CalendarModel();
				$CountryModel = new CountryModel();
				$BookingDetailModel = new BookingDetailModel();
				$ServiceModel = new ServiceModel();
				$EmployeeModel = new EmployeeModel();

				$opts = array();
				if (isset($_GET['service_id']) && (int) $_GET['service_id'] > 0)
				{
					$opts['t1.id >= 1 AND 0'] = array(sprintf("(SELECT COUNT(*) FROM `%s` WHERE `booking_id` = `t1`.`id` AND `service_id` = '%u' LIMIT 1)", $BookingDetailModel->getTable(), (int) $_GET['service_id']), '<', 'null');
				}
				if (isset($_GET['employee_id']) && (int) $_GET['employee_id'] > 0)
				{
					$opts['t1.id + 1 >= 2 AND 0'] = array(sprintf("(SELECT COUNT(*) FROM `%s` WHERE `booking_id` = `t1`.`id` AND `employee_id` = '%u' LIMIT 1)", $BookingDetailModel->getTable(), (int) $_GET['employee_id']), '<', 'null');
				}
				if (isset($_GET['start_date']) && !empty($_GET['start_date']) && isset($_GET['end_date']) && !empty($_GET['end_date']))
				{
					$startDate = Util::formatDate($_GET['start_date'], $this->option_arr['date_format']);
					$endDate = Util::formatDate($_GET['end_date'], $this->option_arr['date_format']);
					$opts['t1.id > 0 AND 0'] = array("(SELECT COUNT(*) FROM `".$BookingDetailModel->getTable()."` WHERE `booking_id` = `t1`.`id` AND `date` <= '".$endDate."' AND `date` >= '".$startDate."' LIMIT 1)", '<', 'null');
				} else {
					if (isset($_GET['start_date']) && !empty($_GET['start_date']))
					{
						$startDate = Util::formatDate($_GET['start_date'], $this->option_arr['date_format']);
						$opts['t1.id > 0 AND 0'] = array("(SELECT COUNT(*) FROM `".$BookingDetailModel->getTable()."` WHERE `booking_id` = `t1`.`id` AND `date` <= '2999-01-01' AND `date` >= '".$startDate."' LIMIT 1)", '<', 'null');
					} elseif (isset($_GET['end_date']) && !empty($_GET['end_date'])) {
						$endDate = Util::formatDate($_GET['end_date'], $this->option_arr['date_format']);
						$opts['t1.id > 0 AND 0'] = array("(SELECT COUNT(*) FROM `".$BookingDetailModel->getTable()."` WHERE `booking_id` = `t1`.`id` AND `date` <= '".$endDate."' AND `date` >= '1971-01-01' LIMIT 1)", '<', 'null');
					}
				}
				if (isset($_GET['q']) && !empty($_GET['q']))
				{
					$q = Object::escapeString($_GET['q']);
					$opts['(t1.customer_name'] = array("'%$q%' OR t1.customer_email LIKE '%$q%' OR t1.customer_phone LIKE '%$q%' OR t1.customer_notes LIKE '%$q%')", 'LIKE', 'null');
				}
				
				$page = isset($_GET['page']) && (int) $_GET['page'] > 0 ? intval($_GET['page']) : 1;
				$count = $BookingModel->getCount($opts);
				$row_count = 10;
				$pages = ceil($count / $row_count);
				$offset = ((int) $page - 1) * $row_count;
				
				if ($this->isOwner())
				{
					$opts['TC.user_id'] = $this->getUserId();
				}
				
				#$BookingModel->debug = 1;
				$BookingModel->addJoin($BookingModel->joins, $CalendarModel->getTable(), 'TC', array('TC.id' => 't1.calendar_id'), array('TC.calendar_title'), 'inner');
				$arr = $BookingModel->getAll(array_merge($opts, array('offset' => $offset, 'row_count' => $row_count, 'col_name' => 't1.created', 'direction' => 'desc')));
				foreach ($arr as $k => $v)
				{
					$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name'));
					$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
					$arr[$k]['b_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $v['id']));
				}
				
				$this->tpl['arr'] = $arr;
				$this->tpl['paginator'] = array('pages' => $pages, 'row_count' => $row_count, 'count' => $count);
				$this->tpl['country_arr'] = $CountryModel->getAll(array('t1.status' => 'T', 'col_name' => 't1.country_title', 'direction' => 'asc'));
				$this->tpl['service_arr'] = $ServiceModel->getAll(array('t1.calendar_id' => $this->getCalendarId(), 'col_name' => 't1.name', 'direction' => 'asc'));
				$this->tpl['employee_arr'] = $EmployeeModel->getAll(array('t1.calendar_id' => $this->getCalendarId(), 'col_name' => 't1.name', 'direction' => 'asc'));
					
				$this->js[] = array('file' => 'jquery.ui.button.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.position.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.dialog.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				
				$this->css[] = array('file' => 'jquery.ui.button.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				$this->css[] = array('file' => 'jquery.ui.dialog.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
				$this->js[] = array('file' => 'jquery.ui.datepicker.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->css[] = array('file' => 'jquery.ui.datepicker.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
					
				$this->js[] = array('file' => 'index.php?controller=Front&action=loadJs', 'path' => INSTALL_URL, 'remote' => true);
				$this->css[] = array('file' => 'index.php?controller=Front&action=loadCss&cid='.$this->getCalendarId(), 'path' => INSTALL_URL, 'remote' => true);
				
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminBookings.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
	
	function reSend()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			Object::import('Model', 'Booking');
			$BookingModel = new BookingModel();
				
			if (isset($_POST['message']))
			{
				$booking_arr = $BookingModel->get($_POST['id']);
				
				Object::import('Component', 'Email');
				$Email = new Email();
				$Email->send($booking_arr['customer_email'], $this->option_arr['email_confirmation_subject'], $_POST['message'], $this->option_arr['email_address']);
				//send sms to customer
				sendSMS($_POST['message'], $booking['customer_phone']);
				
				//send sms to admin
				sendSMS($_POST['message'], $this->option_arr['admin_phone']);
				
				header("Content-type: text/json");
				echo '{"code":200}';
				exit;
			} else {
				$booking_arr = $BookingModel->get($_GET['id']);
				
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
				
				if (count($booking_arr) > 0)
				{
					Object::import('Model', array('BookingDetail', 'Service'));
					$BookingDetailModel = new BookingDetailModel();
					$ServiceModel = new ServiceModel();
					$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
					$booking_arr['details_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $booking_arr['id']));
				}
				
				$row = array();
				foreach ($booking_arr['details_arr'] as $v)
				{
					$cell = stripslashes($v['service_name']) . ": ".
					date($this->option_arr['date_format'], strtotime($v['date'])). ", ".
					date($this->option_arr['time_format'], $v['start_ts']). " - ".
					date($this->option_arr['time_format'], $v['start_ts'] + $v['total'] * 60);
					$row[] = $cell;
				}
				$booking_data = count($row) > 0 ? join("\n", $row) : NULL;
		
				$cancelURL = INSTALL_URL . 'index.php?controller=Front&action=cancel&cid='.$booking_arr['calendar_id'].'&id='.$booking_arr['id'].'&hash='.sha1($booking_arr['id'].$booking_arr['created'].$this->salt);
				$search = array('{Name}', '{Email}', '{Phone}', '{Country}', '{City}', '{State}', '{Zip}', '{Address}', '{Notes}', '{CCType}', '{CCNum}', '{CCExp}', '{CCSec}', '{PaymentMethod}', '{PaymentOption}', '{Deposit}', '{Total}', '{Tax}', '{BookingID}', '{Services}', '{CancelURL}');
				$replace = array($booking_arr['customer_name'], $booking_arr['customer_email'], $booking_arr['customer_phone'], $country, $booking_arr['customer_city'], $booking_arr['customer_state'], $booking_arr['customer_zip'], $booking_arr['customer_address'], $booking_arr['customer_notes'], $booking_arr['cc_type'], $booking_arr['cc_num'], ($booking_arr['payment_method'] == 'creditcard' ? $booking_arr['cc_exp'] : NULL), $booking_arr['cc_code'], $booking_arr['payment_method'], $booking_arr['payment_option'], $booking_arr['booking_deposit'] . " " . $this->option_arr['currency'], $booking_arr['booking_total'] . " " . $this->option_arr['currency'], $booking_arr['booking_tax'] . " " . $this->option_arr['currency'], $booking_arr['id'], $booking_data, $cancelURL);
				
				$message = str_replace($search, $replace, $this->option_arr['email_confirmation_message']);
				
				$this->tpl['message'] = $message;
			}
		}
	}
/**
 * Update booking
 *
 * @access public
 * @return void
 */
	function update()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin())
			{
				Object::import('Model', array('Booking', 'BookingDetail', 'Service', 'Employee'));
				$BookingModel = new BookingModel();
				$BookingDetailModel = new BookingDetailModel();
				$ServiceModel = new ServiceModel();
				$EmployeeModel = new EmployeeModel();
						
				if (isset($_POST['booking_update']))
				{
					if ($this->isDemo())
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=index&err=7");
					}
					
					$data = array();
					if ($_POST['payment_method'] == 'creditcard')
					{
						$data['cc_exp'] = $_POST['cc_exp_year'] . '-' . $_POST['cc_exp_month'];
					} else {
						$data['cc_type'] = array('NULL');
						$data['cc_exp'] = array('NULL');
						$data['cc_num'] = array('NULL');
						$data['cc_code'] = array('NULL');
					}
					
					$arr = $BookingModel->get($_POST['id']);
					
					$BookingModel->update(array_merge($_POST, $data));
					
					$details = array();
					$details['booking_id'] = $arr['id'];
					$service_arr = AppController::getCartPrices($arr['calendar_id'], $this->cartName);
					foreach ($_SESSION[$this->cartName] as $cid => $date_arr)
					{
						if ($cid != $arr['calendar_id'])
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
					$_SESSION[$this->cartName] = array();
					
					/*if (isset($_POST['booking']) && count($_POST['booking']) > 0)
					{
						$BookingDetailModel->delete(array('id' => array("('". join("','", array_map("intval", $_POST['booking'])) ."')", 'IN', 'null')));
					}*/
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=index&err=5");
					
				} else {
					$arr = $BookingModel->get($_GET['id']);
					
					if (count($arr) == 0)
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=index&err=8");
					}
					
					$this->tpl['arr'] = $arr;
					
					Object::import('Model', array('Country'));
					$CountryModel = new CountryModel();
					
					$this->tpl['country_arr'] = $CountryModel->getAll(array('t1.status' => 'T', 'col_name' => 't1.country_title', 'direction' => 'asc'));
					$BookingDetailModel->addJoin($BookingDetailModel->joins, $ServiceModel->getTable(), 'TS', array('TS.id' => 't1.service_id'), array('TS.name.service_name'));
					$BookingDetailModel->addJoin($BookingDetailModel->joins, $EmployeeModel->getTable(), 'TE', array('TE.id' => 't1.employee_id'), array('TE.name.employee_name'));
					$this->tpl['booking_detail_arr'] = $BookingDetailModel->getAll(array('t1.booking_id' => $arr['id'], 'col_name' => 't1.price', 'direction' => 'asc'));
				
					$this->js[] = array('file' => 'jquery.ui.button.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
					$this->js[] = array('file' => 'jquery.ui.position.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
					$this->js[] = array('file' => 'jquery.ui.dialog.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
					
					$this->css[] = array('file' => 'jquery.ui.button.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
					$this->css[] = array('file' => 'jquery.ui.dialog.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
					$this->js[] = array('file' => 'jquery.ui.datepicker.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
					$this->css[] = array('file' => 'jquery.ui.datepicker.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
					
					$this->js[] = array('file' => 'index.php?controller=Front&action=loadJs', 'path' => INSTALL_URL, 'remote' => true);
					$this->css[] = array('file' => 'index.php?controller=Front&action=loadCss&cid='.$this->getCalendarId(), 'path' => INSTALL_URL, 'remote' => true);
					
					$this->js[] = array('file' => 'jquery.blockUI.js', 'path' => LIBS_PATH . 'jquery/plugins/blockUI/');
					
					$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
					$this->js[] = array('file' => 'adminBookings.js', 'path' => JS_PATH);
				}
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
}
function _clean($val)
{
	$newLines = array("\r\n", "\n\r", "\n", "\r");
	
	return str_replace($newLines, " ", '"' . $val . '"');
}
?>