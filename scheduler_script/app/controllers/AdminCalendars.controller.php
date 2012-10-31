<?php
require_once CONTROLLERS_PATH . 'Admin.controller.php';
/**
 * AdminCalendars controller
 *
 * @package ebc
 * @subpackage ebc.app.controllers
 */
class AdminCalendars extends Admin
{
/**
 * Create new calendar
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
				if (isset($_POST['calendar_create']))
				{
					if ($this->isDemo())
					{
						$err = 7;
					} else {
					
						Object::import('Model', 'Calendar');
						$CalendarModel = new CalendarModel();
	
						$data = array();
						if ($this->isOwner())
						{
							$data['user_id'] = $this->getUserId();
						}
						$insert_id = $CalendarModel->save(array_merge($_POST, $data));
						if ($insert_id !== false && (int) $insert_id > 0)
						{
							Object::import('Model', array('Option', 'WorkingTime'));
							$OptionModel = new OptionModel();
							$WorkingTimeModel = new WorkingTimeModel();				
							$WorkingTimeModel->initWorkingTime($insert_id);

							$arr = $OptionModel->getAll(array('group_by' => 't1.key', 'col_name' => 't1.key', 'direction' => 'asc'));
							foreach ($arr as $v)
							{
								//FIXME Optimize (bulk save)!
								$OptionModel->save(array_merge($v, array('calendar_id' => $insert_id)));
							}
							
							$_SESSION[$this->default_user]['calendar_id'] = $insert_id;
	            			$err = 1;
						} else {
							$err = 2;
						}
					}
				}
				
				Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=$err");
				
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
/**
 * Delete a calendar
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
				if (!$this->isMultiCalendar())
				{
					$this->tpl['status'] = 8;
					return;
				}
				
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
				
				Object::import('Model', 'Calendar');
				$CalendarModel = new CalendarModel();
					
				$arr = $CalendarModel->get($id);
				if (count($arr) == 0)
				{
					if ($this->isXHR())
					{
						$_GET['err'] = 8;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=8");
					}
				} elseif ($this->isOwner() && $arr['user_id'] != $this->getUserId()) {
					if ($this->isXHR())
					{
						$_GET['err'] = 9;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=9");
					}
				}
				
				if ($CalendarModel->delete($id))
				{
					Object::import('Model', 'WorkingTime');
					$WorkingTimeModel = new WorkingTimeModel();
					$WorkingTimeModel->delete($id);
					$this->models['Option']->delete(array('calendar_id' => $id, 'calendar_id != 1 AND 1' => 1));
					
					if ($this->isXHR())
					{
						$_GET['err'] = 3;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=3");
					}
				} else {
					if ($this->isXHR())
					{
						$_GET['err'] = 4;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=4");
					}
				}
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
/**
 * List calendars
 *
 * (non-PHPdoc)
 * @see app/controllers/Admin::index()
 * @access public
 * @return void
 */
	function index()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				if (!$this->isMultiCalendar())
				{
					$this->tpl['status'] = 8;
					return;
				}
				
				$opts = array();
				if (isset($_GET['type']) && !empty($_GET['type']))
				{
					$opts['t1.type'] = $_GET['type'];
				}
				
				if ($this->isOwner())
				{
					$opts['t1.user_id'] = $this->getUserId();
				}
				
				Object::import('Model', array('Calendar', 'User'));
				$CalendarModel = new CalendarModel();
				$UserModel = new UserModel();
				
				$page = isset($_GET['page']) && (int) $_GET['page'] > 0 ? intval($_GET['page']) : 1;
				$count = $CalendarModel->getCount($opts);
				$row_count = 10;
				$pages = ceil($count / $row_count);
				$offset = ((int) $page - 1) * $row_count;
				
				$CalendarModel->addJoin($CalendarModel->joins, $UserModel->getTable(), 'TU', array('TU.id' => 't1.user_id'), array('TU.username'));
				$arr = $CalendarModel->getAll(array_merge($opts, array('offset' => $offset, 'row_count' => $row_count, 'col_name' => 't1.calendar_title', 'direction' => 'asc')));
				
				$this->tpl['arr'] = $arr;
				$this->tpl['paginator'] = array('pages' => $pages, 'row_count' => $row_count, 'count' => $count);
				$this->tpl['user_arr'] = $UserModel->getAll(array('t1.role_id' => 2, 'col_name' => 't1.username', 'direction' => 'asc'));
				
				$this->js[] = array('file' => 'jquery.ui.button.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.position.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.dialog.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				
				$this->css[] = array('file' => 'jquery.ui.button.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				$this->css[] = array('file' => 'jquery.ui.dialog.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminCalendars.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
/**
 * Update calendar
 *
 * @param int $id
 * @access public
 * @return void
 */
	function update($id=null)
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				Object::import('Model', 'Calendar');
				$CalendarModel = new CalendarModel();
					
				if (isset($_POST['calendar_update']))
				{
					if ($this->isDemo())
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=7");
					}
					
					$arr = $CalendarModel->get($_POST['id']);
					if (count($arr) == 0)
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=8");
					} elseif ($this->isOwner() && $arr['user_id'] != $this->getUserId()) {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=9");
					}
					
					$CalendarModel->update($_POST);
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=5");
					
				} else {
					$arr = $CalendarModel->get($id);
					
					if (count($arr) == 0)
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminCalendars&action=index&err=8");
					}
					
					$this->tpl['arr'] = $arr;
					
					Object::import('Model', 'User');
					$UserModel = new UserModel();
					$this->tpl['user_arr'] = $UserModel->getAll(array('t1.role_id' => 2, 'col_name' => 't1.username', 'direction' => 'asc'));
				}
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminCalendars.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
/**
 * View calendar
 *
 * @param int $id
 * @access public
 * @return void
 */
	function view($id)
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				$_SESSION[$this->default_user]['calendar_id'] = $id;
				$this->js[] = array('file' => 'adminCalendars.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
}
?>