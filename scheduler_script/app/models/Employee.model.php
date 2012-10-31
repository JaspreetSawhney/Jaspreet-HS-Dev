<?php
require_once MODELS_PATH . 'App.model.php';
/**
 * Employee model
 *
 * @package ebc
 * @subpackage ebc.app.models
 */
class EmployeeModel extends AppModel
{
/**
 * The name of table's primary key. If PK is over 2 or more columns set this to null
 *
 * @var string
 * @access public
 */
	var $primaryKey = 'id';
/**
 * The name of table associate with current model
 *
 * @var string
 * @access protected
 */
	var $table = 'service_booking_employees';
/**
 * Table schema
 *
 * @var array
 * @access protected
 */
	var $schema = array(
		array('name' => 'id', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'calendar_id', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'name', 'type' => 'varchar', 'default' => ':NULL'),
		array('name' => 'email', 'type' => 'varchar', 'default' => ':NULL'),
		array('name' => 'phone', 'type' => 'varchar', 'default' => ':NULL'),
		array('name' => 'notes', 'type' => 'text', 'default' => ':NULL'),
		array('name' => 'send_email', 'type' => 'tinyint', 'default' => 0)
	);
}
?>