<?php
require_once CONTROLLERS_PATH . 'Admin.controller.php';
class AdminTime extends Admin
{
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
				
				Object::import('Model', array('Date', 'Calendar'));
				$DateModel = new DateModel();
				$CalendarModel = new CalendarModel();
				
				$DateModel->addJoin($DateModel->joins, $CalendarModel->getTable(), 'TC', array('TC.id' => 't1.calendar_id'), array('TC.user_id'));
				$arr = $DateModel->get($id);
				if (count($arr) == 0)
				{
					if ($this->isXHR())
					{
						$_GET['err'] = 8;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminTime&action=index&err=8&tab_id=tabs-2");
					}
				} elseif ($this->isOwner() && $arr['user_id'] != $this->getUserId()) {
					if ($this->isXHR())
					{
						$_GET['err'] = 9;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminTime&action=index&err=9&tab_id=tabs-2");
					}
				}
				
				if ($DateModel->delete($id))
				{					
					if ($this->isXHR())
					{
						$_GET['err'] = 3;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminTime&action=index&err=3&tab_id=tabs-2");
					}
				} else {
					if ($this->isXHR())
					{
						$_GET['err'] = 4;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminTime&action=index&err=4&tab_id=tabs-2");
					}
				}
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}

	function index()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				Object::import('Model', array('WorkingTime', 'Date'));
				$WorkingTimeModel = new WorkingTimeModel();
				$DateModel = new DateModel();
				
				if (isset($_POST['working_time']))
				{
					if ($this->isDemo())
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminTime&action=index&err=7");
					}
					
					$arr = $WorkingTimeModel->get($this->getCalendarId());
					
					$data = array();
					$data['calendar_id'] = $this->getCalendarId();

					$weekDays = array('monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday', 'sunday');
					foreach ($weekDays as $day)
					{
						if (!isset($_POST[$day . '_dayoff']))
						{
							$data[$day . '_from'] = $_POST[$day . '_hour_from'] . ":" . $_POST[$day . '_minute_from'];
							$data[$day . '_to'] = $_POST[$day . '_hour_to'] . ":" . $_POST[$day . '_minute_to'];
						}
					}
					
					if (count($arr) > 0)
					{
						$WorkingTimeModel->delete($this->getCalendarId());
					}
					$WorkingTimeModel->save(array_merge($_POST, $data));
					
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminTime&action=index&err=4");
				}
				
				if (isset($_POST['custom_time']))
				{
					$date = Util::formatDate($_POST['date'], $this->option_arr['date_format']);
					$DateModel->delete(array('calendar_id' => $this->getCalendarid(), '`date`' => $date));
					$data = array();
					$data['calendar_id'] = $this->getCalendarId();
					$data['start_time'] = join(":", array($_POST['start_hour'], $_POST['start_minute']));
					$data['end_time'] = join(":", array($_POST['end_hour'], $_POST['end_minute']));
					$data['date'] = $date;
					
					$DateModel->save(array_merge($_POST, $data));
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminTime&action=index&err=5&tab_id=tabs-2");
				}
				
				$arr = $WorkingTimeModel->get($this->getCalendarId());
				$this->tpl['arr'] = $arr;
				
				$opts = array();
				$opts['t1.calendar_id'] = $this->getCalendarId();
				
				$page = isset($_GET['page']) && (int) $_GET['page'] > 0 ? intval($_GET['page']) : 1;
				$count = $DateModel->getCount($opts);
				$row_count = 10;
				$pages = ceil($count / $row_count);
				$offset = ((int) $page - 1) * $row_count;
				
				$this->tpl['date_arr'] = $DateModel->getAll(array_merge($opts, compact('offset', 'row_count'), array('col_name' => 't1.date', 'direction' => 'desc')));
				$this->tpl['paginator'] = array('pages' => $pages, 'row_count' => $row_count, 'count' => $count);
				
				# Dialog
				$this->js[] = array('file' => 'jquery.ui.button.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.position.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.dialog.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				
				$this->css[] = array('file' => 'jquery.ui.button.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				$this->css[] = array('file' => 'jquery.ui.dialog.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
				# Datepicker
				$this->js[] = array('file' => 'jquery.ui.datepicker.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->css[] = array('file' => 'jquery.ui.datepicker.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminTime.js', 'path' => JS_PATH);
			}
		}
	}
	
	function update($id)
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				Object::import('Model', array('Date'));
				$DateModel = new DateModel();
				
				$arr = $DateModel->get($id);
				
				if (count($arr) == 0)
				{
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminTime&action=index&err=8&tab_id=tabs-2");
				}
				$this->tpl['arr'] = $arr;
								
				# Datepicker
				$this->js[] = array('file' => 'jquery.ui.datepicker.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->css[] = array('file' => 'jquery.ui.datepicker.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminTime.js', 'path' => JS_PATH);
				
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
}
?>