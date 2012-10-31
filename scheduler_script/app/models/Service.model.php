<?php
require_once MODELS_PATH . 'App.model.php';
/**
 * Service model
 *
 * @package ebc
 * @subpackage ebc.app.models
 */
class ServiceModel extends AppModel
{
/**
 * The name of table's primary key. If PK is over 2 or more columns set this to boolean null
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
	var $table = 'service_booking_services';
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
		array('name' => 'description', 'type' => 'text', 'default' => ':NULL'),
		array('name' => 'price', 'type' => 'decimal', 'default' => ':NULL'),
		array('name' => 'length', 'type' => 'smallint', 'default' => ':NULL'),
		array('name' => 'before', 'type' => 'smallint', 'default' => ':NULL'),
		array('name' => 'after', 'type' => 'smallint', 'default' => ':NULL'),
		array('name' => 'total', 'type' => 'smallint', 'default' => ':NULL')
	);
}
?>