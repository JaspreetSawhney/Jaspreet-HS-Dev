<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.Front
 */
list($_GET['month'], $_GET['year']) = explode("-", date("n-Y"));
?>
<div id="StivaApp_<?php echo $_GET['cid']; ?>">
	<div id="StivaApp_Left_<?php echo $_GET['cid']; ?>" class="StivaApp_Left">
		<div id="StivaApp_Date_<?php echo $_GET['cid']; ?>" class="StivaApp_Date StivaApp_Box StivaApp_B10">
			<div class="StivaApp_Box_Top">
				<div class="StivaApp_Box_Top_Left"></div>
				<div class="StivaApp_Box_Top_Right"></div>
				<span class="StivaApp_Box_Top_Point">1</span>
				<?php echo $SBS_LANG['front']['step_1']; ?>
			</div>
			<div class="StivaApp_Box_Middle">
				<span id="SBScript_Date_<?php echo $_GET['cid']; ?>"></span>
			</div>
			<div class="StivaApp_Box_Bottom">
				<div class="StivaApp_Box_Bottom_Left"></div>
				<div class="StivaApp_Box_Bottom_Right"></div>
			</div>
		</div>
		
		<div id="StivaApp_Selected_<?php echo $_GET['cid']; ?>" class="StivaApp_Selected StivaApp_Box">
			<div class="StivaApp_Box_Top">
				<div class="StivaApp_Box_Top_Left"></div>
				<div class="StivaApp_Box_Top_Right"></div>
				<span class="StivaApp_Box_Top_Point">4</span>
				<?php echo $SBS_LANG['front']['step_4']; ?>
			</div>
			<div id="StivaApp_Basket_<?php echo $_GET['cid']; ?>" class="StivaApp_Box_Middle"></div>
			<div class="StivaApp_Box_Bottom">
				<div class="StivaApp_Box_Bottom_Left"></div>
				<div class="StivaApp_Box_Bottom_Right"></div>
			</div>
		</div>
	</div>
	<div id="StivaApp_Services_<?php echo $_GET['cid']; ?>" class="StivaApp_Services"></div>
	<div id="StivaApp_Preload_<?php echo $_GET['cid']; ?>" style="display: none; width: <?php echo $tpl['option_arr']['calendar_width']; ?>px" class="SBScript_Preload"></div>
</div>

<script type="text/javascript">
var StivaApp_<?php echo $_GET['cid']; ?> = new StivaApp({
	booking_form_name: "EBCalBookingForm_<?php echo $_GET['cid']; ?>",
	booking_form_submit_name: "EBCalBookingFormSubmit",
	booking_form_cancel_name: "EBCalBookingFormCancel",
	booking_form_confirm_name: "EBCalBookingFormConfirm",
	booking_form_payment_method: "payment_method",
	
	booking_summary_name: "EBCalBookingSummary_<?php echo $_GET['cid']; ?>",
	booking_summary_submit_name: "EBCalBookingSummarySubmit",
	booking_summary_cancel_name: "EBCalBookingSummaryCancel",
	
	calendar_id: <?php echo $_GET['cid']; ?>,

	container_services: "StivaApp_Services_<?php echo $_GET['cid']; ?>",
	preload: "StivaApp_Preload_<?php echo $_GET['cid']; ?>",
	datepicker: "SBScript_Date_<?php echo $_GET['cid']; ?>",
	container_basket: "StivaApp_Basket_<?php echo $_GET['cid']; ?>",
	
	class_name_dates: "calendar",
	class_name_events: "SBScript_Event",
	class_name_month: "calendarLinkMonth",
	class_name_price: "SBScript_Price",
	events_close: "SBScript_Close",
	
	get_service_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=Front&action=getService",
	get_services_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=Front&action=getServices",

	get_cart_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=FrontCart&action=basket",
	get_add_cart_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=FrontCart&action=add",
	get_remove_cart_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=FrontCart&action=remove",
	
	get_booking_form_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=Front&action=bookingForm",
	get_booking_summary_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=Front&action=bookingSummary",
	get_booking_captcha_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=Front&action=checkCaptcha",
	get_booking_save_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=Front&action=bookingSave",
	get_payment_form_url: "<?php echo INSTALL_FOLDER; ?>index.php?controller=Front&action=paymentForm",

	message_1: "<?php echo $SBS_LANG['front']['msg_1']; ?>",
	message_2: "<?php echo $SBS_LANG['front']['msg_2']; ?>",
	message_4: "<?php echo $SBS_LANG['front']['msg_4']; ?>",
	message_5: "<?php echo $SBS_LANG['front']['msg_5']; ?>",
	message_6: "<?php echo $SBS_LANG['front']['msg_6']; ?>",
	message_7: "<?php echo $SBS_LANG['front']['msg_7']; ?>",
	message_8: "<?php echo $SBS_LANG['front']['msg_8']; ?>",

	validation: {
		error_title: "<?php echo $SBS_LANG['front']['v_err_title']; ?>",
		error_email: "<?php echo $SBS_LANG['front']['v_err_email']; ?>",
		error_captcha: "<?php echo $SBS_LANG['front']['v_err_captcha']; ?>",
		error_payment: "<?php echo $SBS_LANG['front']['v_err_payment']; ?>",
		error_min: "<?php echo $SBS_LANG['front']['v_err_min']; ?>"
	},

	payment: {
		paypal: "SBScript_FormPaypal_<?php echo $_GET['cid']; ?>",
		authorize: "SBScript_FormAuthorize_<?php echo $_GET['cid']; ?>"
	},

	cc_data_wrapper: "SBScript_CCData",
	cc_data_names: ["cc_type", "cc_num", "cc_exp_month", "cc_exp_year", "cc_code"],
	cc_data_flag: <?php echo $tpl['option_arr']['payment_enable_creditcard'] == 'Yes' ? 'true' : 'false'; ?>,

	date_format: "<?php echo $tpl['option_arr']['date_format']; ?>",
	start_day: 1,
	month: <?php echo date("n"); ?>,
	year: <?php echo date("Y"); ?>,
	today: "<?php echo date($tpl['option_arr']['date_format']); ?>",
	view: 1,
	checkout: <?php echo !isset($_GET['checkout']) ? 1 : 0; ?>
});
</script>