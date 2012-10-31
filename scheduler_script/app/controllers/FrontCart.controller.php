<?php
require_once CONTROLLERS_PATH . 'Front.controller.php';
class FrontCart extends Front
{
	var $cart = null;
	
	function FrontCart()
	{
		Object::import('Component', 'Cart');
		$this->cart = new Cart($this->cartName);
	}
	
	function add()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			$this->cart->add($_GET['cid'], $_POST['date'], $_POST['service_id'], $_POST['start_ts'] . "|" . $_POST['end_ts'], $_POST['employee_id']);
		}
	}
	
	function basket()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			$this->tpl['cart_arr'] = $_SESSION[$this->cartName];
			$this->tpl['service_arr'] = AppController::getCartPrices($_GET['cid'], $this->cartName);
		}
	}
	
	function remove()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			$this->cart->remove($_GET['cid'], $_POST['date'], $_POST['service_id']);
		}
	}
	
	function reset()
	{
		$this->isAjax = true;
		
		if ($this->isXHR())
		{
			$this->cart->reset();
		}
	}
}
?>