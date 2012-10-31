<?php
require_once CONTROLLERS_PATH . 'AppController.controller.php';
/**
 * Admin controller
 *
 * @package ebc
 * @subpackage ebc.app.controllers
 */
class Admin extends AppController
{
/**
 * Hold name of current layout
 *
 * @access public
 * @var string
 */
	var $layout = 'admin';
/**
 * Hold the name of session variable which store all the login information
 *
 * @access public
 * @var string
 * @example $_SESSION[$this->default_user] = 'test';
 */
	var $default_user = 'admin_user';
/**
 * Hold the name of session variable which store selected language iso, e.g. 'en'
 *
 * @access public
 * @var string
 * @example $_SESSION[$this->default_language] = 'test';
 */
	var $default_language = 'admin_language';
/**
 * Whether to requre login or not
 *
 * @access private
 * @var bool
 */
	var $require_login = true;
/**
 * Constructor
 *
 * @param bool $require_login
 */
	function Admin($require_login=null)
	{
		if (!is_null($require_login) && is_bool($require_login))
		{
			$this->require_login = $require_login;
		}
		
		if ($this->require_login)
		{
			if (!$this->isLoged() && @$_GET['action'] != 'login')
			{
				Util::redirect($_SERVER['PHP_SELF'] . "?controller=Admin&action=login");
			}
		}
	}
/**
 * (non-PHPdoc)
 * @see core/framework/Controller::afterFilter()
 */
	function afterFilter()
	{
		Object::import('Model', 'Calendar');
		$CalendarModel = new CalendarModel();
		$opts = array();
		if ($this->isOwner())
		{
			$opts['t1.user_id'] = $this->getUserId();
		}
		$this->tpl['_calendar_arr'] = $CalendarModel->getAll(array_merge($opts, array('col_name' => 't1.calendar_title', 'direction' => 'asc')));
	}
/**
 * (non-PHPdoc)
 * @see core/framework/Controller::beforeFilter()
 */
	function beforeFilter()
	{
		$this->js[] = array('file' => 'jquery-1.6.4.min.js', 'path' => LIBS_PATH . 'jquery/');
		$this->js[] = array('file' => 'admin-core.js', 'path' => JS_PATH);
		
		$this->js[] = array('file' => 'jquery.ui.core.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
		$this->js[] = array('file' => 'jquery.ui.widget.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
		$this->js[] = array('file' => 'jquery.ui.tabs.min.js', 'path' => LIBS_PATH . 'jquery/ui/js/');
		
		$this->css[] = array('file' => 'jquery.ui.core.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
		$this->css[] = array('file' => 'jquery.ui.theme.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
		$this->css[] = array('file' => 'jquery.ui.tabs.css', 'path' => LIBS_PATH . 'jquery/ui/css/smoothness/');
				
		$this->css[] = array('file' => 'admin.css', 'path' => CSS_PATH);
		
		Object::import('Model', 'Option');
		$OptionModel = new OptionModel();
		$this->models['Option'] = $OptionModel;
		$this->option_arr = $OptionModel->getPairs($this->getCalendarId());
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
	
			AppController::setTimezone('Etc/GMT' . $offset);
			if (strpos($offset, '-') !== false)
			{
				$offset = str_replace('-', '+', $offset);
			} elseif (strpos($offset, '+') !== false) {
				$offset = str_replace('+', '-', $offset);
			}
			AppController::setMySQLServerTime($offset . ":00");
		}
	}
/**
 * (non-PHPdoc)
 * @see core/framework/Controller::beforeRender()
 */
	function beforeRender()
	{
		
	}
/**
 * (non-PHPdoc)
 * @see core/framework/Controller::index()
 * @access public
 * @return void
 */
	function index()
	{
		if ($this->isLoged())
		{
			if ($this->isAdmin())
			{
								
			} else {
				$this->tpl['status'] = 2;
			}
		} else {
			$this->tpl['status'] = 1;
		}
	}
/**
 * Log in user
 *
 * @access public
 * @return void
 */
	function login()
	{
		$this->layout = 'admin_login';
		
		if (isset($_POST['login_user']))
		{
			Object::import('Model', 'User');
			$UserModel = new UserModel();

			$opts['username'] = $_POST['login_username'];
			$opts['password'] = $_POST['login_password'];
			
			$user = $UserModel->getAll($opts);

			if (count($user) != 1)
			{
				# Login failed
				Util::redirect($_SERVER['PHP_SELF'] . "?controller=Admin&action=login&err=1");
			} else {
				$user = $user[0];
				#unset($user['password']);
															
				if (!in_array($user['role_id'], array(1, 2)))
				{
					# Login denied
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=Admin&action=login&err=2");
				}
				
				if ($user['status'] != 'T')
				{
					# Login forbidden
					Util::redirect($_SERVER['PHP_SELF'] . "?controller=Admin&action=login&err=3");
				}
				
				# Login succeed
    			$_SESSION[$this->default_user] = $user;
    			
    			Object::import('Model', 'Calendar');
				$CalendarModel = new CalendarModel();
				$options = array();
				if ($this->isOwner())
				{
					$options['t1.user_id'] = $this->getUserId();
				}
				$arr = $CalendarModel->getAll(array_merge($options, array('col_name' => 't1.id', 'direction' => 'asc', 'row_count' => 1, 'offset' => 0)));
				if (count($arr) == 1)
				{
					$_SESSION[$this->default_user]['calendar_id'] = $arr[0]['id'];
				} else {
					$_SESSION[$this->default_user]['calendar_id'] = NULL;
				}
    			
    			# Update
    			$data['id'] = $user['id'];
    			$data['last_login'] = date("Y-m-d H:i:s");
    			$UserModel->update($data);

    			if ($this->isAdmin())
    			{
	    			Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=schedule");
    			}
    			
				if ($this->isOwner())
    			{
	    			Util::redirect($_SERVER['PHP_SELF'] . "?controller=AdminBookings&action=schedule");
    			}
			}
		}
		$this->js[] = array('file' => 'jquery.validate.min.js', 'path' => LIBS_PATH . 'jquery/plugins/validate/js/');
		$this->js[] = array('file' => 'admin.js', 'path' => JS_PATH);
		return false;
	}
/**
 * Log out user
 *
 * @access public
 * @return void
 */
	function logout()
	{
		if ($this->isLoged())
        {
        	unset($_SESSION[$this->default_user]);
        	Util::redirect($_SERVER['PHP_SELF'] . "?controller=Admin&action=login");
        } else {
        	Util::redirect($_SERVER['PHP_SELF'] . "?controller=Admin&action=login");
        }
	}
/**
 * Change current locale
 *
 * @param string $iso
 * @access public
 * @return void
 */
	function local($iso)
	{
		if (in_array(strtolower($iso), array('en')))
		{
			$_SESSION[$this->default_language] = $iso;
		}
				
		Util::redirect($_SESSION['PHP_SELF'] . "?controller=Admin&action=index");
	}
}