<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminTime&amp;action=index" method="post" class="tsbc-form">
	<input type="hidden" name="working_time" value="1" />
	<table class="ebc-table" cellpadding="0" cellspacing="0">
		<thead>
			<tr>
				<th colspan="7"><?php echo $SBS_LANG['time_title']; ?></th>
			</tr>
			<tr>
				<th class="sub"><?php echo $SBS_LANG['time_day']; ?></th>
				<th class="sub"><?php echo $SBS_LANG['time_from']; ?></th>
				<th class="sub"><?php echo $SBS_LANG['time_to']; ?></th>
				<th class="sub"><?php echo $SBS_LANG['time_is']; ?></th>
			</tr>
		</thead>
		<tbody>
		<?php
		include_once VIEWS_PATH . 'Helpers/time.widget.php';
		$i = 1;
		foreach ($SBS_LANG['days'] as $k => $day)
		{
			if (isset($tpl['arr']) && count($tpl['arr']) > 0)
			{
				$hour_from = substr($tpl['arr'][$k.'_from'], 0, 2);
				$hour_to = substr($tpl['arr'][$k.'_to'], 0, 2);
				$minute_from = substr($tpl['arr'][$k.'_from'], 3, 2);
				$minute_to = substr($tpl['arr'][$k.'_to'], 3, 2);
				$attr = array();
				$checked = NULL;
				if (is_null($tpl['arr'][$k.'_from']))
				{
					$attr['disabled'] = 'disabled';
					$checked = ' checked="checked"';
				}
			} else {
				$hour_from = NULL;
				$hour_to = NULL;
				$minute_from = NULL;
				$minute_to = NULL;
				$attr = array();
				$checked = NULL;
			}
			$step = 5;
			?>
			<tr class="<?php echo ($i % 2 !== 0 ? 'odd' : 'even'); ?>">
				<td><?php echo $day; ?></td>
				<td><?php echo hourWidget($hour_from, $k . '_hour_from', $k . '_hour_from', 'select w60', $attr); ?> <?php echo minuteWidget($minute_from, $k . '_minute_from', $k . '_minute_from', 'select w60', $attr, $step); ?></td>
				<td><?php echo hourWidget($hour_to, $k . '_hour_to', $k . '_hour_to', 'select w60', $attr); ?> <?php echo minuteWidget($minute_to, $k . '_minute_to', $k . '_minute_to', 'select w60', $attr, $step); ?></td>
				<td><input type="checkbox" class="working_day" name="<?php echo $k; ?>_dayoff" value="T"<?php echo $checked; ?> /></td>
			</tr>
			<?php
			$i++;
		}
		?>
		</tbody>
		<tfoot>
			<tr>
				<td colspan="7"><input type="submit" value="" class="button button_save" /></td>
			</tr>
		</tfoot>
	</table>
</form>