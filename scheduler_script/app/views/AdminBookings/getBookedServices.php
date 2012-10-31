<table class="ebc-table" cellspacing="0" cellpadding="0">
	<thead>
		<tr>
			<th class="sub"><?php echo $SBS_LANG['booking_date']; ?></th>
			<th class="sub"><?php echo $SBS_LANG['booking_start']; ?></th>
			<th class="sub"><?php echo $SBS_LANG['booking_service']; ?></th>
			<th class="sub"><?php echo $SBS_LANG['booking_employee']; ?></th>
			<th class="sub"><?php echo $SBS_LANG['booking_price']; ?></th>
			<th class="sub" style="width: 8%">&nbsp;</th>
		</tr>
	</thead>
	<tbody>
	<?php 
	foreach ($tpl['booking_detail_arr'] as $booking)
	{
		?>
		<tr>
			<td><?php echo date($tpl['option_arr']['date_format'], strtotime($booking['date'])); ?></td>
			<td><?php echo date($tpl['option_arr']['time_format'], $booking['start_ts']); ?></td>
			<td><?php echo stripslashes($booking['service_name']); ?></td>
			<td><?php echo stripslashes($booking['employee_name']); ?></td>
			<td><?php echo Util::formatCurrencySign(number_format($booking['price'], 2), $tpl['option_arr']['currency']); ?></td>
			<td><a class="icon icon-del" rel="<?php echo $booking['id']; ?>" rev="<?php echo $booking['booking_id']; ?>" href="<?php echo $_SERVER['PHP_SELF']; ?>index.php?controller=AdminBookings"><?php echo $SBS_LANG['_delete']; ?></a></td>
		</tr>
		<?php
	}
	?>
	</tbody>
</table>