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
			Util::printNotice($SBS_LANG['status'][1]);
			break;
		case 2:
			Util::printNotice($SBS_LANG['status'][2]);
			break;
	}
} else {
	$week_start = 1;
	$jqDateFormat = Util::jqDateFormat($tpl['option_arr']['date_format']);
	?>
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1"><?php echo $SBS_LANG['booking_schedule']; ?></a></li>
			<li><a href="#tabs-2"><?php echo $SBS_LANG['booking_list']; ?></a></li>
			<li><a href="#tabs-3"><?php echo $SBS_LANG['booking_create']; ?></a></li>
			<li><a href="#tabs-4"><?php echo $SBS_LANG['booking_find']; ?></a></li>
			<li><a href="#tabs-5"><?php echo $SBS_LANG['booking_export']; ?></a></li>
		</ul>
		<div id="tabs-1">
			<div class="float_left" id="date" rel="<?php echo $week_start; ?>|<?php echo $jqDateFormat; ?>|<?php echo isset($_GET['date']) && !empty($_GET['date']) ? $_GET['date'] : date($tpl['option_arr']['date_format']); ?>"></div>
			<div class="float_left l5" style="width: 535px" id="boxSchedule">
			<?php include VIEWS_PATH . 'AdminBookings/getSchedule.php'; ?>
			</div>
		</div>
		<div id="tabs-2"></div>
		<div id="tabs-3"></div>
		<div id="tabs-4"></div>
		<div id="tabs-5"></div>
	</div>
	<script type="text/javascript">
	$(function () {
		$("#tabs").bind("tabsselect", function (event, ui) {
			if (ui.index > 0) {
				window.location.href = 'index.php?controller=AdminBookings&action=index&tab_id=' + ui.index;
			}			
		});
	});
	</script>
	<?php
}
?>