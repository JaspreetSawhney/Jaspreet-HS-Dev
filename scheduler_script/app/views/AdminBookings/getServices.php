<select name="service_id" id="service_id" class="select w300">
	<option value=""><?php echo $SBS_LANG['booking_choose']; ?></option>
	<?php 
	foreach ($tpl['service_arr'] as $v)
	{
		?><option value="<?php echo $v['id']; ?>"<?php echo isset($_GET['service_id']) && $_GET['service_id'] == $v['id'] ? ' selected="selected"' : NULL; ?>><?php echo stripslashes($v['name']); ?></option><?php
	}
	?>
</select>