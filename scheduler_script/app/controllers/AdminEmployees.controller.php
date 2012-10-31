<?php
require_once CONTROLLERS_PATH . 'Admin.controller.php';
/**
 * AdminEmployees controller
 *
 * @package ebc
 * @subpackage ebc.app.controllers
 */
class AdminEmployees extends Admin
{
/**
 * Create employee
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
				if (isset($_POST['employee_create']))
				{
					if ($this->isDemo())
					{
						$err = 7;
					} else {
						Object::import('Model', 'Employee');
						$EmployeeModel = new EmployeeModel();
					
						$data = array();
						$data['calendar_id'] = $this->getCalendarId();

						$id = $EmployeeModel->save(array_merge($_POST, $data));						
						if ($id !== false && (int) $id > 0)
						{
							if (isset($_POST['service_id']) && count($_POST['service_id']) > 0)
							{
								Object::import('Model', 'EmployeeService');
								$EmployeeServiceModel = new EmployeeServiceModel();
								Object::addLinked($EmployeeServiceModel->getTable(), 'employee_id', 'service_id', $id, $_POST['service_id']);
							}
							$err = 1;
						} else {
							$err = 2;
						}
					}
				}
				Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminEmployees&action=index&err=$err");
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
/**
 * Delete employee, support AJAX too
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
				
				Object::import('Model', 'Employee');
				$EmployeeModel = new EmployeeModel();
					
				$arr = $EmployeeModel->get($id);
				if (count($arr) == 0)
				{
					if ($this->isXHR())
					{
						$_GET['err'] = 1;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminEmployees&action=index&err=8");
					}
				}
				
				if ($EmployeeModel->delete($id))
				{
					Object::import('Model', 'EmployeeService');
					$EmployeeServiceModel = new EmployeeServiceModel();
					$EmployeeServiceModel->delete(array('employee_id' => $id));
					
					if ($this->isXHR())
					{
						$_GET['err'] = 3;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminEmployees&action=index&err=3");
					}
				} else {
					if ($this->isXHR())
					{
						$_GET['err'] = 4;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminEmployees&action=index&err=4");
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
 * List of employees
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
				Object::import('Model', array('Employee', 'Service'));
				$EmployeeModel = new EmployeeModel();
				$ServiceModel = new ServiceModel();
				
				$opts = array();
				
				$page = isset($_GET['page']) && (int) $_GET['page'] > 0 ? intval($_GET['page']) : 1;
				$count = $EmployeeModel->getCount($opts);
				$row_count = 20;
				$pages = ceil($count / $row_count);
				$offset = ((int) $page - 1) * $row_count;
				
				$arr = $EmployeeModel->getAll($opts);
				
				$this->tpl['arr'] = $arr;
				$this->tpl['paginator'] = array('pages' => $pages);
				$this->tpl['service_arr'] = $ServiceModel->getAll(array('t1.calendar_id' => $this->getCalendarId(), 'col_name' => 't1.name', 'direction' => 'asc'));					
				
				$this->js[] = array('file' => 'jquery.ui.button.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.position.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.dialog.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				
				$this->css[] = array('file' => 'jquery.ui.button.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				$this->css[] = array('file' => 'jquery.ui.dialog.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminEmployees.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
/**
 * Update employee
 *
 * @access public
 * @return void
 */
	function update()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{				
				Object::import('Model', array('Employee', 'EmployeeService'));
				$EmployeeModel = new EmployeeModel();
				$EmployeeServiceModel = new EmployeeServiceModel();
				
				if (isset($_POST['employee_update']))
				{
					if ($this->isDemo())
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminEmployees&action=index&err=7");
					}
				
					$data = array();
					$data['calendar_id'] = $this->getCalendarId();
					$data['send_email'] = isset($_POST['send_email']) ? 1 : 0;
					$EmployeeModel->update(array_merge($_POST, $data));
					
					$EmployeeServiceModel->delete(array('employee_id' => $_POST['id']));
					if (isset($_POST['service_id']) && count($_POST['service_id']) > 0)
					{
						Object::addLinked($EmployeeServiceModel->getTable(), 'employee_id', 'service_id', $_POST['id'], $_POST['service_id']);
					}
					
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminEmployees&action=index&err=5");

				} else {
					$arr = $EmployeeModel->get($_GET['id']);
					if (count($arr) === 0)
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminEmployees&action=index&err=8");
					}
					$this->tpl['arr'] = $arr;
					
					Object::import('Model', 'Service');
					$ServiceModel = new ServiceModel();
					$this->tpl['service_arr'] = $ServiceModel->getAll(array('t1.calendar_id' => $this->getCalendarId(), 'col_name' => 't1.name', 'direction' => 'asc'));
					$this->tpl['employee_service_arr'] = Object::getLinked($EmployeeServiceModel->getTable(), 'employee_id', 'service_id', $arr['id']);
				}
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminEmployees.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
}