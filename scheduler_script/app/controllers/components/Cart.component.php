<?php
class Cart
{
	var $cartName;
	
	function Cart($cartName)
	{
		$this->cartName = $cartName;
		
		if (!array_key_exists($this->cartName, $_SESSION))
		{
			$this->reset();
		}
	}
	
	function add($cid, $date, $service_id, $time_pair, $employee_id)
	{
		if ($this->check($cid, $date, $service_id))
		{
			$this->remove($cid, $date, $service_id);
		}
		$this->insert($cid, $date, $service_id, $time_pair, $employee_id);
	}
	
	function check($cid, $date, $service_id)
	{
		return array_key_exists($cid, $_SESSION[$this->cartName]) && 
			array_key_exists($date, $_SESSION[$this->cartName][$cid]) && 
			array_key_exists($service_id, $_SESSION[$this->cartName][$cid][$date]);
	}
	
	function insert($cid, $date, $service_id, $time_pair, $employee_id)
	{
		$_SESSION[$this->cartName][$cid][$date][$service_id][$time_pair] = $employee_id;
	}
	
	function remove($cid, $date, $service_id)
	{
		if ($this->check($cid, $date, $service_id))
		{
			unset($_SESSION[$this->cartName][$cid][$date][$service_id]);
			if (count($_SESSION[$this->cartName][$cid][$date]) == 0)
			{
				unset($_SESSION[$this->cartName][$cid][$date]);
			}
		}
	}

	function reset()
	{
		$_SESSION[$this->cartName] = array();
	}
}
?>