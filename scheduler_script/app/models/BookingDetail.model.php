<?php
require_once MODELS_PATH . 'App.model.php';
/**
 * BookingDetail model
 *
 * @package ebc
 * @subpackage ebc.app.models
 */
class BookingDetailModel extends AppModel
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
	var $table = 'service_booking_bookings_details';
/**
 * Table schema
 *
 * @var array
 * @access protected
 */
	var $schema = array(
		array('name' => 'id', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'booking_id', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'service_id', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'employee_id', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'date', 'type' => 'date', 'default' => ':NULL'),
		array('name' => 'start', 'type' => 'time', 'default' => ':NULL'),
		array('name' => 'start_ts', 'type' => 'int', 'default' => ':NULL'),
		array('name' => 'total', 'type' => 'smallint', 'default' => ':NULL'),
		array('name' => 'price', 'type' => 'decimal', 'default' => ':NULL'),
		array('name' => 'reminder_email', 'type' => 'tinyint', 'default' => 0),
		array('name' => 'reminder_sms', 'type' => 'tinyint', 'default' => 0)
	);
}
?>