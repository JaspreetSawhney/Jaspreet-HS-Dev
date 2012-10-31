<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.AdminEmployees
 */
if (isset($tpl['status']))
{
	switch ($tpl['status'])
	{
		case 1:
			?><p class="status_err"><span>&nbsp;</span><?php echo $SBS_LANG['status'][1]; ?></p><?php
			break;
		case 2:
			?><p class="status_err"><span>&nbsp;</span><?php echo $SBS_LANG['status'][2]; ?></p><?php
			break;
		case 9:
			Util::printNotice($SBS_LANG['status'][9]);
			break;
	}
} else {
	?>
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1"><?php echo $SBS_LANG['employee_update']; ?></a></li>
		</ul>
		<div id="tabs-1">

			<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminEmployees&amp;action=update&amp;id=<?php echo $tpl['arr']['id']; ?>" method="post" id="frmUpdateEmployee" class="ebc-form">
				<input type="hidden" name="employee_update" value="1" />
				<input type="hidden" name="id" value="<?php echo $tpl['arr']['id']; ?>" />
				<p><label class="title"><?php echo $SBS_LANG['employee_name']; ?></label><input type="text" name="name" id="name" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['name'])); ?>" class="text w200 required" /></p>
				<p><label class="title"><?php echo $SBS_LANG['employee_email']; ?></label><input type="text" name="email" id="email" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['email'])); ?>" class="text w200 email" />
					<input type="checkbox" name="send_email" id="send_email" value="1"<?php echo $tpl['arr']['send_email'] == 1 ? ' checked="checked"' : NULL; ?> /> <label for="send_email"><?php echo $SBS_LANG['employee_send_email']; ?></label>
				</p>
				<p><label class="title"><?php echo $SBS_LANG['employee_phone']; ?></label><input type="text" name="phone" id="phone" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['phone'])); ?>" class="text w200" /></p>
				<p><label class="title"><?php echo $SBS_LANG['employee_notes']; ?></label><textarea name="notes" id="notes" class="textarea w500 h150"><?php echo htmlspecialchars(stripslashes($tpl['arr']['notes'])); ?></textarea></p>
				<div class="m10 overflow">
					<label class="title"><?php echo $SBS_LANG['employee_services']; ?></label>
					<div class="float_left">
				<?php 
				foreach ($tpl['service_arr'] as $service)
				{
					?>
					<input type="checkbox" name="service_id[]" id="service_id_<?php echo $service['id']; ?>" value="<?php echo $service['id']; ?>"<?php echo in_array($service['id'], $tpl['employee_service_arr']) ? ' checked="checked"' : NULL; ?> />
					<label for="service_id_<?php echo $service['id']; ?>"><?php echo stripslashes($service['name']); ?></label>
					<br />
					<?php
				}
				?>
					</div>
				</div>
				<p>
					<label class="title">&nbsp;</label>
					<input type="submit" value="" class="button button_save" />
				</p>
			</form>
		</div> <!-- tabs-1 -->
	</div> <!-- tabs -->
	<?php
}
?>