<?php 
if (isset($tpl['arr']) && count($tpl['arr']) > 0)
{
	foreach ($tpl['arr'] as $employee => $booking_arr)
	{
		list($e_id, $e_name) = explode(":^:", $employee);
		?>
		<table class="ebc-table b10" cellspacing="0" cellpadding="0">
		<thead>
			<tr>
				<th class="sub" colspan="4"><?php echo stripslashes($e_name); ?></th>
			</tr>
		</thead>
			<tbody>
			<?php
			foreach ($booking_arr as $k => $booking)
			{
				?>
				<tr class="pointer <?php echo $k % 2 === 0 ? 'even' : 'odd'; ?> {id: <?php echo $booking['booking_id']; ?>}">
					<td style="width: 14%"><?php echo date($tpl['option_arr']['time_format'], $booking['start_ts']); ?> - <?php echo date($tpl['option_arr']['time_format'], $booking['start_ts'] + $booking['total'] * 60); ?></td>
					<td style="width: auto"><?php echo stripslashes($booking['service_name']); ?></td>
					<td style="width: 20%"><?php echo stripslashes($booking['customer_name']); ?></td>
					<td style="width: 4%" class="align_center"><acronym title="<?php echo $booking['booking_status']; ?>" class="booking-status booking-status-<?php echo $booking['booking_status']; ?>"><?php echo @$SBS_LANG['booking_booking_statuses'][$booking['booking_status']]; ?></acronym></td>
				</tr>
				<?php
			}
			?>
			</tbody>
		</table>
		<?php
	}
} else {
	Util::printNotice($SBS_LANG['booking_empty']);
}
?>