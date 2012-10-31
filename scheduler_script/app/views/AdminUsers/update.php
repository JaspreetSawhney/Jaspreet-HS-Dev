<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.AdminUsers
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
			<li><a href="#tabs-1"><?php echo $SBS_LANG['user_update']; ?></a></li>
		</ul>
		<div id="tabs-1">

			<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminUsers&amp;action=update&amp;id=<?php echo $tpl['arr']['id']; ?>" method="post" id="frmUpdateUser" class="ebc-form">
				<input type="hidden" name="user_update" value="1" />
				<input type="hidden" name="id" value="<?php echo $tpl['arr']['id']; ?>" />
				<p><label class="title"><?php echo $SBS_LANG['user_role']; ?></label>
					<select name="role_id" id="role_id" class="select w150 required">
					<?php
					foreach ($tpl['role_arr'] as $v)
					{
						if ($tpl['arr']['role_id'] == $v['id'])
						{
							?><option value="<?php echo $v['id']; ?>" selected="selected"><?php echo stripslashes($v['role']); ?></option><?php
						} else {
							?><option value="<?php echo $v['id']; ?>"><?php echo stripslashes($v['role']); ?></option><?php
						}
					}
					?>
					</select>
				</p>
				<p><label class="title"><?php echo $SBS_LANG['user_username']; ?></label><input type="text" name="username" id="username" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['username'])); ?>" class="text w150 required" /></p>
				<p><label class="title"><?php echo $SBS_LANG['user_password']; ?></label><input type="password" name="password" id="password" value="password" class="text w150 required" /></p>
				<p><label class="title"><?php echo $SBS_LANG['user_status']; ?></label>
					<select name="status" id="status" class="select w150">
					<?php
					foreach ($SBS_LANG['user_statarr'] as $k => $v)
					{
						if ($k == $tpl['arr']['status'])
						{
							echo '<option value="'.$k.'" selected="selected">'.$v.'</option>';
						} else {
							echo '<option value="'.$k.'">'.$v.'</option>';
						}
					}
					?>
					</select>
				</p>
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