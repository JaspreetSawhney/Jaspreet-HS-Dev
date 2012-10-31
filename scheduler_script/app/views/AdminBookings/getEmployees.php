<select name="employee_id" id="employee_id" class="select w300">
	<option value=""><?php echo $SBS_LANG['booking_choose']; ?></option>
	<?php 
	foreach ($tpl['employee_arr'] as $v)
	{
		?><option value="<?php echo $v['id']; ?>"<?php echo isset($_GET['employee_id']) && $_GET['employee_id'] == $v['id'] ? ' selected="selected"' : NULL; ?>><?php echo stripslashes($v['name']); ?></option><?php
	}
	?>
</select>