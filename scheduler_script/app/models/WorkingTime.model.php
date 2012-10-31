<?php
require_once MODELS_PATH . 'App.model.php';
class WorkingTimeModel extends AppModel
{
	var $primaryKey = 'calendar_id';
	
	var $table = 'service_booking_working_times';
	
	var $schema = array(
		array('name' => 'calendar_id', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'monday_from', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'monday_to', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'monday_dayoff', 'type' => 'enum', 'default' => 'F'),
		array('name' => 'tuesday_from', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'tuesday_to', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'tuesday_dayoff', 'type' => 'enum', 'default' => 'F'),
		array('name' => 'wednesday_from', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'wednesday_to', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'wednesday_dayoff', 'type' => 'enum', 'default' => 'F'),
		array('name' => 'thursday_from', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'thursday_to', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'thursday_dayoff', 'type' => 'enum', 'default' => 'F'),
		array('name' => 'friday_from', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'friday_to', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'friday_dayoff', 'type' => 'enum', 'default' => 'F'),
		array('name' => 'saturday_from', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'saturday_to', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'saturday_dayoff', 'type' => 'enum', 'default' => 'F'),
		array('name' => 'sunday_from', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'sunday_to', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'sunday_dayoff', 'type' => 'enum', 'default' => 'F')
	);
	
	function getDaysOff($calendar_id)
	{
		$_arr = array();
		$arr = $this->get($calendar_id);
		foreach ($arr as $k => $v)
		{
			if (is_null($v) && (strpos($k, "_from") !== false || strpos($k, "_to") !== false))
			{
				list($key) = explode("_", $k);
				$_arr[$key] = 1;
			}
		}
		return $_arr;
	}
	
	function getWorkingTime($calendar_id, $date)
	{
		$day = strtolower(date("l", strtotime($date)));
		$arr = $this->get($calendar_id);

		if (count($arr) == 0)
		{
			return false;
		}
	
		if ($arr[$day . '_dayoff'] == 'T')
		{
			return array();
		}
		
		$wt = array();
		foreach ($arr as $k => $v)
		{			
			if (strpos($k, $day . '_from') !== false && !is_null($v))
			{
				$d = getdate(strtotime($v));
				$wt['start_hour'] = $d['hours'];
				$wt['start_minutes'] = $d['minutes'];
				$wt['start_ts'] = strtotime($date . " " . $v);
				continue;
			}
		
			if (strpos($k, $day . '_to') !== false && !is_null($v))
			{
				$d = getdate(strtotime($v));
				$wt['end_hour'] = $d['hours'];
				$wt['end_minutes'] = $d['minutes'];
				$wt['end_ts'] = strtotime($date . " " . $v);
				continue;
			}
		}
		return $wt;
	}
	
	function initWorkingTime($calendar_id)
	{
		$data['calendar_id']    = $calendar_id;
		$data['monday_from']    = '08:00:00';
		$data['monday_to']      = '18:00:00';
		$data['tuesday_from']   = '08:00:00';
		$data['tuesday_to']     = '18:00:00';
		$data['wednesday_from'] = '08:00:00';
		$data['wednesday_to']   = '18:00:00';
		$data['thursday_from']  = '08:00:00';
		$data['thursday_to']    = '18:00:00';
		$data['friday_from']    = '08:00:00';
		$data['friday_to']      = '18:00:00';
		$data['saturday_from']  = '08:00:00';
		$data['saturday_to']    = '18:00:00';
		$data['sunday_from']    = '08:00:00';
		$data['sunday_to']      = '18:00:00';
		return $this->save($data);
	}
}