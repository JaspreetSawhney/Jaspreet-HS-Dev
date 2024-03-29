<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.Front
 */
?>
<div class="SBScript_W_Holder">
	<div class="SBScript_Top">
		<div class="SBScript_Top_Left"></div>
		<div class="SBScript_Top_Right"></div>
		<?php echo $SBS_LANG['front']['bf_form']; ?>
		<a href="#" class="SBScript_Close SBScript_Top_Close"></a>
	</div>
	<div class="SBScript_Middle">
	
<form action="" method="post" name="EBCalBookingForm_<?php echo $_REQUEST['cid']; ?>" class="SBScript_Form SBScript_Font" style="width: auto">
	<?php
	if (in_array($tpl['option_arr']['bf_include_name'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_name']; ?></label>
			<input type="text" name="customer_name" class="SBScript_Field SBScript_FieldText<?php echo $tpl['option_arr']['bf_include_name'] == 3 ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['customer_name']) ? htmlspecialchars($_POST['customer_name']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_name']); ?>" />
		</p>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_email'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_email']; ?></label>
			<input type="text" name="customer_email" class="SBScript_Field SBScript_Email SBScript_FieldText<?php echo $tpl['option_arr']['bf_include_email'] == 3 ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['customer_email']) ? htmlspecialchars($_POST['customer_email']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_email']); ?>" />
		</p>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_phone'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_phone']; ?></label>
			<input type="text" name="customer_phone" class="SBScript_Field SBScript_FieldText<?php echo $tpl['option_arr']['bf_include_phone'] == 3 ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['customer_phone']) ? htmlspecialchars($_POST['customer_phone']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_phone']); ?>" />
		</p>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_country'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_country']; ?></label>
			<select name="customer_country" class="SBScript_Field SBScript_FieldSelect<?php echo $tpl['option_arr']['bf_include_country'] == 3 ? ' SBScript_Required' : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_country']); ?>">
				<option value="">---</option>
				<?php
				if (isset($tpl['country_arr']) && is_array($tpl['country_arr']))
				{
					foreach ($tpl['country_arr'] as $v)
					{
						?><option value="<?php echo $v['id']; ?>"<?php echo isset($_POST['customer_country']) && $_POST['customer_country'] == $v['id'] ? ' selected="selected"' : NULL; ?>><?php echo stripslashes($v['country_title']); ?></option><?php
					}
				}
				?>
			</select>
		</p>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_city'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_city']; ?></label>
			<input type="text" name="customer_city" class="SBScript_Field SBScript_FieldText<?php echo $tpl['option_arr']['bf_include_city'] == 3 ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['customer_city']) ? htmlspecialchars($_POST['customer_city']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_city']); ?>" />
		</p>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_state'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_state']; ?></label>
			<input type="text" name="customer_state" class="SBScript_Field SBScript_FieldText<?php echo $tpl['option_arr']['bf_include_state'] == 3 ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['customer_state']) ? htmlspecialchars($_POST['customer_state']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_state']); ?>" />
		</p>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_zip'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_zip']; ?></label>
			<input type="text" name="customer_zip" class="SBScript_Field SBScript_FieldText<?php echo $tpl['option_arr']['bf_include_zip'] == 3 ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['customer_zip']) ? htmlspecialchars($_POST['customer_zip']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_zip']); ?>" />
		</p>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_address'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_address']; ?></label>
			<input type="text" name="customer_address" class="SBScript_Field SBScript_FieldText<?php echo $tpl['option_arr']['bf_include_address'] == 3 ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['customer_address']) ? htmlspecialchars($_POST['customer_address']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_address']); ?>" />
		</p>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_notes'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_notes']; ?></label>
			<textarea name="customer_notes" class="SBScript_Field SBScript_FieldTextarea<?php echo $tpl['option_arr']['bf_include_notes'] == 3 ? ' SBScript_Required' : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_notes']); ?>"><?php echo isset($_POST['customer_notes']) ? htmlspecialchars($_POST['customer_notes']) : NULL; ?></textarea>
		</p>
		<?php
	}
	if ($tpl['option_arr']['payment_disable'] == 'No' && isset($tpl['amount']) && $tpl['amount']['deposit'] > 0 && $tpl['option_arr']['hide_prices'] == 'No')
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_payment_method']; ?></label>
			<select name="payment_method" class="SBScript_Field SBScript_FieldSelect SBScript_Required" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_payment_method']); ?>">
				<option value="">---</option>
				<?php
				foreach ($SBS_LANG['booking_payment_methods'] as $k => $v)
				{
					if (@$tpl['option_arr']['payment_enable_' . $k] == 'Yes')
					{
						?><option value="<?php echo $k; ?>"<?php echo isset($_POST['payment_method']) && $_POST['payment_method'] == $k ? ' selected="selected"' : NULL; ?>><?php echo stripslashes($v); ?></option><?php
					}
				}
				?>
			</select>
		</p>
		<div class="SBScript_CCData" style="display: <?php echo isset($_POST['payment_method']) && $_POST['payment_method'] == 'creditcard' ? 'block' : 'none'; ?>">
			<p>
				<label class="title"><?php echo $SBS_LANG['front']['bf_cc_type']; ?></label>
				<select name="cc_type" class="SBScript_Field SBScript_FieldSelect<?php echo isset($_POST['payment_method']) && $_POST['payment_method'] == 'creditcard' ? ' SBScript_Required' : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_cc_type']); ?>">
					<option value="">---</option>
					<?php
					foreach ($SBS_LANG['front']['bf_cc_types'] as $k => $v)
					{
						if (isset($_POST['cc_type']) && $_POST['cc_type'] == $k)
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
				<label class="title"><?php echo $SBS_LANG['front']['bf_cc_num']; ?></label>
				<input type="text" name="cc_num" class="SBScript_Field SBScript_FieldText<?php echo isset($_POST['payment_method']) && $_POST['payment_method'] == 'creditcard' ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['cc_num']) ? htmlspecialchars($_POST['cc_num']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_cc_num']); ?>" />
			</p>
			<p>
				<label class="title"><?php echo $SBS_LANG['front']['bf_cc_exp']; ?></label>
				<select name="cc_exp_month" class="SBScript_Field SBScript_FieldSelectMedium<?php echo isset($_POST['payment_method']) && $_POST['payment_method'] == 'creditcard' ? ' SBScript_Required' : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_cc_exp_month']); ?>">
					<option value="">---</option>
					<?php
					foreach ($SBS_LANG['month_name'] as $key => $val)
					{
						?><option value="<?php echo $key;?>" <?php echo isset($_POST['cc_exp_month']) && $_POST['cc_exp_month'] == $key ? 'selected="selected"' : NULL;?> ><?php echo $val;?></option><?php
					}
					?>
				</select>
				<select name="cc_exp_year" class="SBScript_Field SBScript_FieldSelectMedium<?php echo isset($_POST['payment_method']) && $_POST['payment_method'] == 'creditcard' ? ' SBScript_Required' : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_cc_exp_year']); ?>">
					<option value="">---</option>
					<?php
					$y = (int) date('Y');
					for ($i = $y; $i <= $y + 10; $i++)
					{
						?><option value="<?php echo $i; ?>"<?php echo isset($_POST['cc_exp_year']) && (int) $_POST['cc_exp_year'] == $i ? ' selected="selected"' : NULL; ?>><?php echo $i; ?></option><?php
					}
					?>
				</select>
			</p>
			<p>
				<label class="title"><?php echo $SBS_LANG['front']['bf_cc_code']; ?></label>
				<input type="text" name="cc_code" class="SBScript_Field SBScript_FieldTextSmall<?php echo isset($_POST['payment_method']) && $_POST['payment_method'] == 'creditcard' ? ' SBScript_Required' : NULL; ?>" value="<?php echo isset($_POST['cc_code']) ? htmlspecialchars($_POST['cc_code']) : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_cc_code']); ?>" />
			</p>
		</div>
		<?php
	}
	if (in_array($tpl['option_arr']['bf_include_captcha'], array(2, 3)))
	{
		?>
		<p>
			<label class="title"><?php echo $SBS_LANG['front']['bf_captcha']; ?></label>
			<span class="SBScript_PseudoField">
				<img src="<?php echo INSTALL_URL; ?>index.php?controller=Front&amp;action=captcha&amp;<?php echo rand(1, 999999); ?>" alt="Captcha" />
				<input type="text" name="captcha" maxlength="6" class="SBScript_Field SBScript_FieldTextSmall<?php echo $tpl['option_arr']['bf_include_captcha'] == 3 ? ' SBScript_Required' : NULL; ?>" rev="<?php echo htmlspecialchars($SBS_LANG['front']['v_captcha']); ?>" />
			</span>
		</p>
		<?php
	}
	?>
	<p class="SBScript_Error" style="display: none"></p>
	<p>
		<label class="title">&nbsp;</label>
		<input type="button" value="<?php echo htmlspecialchars($SBS_LANG['front']['button_submit']); ?>" name="EBCalBookingFormSubmit" class="SBScript_Event_Button SBScript_Button" />
		<input type="button" value="<?php echo htmlspecialchars($SBS_LANG['front']['button_cancel']); ?>" name="EBCalBookingFormCancel" class="SBScript_Event_Button SBScript_Button" />
	</p>
</form>

	</div>	
	<div class="SBScript_Bottom">
		<div class="SBScript_Bottom_Left"></div>
		<div class="SBScript_Bottom_Right"></div>
	</div>
</div>