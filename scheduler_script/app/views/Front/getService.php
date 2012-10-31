<div class="StivaApp_Box_Top StivaApp_B10">
	<div class="StivaApp_Box_Top_Left"></div>
	<div class="StivaApp_Box_Top_Right"></div>
	<span class="StivaApp_Box_Top_Point">3</span>
	<?php echo $SBS_LANG['front']['step_3']; ?>
</div>
<?php
$step = $tpl['option_arr']['step'] * 60;
$service_length = $tpl['service_arr']['total'] * 60;
foreach ($tpl['arr'] as $v)
{
	?>
	<div class="SBScript_Holder">
		<div class="SBScript_Top">
			<div class="SBScript_Top_Left"></div>
			<div class="SBScript_Top_Right"></div>
			<?php echo stripslashes($v['name']); ?>
		</div>
		<div class="SBScript_Middle SBScript_Font">
		<?php 
		foreach (range($tpl['t_arr']['start_ts'], $tpl['t_arr']['end_ts'] - $step, $step) as $i)
		{
			$is_free = true;
			$class = "t-available";
			foreach ($v['booking_arr'] as $item)
			{
				if ($i >= $item['start_ts'] && $i < $item['start_ts'] + $item['total'] * 60)
				{
					$is_free = false;
					$class = "t-booked";
					break;
				}
			}
			if ($is_free)
			{
				foreach ($v['booking_arr'] as $item)
				{
					//if ($i + $service_length > $item['start_ts'] && $i + $service_length <= $item['start_ts'] + $item['total'] * 60)
					if ($i + $service_length > $item['start_ts'] && $i <= $item['start_ts'])
					{
						$class = "t-unavailable";
						break;
					}
				}
				if ($i + $service_length > $tpl['t_arr']['end_ts'])
				{
					$class = "t-unavailable";
				}
				if (isset($_SESSION[$controller->cartName]) && 
					isset($_SESSION[$controller->cartName][$_GET['cid']]) && 
					isset($_SESSION[$controller->cartName][$_GET['cid']][$_GET['date']]) &&
					isset($_SESSION[$controller->cartName][$_GET['cid']][$_GET['date']][$_GET['service_id']]) 
				)
				{
					foreach ($_SESSION[$controller->cartName][$_GET['cid']][$_GET['date']][$_GET['service_id']] as $time => $employee_id)
					{
						list($start_ts, $end_ts) = explode("|", $time);
						if ($i == $start_ts && $v['employee_id'] == $employee_id)
						{
							$class = "t-available t-selected";
							break;
						}
					}
				}
			}
			?><span class="t-block <?php echo $class; ?>" rel="<?php echo $i; ?>|<?php echo $i + $service_length; ?>|<?php echo $v['service_id']; ?>|<?php echo $v['employee_id']; ?>"><?php echo date($tpl['option_arr']['time_format'], $i); ?></span><?php
		}
		?>
		</div>
		<div class="SBScript_Bottom">
			<div class="SBScript_Bottom_Left"></div>
			<div class="SBScript_Bottom_Right"></div>
		</div>
	</div>
	<?php
}
?>