<?php
require_once CONTROLLERS_PATH . 'Admin.controller.php';
/**
 * AdminServices controller
 *
 * @package ebc
 * @subpackage ebc.app.controllers
 */
class AdminServices extends Admin
{
/**
 * Create service
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
				if (isset($_POST['service_create']))
				{
					if ($this->isDemo())
					{
						$err = 7;
					} else {
					
						Object::import('Model', 'Service');
						$ServiceModel = new ServiceModel();
	
						$data = array();
						$data['calendar_id'] = $this->getCalendarId();
						$insert_id = $ServiceModel->save(array_merge($_POST, $data));
						if ($insert_id !== false && (int) $insert_id > 0)
						{
	            			$err = 1;
						} else {
							$err = 2;
						}
					}
				}
				Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminServices&action=index&err=$err");
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
		
		
	}
/**
 * Delete a service
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
				
				Object::import('Model', 'Service');
				$ServiceModel = new ServiceModel();
					
				$arr = $ServiceModel->get($id);
				if (count($arr) == 0)
				{
					if ($this->isXHR())
					{
						$_GET['err'] = 8;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminServices&action=index&err=8");
					}
				}
				
				if ($ServiceModel->delete($id))
				{
					if ($this->isXHR())
					{
						$_GET['err'] = 3;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminServices&action=index&err=3");
					}
				} else {
					if ($this->isXHR())
					{
						$_GET['err'] = 4;
						$this->index();
						return;
					} else {
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminServices&action=index&err=4");
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
 * List services
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
				$opts = array();
				if (isset($_GET['type']) && !empty($_GET['type']))
				{
					$opts['t1.type'] = $_GET['type'];
				}
				
				Object::import('Model', 'Service');
				$ServiceModel = new ServiceModel();
				
				$page = isset($_GET['page']) && (int) $_GET['page'] > 0 ? intval($_GET['page']) : 1;
				$count = $ServiceModel->getCount($opts);
				$row_count = 10;
				$pages = ceil($count / $row_count);
				$offset = ((int) $page - 1) * $row_count;
				
				$arr = $ServiceModel->getAll(array_merge($opts, array('offset' => $offset, 'row_count' => $row_count, 'col_name' => 't1.name', 'direction' => 'asc')));
				
				$this->tpl['arr'] = $arr;
				$this->tpl['paginator'] = array('pages' => $pages, 'row_count' => $row_count, 'count' => $count);
				
				$this->js[] = array('file' => 'jquery.ui.button.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.position.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				$this->js[] = array('file' => 'jquery.ui.dialog.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
				
				$this->css[] = array('file' => 'jquery.ui.button.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				$this->css[] = array('file' => 'jquery.ui.dialog.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminServices.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
/**
 * Update service
 *
 * @param int $id
 * @access public
 * @return void
 */
	function update()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin() || $this->isOwner())
			{
				Object::import('Model', 'Service');
				$ServiceModel = new ServiceModel();
					
				if (isset($_POST['service_update']))
				{
					if ($this->isDemo())
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminServices&action=index&err=7");
					}
					$data = array();
					$data['calendar_id'] = $this->getCalendarId();
					$ServiceModel->update(array_merge($_POST, $data));
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminServices&action=index&err=5");
					
				} else {
					$arr = $ServiceModel->get($_GET['id']);
					
					if (count($arr) == 0)
					{
						Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminServices&action=index&err=8");
					}
					$this->tpl['arr'] = $arr;
				}
				$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
				$this->js[] = array('file' => 'adminServices.js', 'path' => JS_PATH);
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
}
?>