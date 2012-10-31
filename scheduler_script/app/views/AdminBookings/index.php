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
	if (isset($_GET['err']))
	{
		switch ($_GET['err'])
		{
			case 1:
				Util::printNotice($SBS_LANG['booking_err'][1]);
				break;
			case 2:
				Util::printNotice($SBS_LANG['booking_err'][2]);
				break;
			case 3:
				Util::printNotice($SBS_LANG['booking_err'][3]);
				break;
			case 4:
				Util::printNotice($SBS_LANG['booking_err'][4]);
				break;
			case 5:
				Util::printNotice($SBS_LANG['booking_err'][5]);
				break;
			case 7:
				Util::printNotice($SBS_LANG['status'][7]);
				break;
			case 8:
				Util::printNotice($SBS_LANG['booking_err'][8]);
				break;
			case 9:
				Util::printNotice($SBS_LANG['booking_err'][9]);
				break;
		}
	}
	$week_start = 1;
	$jqDateFormat = Util::jqDateFormat($tpl['option_arr']['date_format']);
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
			<li><a href="#tabs-1"><?php echo $SBS_LANG['booking_schedule']; ?></a></li>
			<li><a href="#tabs-2"><?php echo $SBS_LANG['booking_list']; ?></a></li>
			<li><a href="#tabs-3"><?php echo $SBS_LANG['booking_create']; ?></a></li>
			<li><a href="#tabs-4"><?php echo $SBS_LANG['booking_find']; ?></a></li>
			<li><a href="#tabs-5"><?php echo $SBS_LANG['booking_export']; ?></a></li>
		</ul>
		<div id="tabs-1">
		</div>
		<div id="tabs-2">
		<?php
		if (isset($tpl['arr']))
		{
			if (is_array($tpl['arr']))
			{
				$count = count($tpl['arr']);
				if ($count > 0)
				{
					?>
					<table class="ebc-table" cellpadding="0" cellspacing="0">
						<thead>
							<tr>
								<th class="sub"><?php echo $SBS_LANG['booking_time']; ?></th>
								<th class="sub"><?php echo $SBS_LANG['booking_service']; ?></th>
								<th class="sub"><?php echo $SBS_LANG['booking_customer_email']; ?></th>
								<th class="sub"><?php echo $SBS_LANG['booking_booking_status']; ?></th>
								<th class="sub"><?php echo $SBS_LANG['booking_total']; ?></th>
								<th class="sub" style="width: 20px"></th>
								<th class="sub" style="width: 8%"></th>
								<th class="sub" style="width: 8%"></th>
							</tr>
						</thead>
						<tbody>
					<?php
					for ($i = 0; $i < $count; $i++)
					{
						?>
						<tr class="<?php echo $i % 2 === 0 ? 'even' : 'odd'; ?>">
							<td><?php
							$b_ = array(); 
							foreach ($tpl['arr'][$i]['b_arr'] as $item)
							{
								$b_[] = date($tpl['option_arr']['datetime_format'], $item['start_ts']);
							}
							if (count($b_) > 0)
							{
								echo join("<br />", $b_);
							}
							?></td>
							<td><?php
							$b_ = array(); 
							foreach ($tpl['arr'][$i]['b_arr'] as $item)
							{
								$b_[] = $item['service_name'];
							}
							if (count($b_) > 0)
							{
								echo join("<br />", array_map('stripslashes', $b_));
							}
							?></td>
							<td><?php echo stripslashes($tpl['arr'][$i]['customer_email']); ?></td>
							<td class="align_center"><acronym title="<?php echo $tpl['arr'][$i]['booking_status']; ?>" class="booking-status booking-status-<?php echo $tpl['arr'][$i]['booking_status']; ?>"><?php echo @$SBS_LANG['booking_booking_statuses'][$tpl['arr'][$i]['booking_status']]; ?></acronym></td>
							<td class="align_right"><?php echo Util::formatCurrencySign(number_format($tpl['arr'][$i]['booking_total'], 2), $tpl['option_arr']['currency']); ?></td>
							<td><a class="icon icon-email" rel="<?php echo $tpl['arr'][$i]['id']; ?>" href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminBookings&amp;action=update&amp;id=<?php echo $tpl['arr'][$i]['id']; ?>"></a></td>
							<td><a class="icon icon-edit" href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminBookings&amp;action=update&amp;id=<?php echo $tpl['arr'][$i]['id']; ?>"><?php echo $SBS_LANG['_edit']; ?></a></td>
							<td><a class="icon icon-delete" rel="<?php echo $tpl['arr'][$i]['id']; ?>" rev="<?php echo $controller->getCalendarId(); ?>" href="<?php echo  $_SERVER['PHP_SELF']; ?>?controller=AdminBookings&amp;action=delete&amp;id=<?php echo $tpl['arr'][$i]['id']; ?>"><?php echo $SBS_LANG['_delete']; ?></a></td>
						</tr>
						<?php
					}
					?>
						</tbody>
					</table>
					<?php
					if (!$controller->isAjax())
					{
						?>
						<div id="dialogDelete" title="<?php echo htmlspecialchars($SBS_LANG['booking_del_title']); ?>" style="display:none">
							<p><?php echo $SBS_LANG['booking_del_body']; ?></p>
						</div>
						<div id="dialogEmail" title="<?php echo htmlspecialchars($SBS_LANG['booking_email_title']); ?>" style="display:none">
							
						</div>
						<?php
					}

					if (isset($tpl['paginator']))
					{
						?>
						<ul class="ebc-paginator">
						<?php
						for ($i = 1; $i <= $tpl['paginator']['pages']; $i++)
						{
							$qs = "&amp;q=".(isset($_GET['q']) ? urlencode($_GET['q']) : NULL)."&amp;service_id=".(isset($_GET['service_id']) && (int) $_GET['service_id'] > 0 ? intval($_GET['service_id']) : NULL)."&amp;employee_id=".(isset($_GET['employee_id']) && (int) $_GET['employee_id'] > 0 ? intval($_GET['employee_id']) : NULL)."&amp;start_date=".(isset($_GET['start_date']) ? urlencode($_GET['start_date']) : NULL)."&amp;end_date=".(isset($_GET['end_date']) ? urlencode($_GET['end_date']) : NULL);
							if ((isset($_GET['page']) && (int) $_GET['page'] == $i) || (!isset($_GET['page']) && $i == 1))
							{
								?><li><a href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=<?php echo $_GET['controller']; ?>&amp;action=index<?php echo $qs ?>&amp;page=<?php echo $i; ?>" class="focus"><?php echo $i; ?></a></li><?php
							} else {
								?><li><a href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=<?php echo $_GET['controller']; ?>&amp;action=index<?php echo $qs ?>&amp;page=<?php echo $i; ?>"><?php echo $i; ?></a></li><?php
							}
						}
						?>
						</ul>
						<?php
					}
					
				} else {
					Util::printNotice($SBS_LANG['booking_empty']);
				}
			}
		}
		?>
		</div>
		<div id="tabs-3">
			<?php if (!$controller->isAjax()) : ?>
			<script type="text/javascript" src="index.php?controller=Front&action=load&cid=<?php echo $controller->getCalendarId(); ?>"></script>
			<?php endif; ?>
		</div>
		<div id="tabs-4">
		
			<form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="get" class="ebc-form" id="frmFindBooking">
				<input type="hidden" name="controller" value="AdminBookings" />
				<input type="hidden" name="action" value="index" />
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_q']; ?></label>
					<input type="text" name="q" id="q" class="text w300" value="<?php echo isset($_GET['q']) ? htmlspecialchars($_GET['q']) : NULL; ?>" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_service']; ?>:</label>
					<span id="boxService">
					<select name="service_id" id="service_id" class="select w300">
						<option value=""><?php echo $SBS_LANG['booking_choose']; ?></option>
						<?php
						foreach ($tpl['service_arr'] as $v)
						{
							?><option value="<?php echo $v['id']; ?>"<?php echo isset($_GET['service_id']) && $_GET['service_id'] == $v['id'] ? ' selected="selected"' : NULL; ?>"><?php echo stripslashes(htmlspecialchars($v['name'])); ?></option><?php
						}
						?>
					</select>
					</span>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_employee']; ?>:</label>
					<span id="boxEmployee">
					<select name="employee_id" id="employee_id" class="select w300">
						<option value=""><?php echo $SBS_LANG['booking_choose']; ?></option>
						<?php
						foreach ($tpl['employee_arr'] as $v)
						{
							?><option value="<?php echo $v['id']; ?>"<?php echo isset($_GET['employee_id']) && $_GET['employee_id'] == $v['id'] ? ' selected="selected"' : NULL; ?>"><?php echo stripslashes(htmlspecialchars($v['name'])); ?></option><?php
						}
						?>
					</select>
					</span>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_from']; ?></label>
					<input type="text" name="start_date" id="start_date" class="text w80 pointer datepick disabled" value="<?php echo isset($_GET['start_date']) ? htmlspecialchars($_GET['start_date']) : NULL; ?>" rel="<?php echo $week_start; ?>" rev="<?php echo $jqDateFormat; ?>" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_to']; ?></label>
					<input type="text" name="end_date" id="end_date" class="text w80 pointer datepick disabled" value="<?php echo isset($_GET['end_date']) ? htmlspecialchars($_GET['end_date']) : NULL; ?>" rel="<?php echo $week_start; ?>" rev="<?php echo $jqDateFormat; ?>" />
				</p>
				<p>
					<label class="title">&nbsp;</label>
					<input type="submit" value="" class="button button_find" />
				</p>
			</form>
		</div>
		<div id="tabs-5">
			<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminBookings&amp;action=export" method="post" class="ebc-form" id="frmExportBooking">
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_export_format']; ?></label>
					<select name="format" id="format" class="select w100">
						<option value="csv">CSV</option>
						<option value="xml">XML</option>
						<option value="icalendar">iCalendar</option>
					</select>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_export_from']; ?></label>
					<input type="text" name="date_from" id="date_from" class="text w80 datepick pointer disabled" rel="<?php echo $week_start; ?>" rev="<?php echo $jqDateFormat; ?>" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['booking_export_to']; ?></label>
					<input type="text" name="date_to" id="date_to" class="text w80 datepick pointer disabled" rel="<?php echo $week_start; ?>" rev="<?php echo $jqDateFormat; ?>" />
				</p>
				<p>
					<label class="title">&nbsp;</label>
					<input type="submit" value="" class="button button_save" />
				</p>				
			</form>
			<?php 
			Util::printNotice($SBS_LANG['booking_export_note_csv'], false);
			Util::printNotice($SBS_LANG['booking_export_note_xml'], false);
			Util::printNotice($SBS_LANG['booking_export_note_ical'], false);
			?>
		</div>
	</div>
	<script type="text/javascript">
	$(function () {
		<?php $tab_id = isset($_GET['tab_id']) && (int) $_GET['tab_id'] > 0 ? (int) $_GET['tab_id'] : 1; ?>
		$("#tabs").tabs("option", "selected", <?php echo $tab_id; ?>);
	});
	</script>
	<?php
}
?>