<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.AdminBookings
 */
if (isset($tpl['status']))
{
	switch ($tpl['status'])
	{
		case 1:
			?><p class="status_err"><?php echo $SBS_LANG['status'][1]; ?></p><?php
			break;
		case 2:
			?><p class="status_err"><?php echo $SBS_LANG['status'][2]; ?></p><?php
			break;
	}
} else {
	?>
	<style type="text/css">
	#StivaApp_<?php echo $controller->getCalendarId(); ?> .SBScript_Holder, 
	#StivaApp_<?php echo $controller->getCalendarId(); ?> .SBScript_W_Holder {
    	width: 100%;
	}	
	#StivaApp_<?php echo $controller->getCalendarId(); ?> .StivaApp_Services{
		width: 535px;
	}
	</style>
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1"><?php echo $SBS_LANG['booking_update']; ?></a></li>
		</ul>
		<div id="tabs-1">
			<span id="dialogBlockUI" style="display: none" title="<?php echo $SBS_LANG['booking_s_title']; ?>"><?php echo $SBS_LANG['booking_s_body']; ?></span>
			
			<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminBookings&amp;action=update" method="post" id="frmUpdateBooking" class="ebc-form">
				<input type="hidden" name="booking_update" value="1" />
				<input type="hidden" name="id" value="<?php echo $tpl['arr']['id']; ?>" />
				<div id="boxServices"><?php include VIEWS_PATH . 'AdminBookings/getBookedServices.php'; ?></div>
				<?php if (!$controller->isAjax()) : ?>
				<br />
				<input type="button" value="" class="button button_add_service" id="btnAddService" />				
				<div style="display: none" class="t5" id="boxScript">
					<script type="text/javascript" src="index.php?controller=Front&action=load&cid=<?php echo $controller->getCalendarId(); ?>&checkout=0"></script>
				</div>
				<?php endif; ?>
				<p><label class="title"><?php echo $SBS_LANG['booking_total']; ?>:</label><input type="text" name="booking_total" id="booking_total" class="text align_right w80 number" value="<?php echo $tpl['arr']['booking_total']; ?>" /></p>
				<p><label class="title"><?php echo $SBS_LANG['booking_deposit']; ?>:</label><input type="text" name="booking_deposit" id="booking_deposit" class="text align_right w80 number" value="<?php echo $tpl['arr']['booking_deposit']; ?>" /></p>
				<p><label class="title"><?php echo $SBS_LANG['booking_tax']; ?>:</label><input type="text" name="booking_tax" id="booking_tax" class="text align_right w80 number" value="<?php echo $tpl['arr']['booking_tax']; ?>" /></p>
				<?php /*
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_option']; ?>:</label>
					<select name="payment_option" id="payment_option" class="select w150">
					<?php
					foreach ($SBS_LANG['booking_options'] as $k => $v)
					{
						if ($tpl['arr']['payment_option'] == $k)
						{
							?><option value="<?php echo $k; ?>" selected="selected"><?php echo $v; ?></option><?php
						} else {
							?><option value="<?php echo $k; ?>"><?php echo $v; ?></option><?php
						}
					}
					?>
					</select>
				</p> */?>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_booking_status']; ?>:</label>
					<select name="booking_status" id="booking_status" class="select w150">
					<?php
					foreach ($SBS_LANG['booking_booking_statuses'] as $k => $v)
					{
						if ($tpl['arr']['booking_status'] == $k)
						{
							?><option value="<?php echo $k; ?>" selected="selected"><?php echo $v; ?></option><?php
						} else {
							?><option value="<?php echo $k; ?>"><?php echo $v; ?></option><?php
						}
					}
					?>
					</select>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_payment_method']; ?>:</label>
					<select name="payment_method" id="payment_method" class="select w150">
						<option value="">---</option>
					<?php
					foreach ($SBS_LANG['booking_payment_methods'] as $k => $v)
					{
						if ($tpl['arr']['payment_method'] == $k)
						{
							?><option value="<?php echo $k; ?>" selected="selected"><?php echo $v; ?></option><?php
						} else {
							?><option value="<?php echo $k; ?>"><?php echo $v; ?></option><?php
						}
					}
					?>
					</select>
				</p>
				<p class="cc" style="display: <?php echo $tpl['arr']['payment_method'] == 'creditcard' ? 'block' : 'none'; ?>">
					<label class="title"><?php echo $SBS_LANG['booking_cc_type']; ?></label>
					<select name="cc_type" class="select">
						<option value="">---</option>
						<?php
						foreach ($SBS_LANG['front']['bf_cc_types'] as $k => $v)
						{
							if ($tpl['arr']['cc_type'] == $v)
							{
								?><option value="<?php echo $k; ?>" selected="selected"><?php echo $v; ?></option><?php
							} else {
								?><option value="<?php echo $k; ?>"><?php echo $v; ?></option><?php
							}
						}
						?>
					</select>
				</p>
				<p class="cc" style="display: <?php echo $tpl['arr']['payment_method'] == 'creditcard' ? 'block' : 'none'; ?>">
					<label class="title"><?php echo $SBS_LANG['booking_cc_num']; ?></label>
					<input type="text" name="cc_num" class="text w300" value="<?php echo htmlspecialchars($tpl['arr']['cc_num']); ?>" />
				</p>
				<p class="cc" style="display: <?php echo $tpl['arr']['payment_method'] == 'creditcard' ? 'block' : 'none'; ?>">
					<label class="title"><?php echo $SBS_LANG['booking_cc_exp']; ?></label>
					<?php
					$exp_y = $exp_m = NULL;
					if (strpos($tpl['arr']['cc_exp'], "-") !== false)
					{
						list($exp_y, $exp_m) = explode("-", $tpl['arr']['cc_exp']);
					}
					?>
					<select name="cc_exp_month" id="cc_exp_month" class="select">
						<option value="">---</option>
						<?php
						foreach ($SBS_LANG['month_name'] as $key => $val)
						{
							?><option value="<?php echo $key; ?>"<?php echo $key == $exp_m ? ' selected="selected"' : NULL; ?>><?php echo $val; ?></option><?php
						}
						?>
					</select>
					<select name="cc_exp_year" id="cc_exp_year" class="select">
						<option value="">---</option>
						<?php
						$y = (int) date('Y');
						for ($i = $y; $i <= $y + 10; $i++)
						{
							?><option value="<?php echo $i; ?>"<?php echo $i == $exp_y ? ' selected="selected"' : NULL; ?>><?php echo $i; ?></option><?php
						}
						?>
					</select>
				</p>
				<p class="cc" style="display: <?php echo $tpl['arr']['payment_method'] == 'creditcard' ? 'block' : 'none'; ?>">
					<label class="title"><?php echo $SBS_LANG['booking_cc_code']; ?></label>
					<input type="text" name="cc_code" class="text w100" value="<?php echo htmlspecialchars($tpl['arr']['cc_code']); ?>" />
				</p>
				<p><label class="title"><?php echo $SBS_LANG['booking_customer_name']; ?>:</label><input type="text" name="customer_name" id="customer_name" class="text w500" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['customer_name'])); ?>" /></p>
				<p><label class="title"><?php echo $SBS_LANG['booking_customer_email']; ?>:</label><input type="text" name="customer_email" id="customer_email" class="text w500 email" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['customer_email'])); ?>" /></p>
				<p><label class="title"><?php echo $SBS_LANG['booking_customer_phone']; ?>:</label><input type="text" name="customer_phone" id="customer_phone" class="text w500" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['customer_phone'])); ?>" /></p>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_customer_country']; ?>:</label>
					<select name="customer_country" class="select">
						<option value="">---</option>
						<?php
						foreach ($tpl['country_arr'] as $v)
						{
							if ($tpl['arr']['customer_country'] == $v['id'])
							{
								?><option value="<?php echo $v['id']; ?>" selected="selected"><?php echo stripslashes($v['country_title']); ?></option><?php
							} else {
								?><option value="<?php echo $v['id']; ?>"><?php echo stripslashes($v['country_title']); ?></option><?php
							}
						}
						?>
					</select>
				</p>
				<p><label class="title"><?php echo $SBS_LANG['booking_customer_city']; ?>:</label><input type="text" name="customer_city" id="customer_city" class="text w500" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['customer_city'])); ?>" /></p>
				<p><label class="title"><?php echo $SBS_LANG['booking_customer_state']; ?>:</label><input type="text" name="customer_state" id="customer_state" class="text w500" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['customer_state'])); ?>" /></p>
				<p><label class="title"><?php echo $SBS_LANG['booking_customer_zip']; ?>:</label><input type="text" name="customer_zip" id="customer_zip" class="text w500" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['customer_zip'])); ?>" /></p>
				<p><label class="title"><?php echo $SBS_LANG['booking_customer_address']; ?>:</label><input type="text" name="customer_address" id="customer_address" class="text w500" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['customer_address'])); ?>" /></p>
				<p><label class="title"><?php echo $SBS_LANG['booking_customer_notes']; ?>:</label><textarea name="customer_notes" id="customer_notes" class="textarea w500 h100"><?php echo htmlspecialchars(stripslashes($tpl['arr']['customer_notes'])); ?></textarea></p>
				<p id="errContainer">
					<label for="valid_price" class="error" style="display: none"><?php echo $SBS_LANG['booking_validation']['valid_price']; ?></label>
					<label for="valid_num" class="error" style="display: none"><?php echo $SBS_LANG['booking_validation']['valid_num']; ?></label>
				</p>
				<p>
					<label class="title">&nbsp;</label>
					<input type="submit" value="" class="button button_save" />
				</p>
				
			</form>
		</div>
	</div>
	<div id="dialogDeleteBookedService" title="<?php echo htmlspecialchars($SBS_LANG['booking_b_del_title']); ?>" style="display: none">
		<p><?php echo $SBS_LANG['booking_b_del_body']; ?></p>
	</div>
	<?php
}
?>