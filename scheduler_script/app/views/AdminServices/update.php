<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.AdminServices
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
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1"><?php echo $SBS_LANG['service_update']; ?></a></li>
		</ul>
		<div id="tabs-1">
		
			<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminServices&amp;action=update&amp;id=<?php echo $tpl['arr']['id']; ?>" method="post" id="frmCreateService" class="ebc-form">
				<input type="hidden" name="service_update" value="1" />
				<input type="hidden" name="id" value="<?php echo $tpl['arr']['id']; ?>" />
				
				<p>
					<label class="title"><?php echo $SBS_LANG['service_title']; ?>:</label>
					<input type="text" name="name" id="name" class="text w500 required" value="<?php echo htmlspecialchars(stripslashes($tpl['arr']['name'])); ?>" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_description']; ?>:</label>
					<textarea name="description" id="description" class="text w600 h150"><?php echo stripslashes($tpl['arr']['description']); ?></textarea>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_price']; ?>:</label>
					<input type="text" name="price" id="price" class="text w80 required number align_right" value="<?php echo floatval($tpl['arr']['price']); ?>" /> <?php echo $tpl['option_arr']['currency']; ?>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_length']; ?>:</label>
					<input type="text" name="length" id="length" class="text w50 required digits align_right" value="<?php echo intval($tpl['arr']['length']); ?>" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_before']; ?>:</label>
					<input type="text" name="before" id="before" class="text w50 align_right digits" value="<?php echo intval($tpl['arr']['before']); ?>" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_after']; ?>:</label>
					<input type="text" name="after" id="after" class="text w50 align_right digits" value="<?php echo intval($tpl['arr']['after']); ?>" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_total']; ?>:</label>
					<input type="text" name="total" id="total" class="text w50 align_right digits" readonly="readonly" value="<?php echo intval($tpl['arr']['total']); ?>" />
				</p>
				<p>
					<label class="title">&nbsp;</label>
					<input type="submit" value="" class="button button_save" />
				</p>
				
			</form>
		</div>
	</div>
	<?php
}
?>