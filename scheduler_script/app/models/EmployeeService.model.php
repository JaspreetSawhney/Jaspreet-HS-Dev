<?php
require_once MODELS_PATH . 'App.model.php';
/**
 * EmployeeService model
 *
 * @package ebc
 * @subpackage ebc.app.models
 */
class EmployeeServiceModel extends AppModel
{
/**
 * The name of table's primary key. If PK is over 2 or more columns set this to null
 *
 * @var string
 * @access public
 */
	var $primaryKey = null;
/**
 * The name of table associate with current model
 *
 * @var string
 * @access protected
 */
	var $table = 'service_booking_employees_services';
/**
 * Table schema
 *
 * @var array
 * @access protected
 */
	var $schema = array(
		array('name' => 'employee_id', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'service_id', 'type' => 'int', 'default' => ':NULL')
	);
}
?>