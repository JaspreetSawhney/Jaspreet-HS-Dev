<?php
/**
 * @package tsbc
 * @subpackage tsbc.app.views.AdminTime
 */
if (isset($tpl['status']))
{
	switch ($tpl['status'])
	{
		case 1:
			Util::printNotice($SBS_LANG['status'][1]);
			break;
		case 2:
			Util::printNotice($SBS_LANG['status'][2]);
			break;
	}
} else {
	?>
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1"><?php echo $SBS_LANG['time_update']; ?></a></li>
		</ul>
		<div id="tabs-1">
			<?php include_once VIEWS_PATH . 'Helpers/time.widget.php'; ?>
			<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminTime&amp;action=index" method="post" class="ebc-form" id="frmTimeCustom">
				<input type="hidden" name="custom_time" value="1" />
				<p>
					<label class="title"><?php echo $SBS_LANG['time_date']; ?></label>
					<input type="text" name="date" id="date" class="text w100 pointer required" readonly="readonly" value="<?php echo date($tpl['option_arr']['date_format'], strtotime($tpl['arr']['date'])); ?>" />
				</p>
				<?php 
				list($sh, $sm,) = explode(":", $tpl['arr']['start_time']);
				list($eh, $em,) = explode(":", $tpl['arr']['end_time']);
				?>
				<p>
					<label class="title"><?php echo $SBS_LANG['time_from']; ?></label>
					<?php hourWidget((int) $sh, 'start_hour', 'start_hour', 'select w60'); ?>
					<?php minuteWidget((int) $sm, 'start_minute', 'start_minute', 'select w60'); ?>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['time_to']; ?></label>
					<?php hourWidget((int) $eh, 'end_hour', 'end_hour', 'select w60'); ?>
					<?php minuteWidget((int) $em, 'end_minute', 'end_minute', 'select w60'); ?>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['time_is']; ?></label>
					<span class="left"><input type="checkbox" name="is_dayoff" id="is_dayoff" value="T"<?php echo $tpl['arr']['is_dayoff'] == 'T' ? ' checked="checked"' : NULL; ?> /></span>
				</p>
				<p>
					<input type="submit" value="" class="button button_save"  />
				</p>
			</form>
		
		</div>
	</div>
	<?php
}
?>