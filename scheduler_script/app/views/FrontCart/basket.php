<?php
if (isset($tpl['cart_arr']))
{
	if (count($tpl['cart_arr']) > 0 && array_key_exists($_GET['cid'], $tpl['cart_arr']) && count($tpl['cart_arr'][$_GET['cid']]) > 0)
	{
		ob_start();
		$total = 0;
		foreach ($tpl['cart_arr'] as $cid => $date_arr)
		{
			if ($cid != $_GET['cid'])
			{
				continue;
			}
			foreach ($date_arr as $date => $service_arr)
			{
				foreach ($service_arr as $service_id => $time_arr)
				{
					foreach ($time_arr as $time => $employee_id)
					{
						list($start_ts, $end_ts) = explode("|", $time);
						$sd = date("Y-m-d", $start_ts);
						$_date = $date == $sd ? $date : $sd;							
						?>
						<tr>
							<td colspan="5" class="SBScript_Bold" style="width: auto"><?php echo stripslashes($tpl['service_arr'][$service_id]['name']); ?></td>
						</tr>
						<tr class="StivaApp_Basket_Row">
							<td class="StivaApp_Basket_El"><?php echo date($tpl['option_arr']['date_format'], strtotime($_date)); ?></td>
							<td class="StivaApp_Basket_El"><?php echo date($tpl['option_arr']['time_format'], $start_ts); ?></td>
							<td class="StivaApp_Basket_El"><?php echo date($tpl['option_arr']['time_format'], $end_ts); ?></td>
							<td class="StivaApp_Basket_El"><?php echo $tpl['option_arr']['hide_prices'] == 'No' && (float) @$tpl['service_arr'][$service_id]['price'] > 0 ? Util::formatCurrencySign(number_format(@$tpl['service_arr'][$service_id]['price'], 2, '.', ','), $tpl['option_arr']['currency']) : NULL; ?></td>
							<td class="SBScript_Center"><input type="checkbox" name="service[<?php echo $date; ?>][<?php echo $service_id; ?>][<?php echo $start_ts; ?>]" value="<?php echo $end_ts; ?>" checked="checked" class="SBScript_Slot" rel="<?php echo $date; ?>|<?php echo $service_id; ?>" /></td>
						</tr>
						<?php
						$total += @$tpl['service_arr'][$service_id]['price'];
					}
				}
			}
		}
		$content = ob_get_contents();
		ob_end_clean();
		?>
		<form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="get" name="SBScript_Form_Cart">
			<table class="SBScript_Table SBScript_Font StivaApp_Basket_Table">
				<tbody>
					<tr>
						<td style="width: 100%"></td>
						<td style="width: 35px"></td>
						<td style="width: 35px"></td>
						<td style="width: 45px"></td>
						<td style="width: 25px"></td>
					</tr>
				<?php echo $content; ?>
				</tbody>
				<?php if ($tpl['option_arr']['hide_prices'] == 'No' && $total > 0) : ?>
				<tfoot>
					<tr>
						<td colspan="3" class="SBScript_Bold SBScript_Right"><?php echo $SBS_LANG['front']['cart']['total']; ?>:</td>
						<td class="SBScript_Bold"><?php echo Util::formatCurrencySign(number_format($total, 2, '.', ','), $tpl['option_arr']['currency']); ?></td>
						<td>&nbsp;</td>
					</tr>
				</tfoot>
				<?php endif; ?>
			</table>
		</form>
		<?php
		if (isset($_GET['checkout']) && $_GET['checkout'] == 1)
		{
			?><div style="text-align: right"><input type="button" value="<?php echo htmlspecialchars($SBS_LANG['front']['services']['proceed']); ?>" class="SBScript_Event SBScript_Event_Button SBScript_Button SBScript_Button_Proceed" style="margin: 0 8px 4px 0" /></div><?php	
		}
	} else {
		?><div class="SBScript_Font StivaApp_P10"><?php echo $SBS_LANG['front']['cart']['empty']; ?></div><?php
	}
}
?>