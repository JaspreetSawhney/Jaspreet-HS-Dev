<?php
require_once FRAMEWORK_PATH . 'Controller.class.php';
/**
 * App controller
 *
 * @package ebc
 * @subpackage ebc.app.controllers
 */
class AppController extends Controller
{
	var $cartName = 'SBScript_Cart';
/**
 * Model's cache
 *
 * @var array
 * @access protected
 */
	var $models = array();
/**
 * Multi calendar support
 *
 * @var bool
 * @access private
 */
	var $multiCalendar = true;
/**
 * Multi user support
 *
 * @var bool
 * @access private
 */
	var $multiUser = true;
/**
 * Check if multi-calendar support is enabled
 *
 * @return bool
 * @access public
 */
	function isMultiCalendar()
	{
		return $this->multiCalendar;
	}
/**
 * Check if multi-user support is enabled
 *
 * @return bool
 * @access public
 */
	function isMultiUser()
	{
		return $this->multiUser;
	}
/**
 * Check loged user against 'owner' role
 *
 * @access public
 * @return bool
 */
	function isOwner()
    {
   		return $this->getRoleId() == 2;
    }
/**
 * Get calendar ID
 *
 * @access public
 * @return int|false
 */
    function getCalendarId()
    {
    	return isset($_SESSION[$this->default_user]) && array_key_exists('calendar_id', $_SESSION[$this->default_user]) ? $_SESSION[$this->default_user]['calendar_id'] : false;
    }
/**
 * Build CSS by given $calendar_id
 *
 * @param int $calendar_id
 * @access public
 * @return string
 */
    function css($calendar_id)
    {
    	Object::import('Model', 'Option');
    	$OptionModel = new OptionModel();
    	$option_arr = $OptionModel->getPairs($calendar_id);

		$subject = @file_get_contents(CSS_PATH . 'calendar.txt');
		$search = array(
			'[calendar_width]',
			'[font_size]',
			'[font_family]',
			'[calendarContainer]'
		);
		
		$replace = array(
			$option_arr['calendar_width'],
			$option_arr['font_size'],
			$option_arr['font_family'],
			'#StivaApp_' . $calendar_id
		);
		
		header("Content-type: text/css");
		echo str_replace($search, $replace, $subject);
		exit;
    }
    
    function getCartPrices($calendar_id, $cartName)
    {
    	Object::import('Model', 'Service');
		$ServiceModel = new ServiceModel();
		
		$_arr = array();
		if (isset($_SESSION[$cartName]) && isset($_SESSION[$cartName][$calendar_id]))
		{
			foreach ($_SESSION[$cartName][$calendar_id] as $service_arr)
			{
				$_arr = array_merge($_arr, array_keys($service_arr));
			}
		}
		$_arr = array_unique($_arr);
		$s_arr = $ServiceModel->getAll(array('t1.id' => array("('" . join("','", $_arr) . "')", 'IN', 'null'), 't1.calendar_id' => $calendar_id));
		$service_arr = array();
		foreach ($s_arr as $service)
		{
			$service_arr[$service['id']] = $service;
		}
		return $service_arr;
    }
    
    function getCartTotal($calendar_id, $cartName, $option_arr)
    {
    	$arr = AppController::getCartPrices($calendar_id, $cartName);
    	$price = 0;
    	if (isset($_SESSION[$cartName]))
    	{
    		foreach ($_SESSION[$cartName] as $cid => $date_arr)
    		{
    			if ($cid != $calendar_id)
				{
					continue;
				}
				foreach ($date_arr as $date => $service_arr)
				{
					foreach ($service_arr as $service_id => $time_arr)
					{
						if (isset($arr[$service_id]) && isset($arr[$service_id]['price']))
						{
							$price += (float) $arr[$service_id]['price'];
						}
					}
				}
    		}
    	}
    	
    	$tax = ($price * $option_arr['tax']) / 100;
		$total = $price + $tax;
		$deposit = ($total * $option_arr['deposit_percent']) / 100;

		return array('price' => round($price, 2), 'total' => round($total, 2), 'deposit' => round($deposit, 2), 'tax' => round($tax, 2));
    }
/**
 * Return an array with range of slots for given date
 * 
 * @param int $calendar_id
 * @param string $date
 * @param array $option_arr
 * @static
 * @return array|false
 */
	function getRawSlots($calendar_id, $date, $option_arr)
	{
		Object::import('Model', 'Date');
		$DateModel = new DateModel();
			
		$date_arr = $DateModel->getWorkingTime($calendar_id, $date);
		if ($date_arr === false)
		{
			# There is not custom working time/prices for given date, so get for day of week (Monday, Tuesday...)
			Object::import('Model', 'WorkingTime');
			$WorkingTimeModel = new WorkingTimeModel();
			$wt_arr = $WorkingTimeModel->getWorkingTime($calendar_id, $date);
			if (count($wt_arr) == 0)
			{
				# It's Day off
				return false;
			}
			//$wt_arr['slot_length'] = $option_arr['slot_length'];
			$t_arr = $wt_arr;
		} else {
			# There is custom working time/prices for given date
			if (count($date_arr) == 0)
			{
				# It's Day off
				return false;
			}
			$t_arr = $date_arr;
		}
		return $t_arr;
	}
/**
 * Set timezone
 *
 * @param int $timezone
 * @access public
 * @return void
 * @static
 */
    function setTimezone($timezone="UTC")
    {
    	if (in_array(version_compare(phpversion(), '5.1.0'), array(0,1)))
		{
			date_default_timezone_set($timezone);
		} else {
			$safe_mode = ini_get('safe_mode');
			if ($safe_mode)
			{
				putenv("TZ=".$timezone);
			}
		}
    }
/**
 * Set MySQL server time
 *
 * @param int $offset
 * @access public
 * @return void
 * @static
 */
    function setMySQLServerTime($offset="-0:00")
    {
		mysql_query("SET SESSION time_zone = '$offset';");
    }
}
?>