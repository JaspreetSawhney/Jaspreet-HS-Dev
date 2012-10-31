<div class="StivaApp_Box_Top StivaApp_B10">
	<div class="StivaApp_Box_Top_Left"></div>
	<div class="StivaApp_Box_Top_Right"></div>
	<span class="StivaApp_Box_Top_Point">2</span>
	<?php echo $SBS_LANG['front']['step_2']; ?>
</div>
<?php
if (isset($tpl['isPastDate']))
{
	// Past date
	?>
	<div class="SBScript_Holder">
		<div class="SBScript_Top">
			<div class="SBScript_Top_Left"></div>
			<div class="SBScript_Top_Right"></div>
			<?php echo $_GET['date']; ?>
		</div>
		<div class="SBScript_Middle">
			<span class="SBScript_Font"><?php echo $SBS_LANG['front']['services']['pastdate']; ?></span>
		</div>
		<div class="SBScript_Bottom">
			<div class="SBScript_Bottom_Left"></div>
			<div class="SBScript_Bottom_Right"></div>
		</div>
	</div>
	<?php	
} else {
	if (!isset($tpl['dayoff']))
	{
		if (isset($tpl['service_arr']) && count($tpl['service_arr']) > 0)
		{
			foreach ($tpl['service_arr'] as $k => $v)
			{
				?>
				<div class="SBScript_Holder">
					<div class="SBScript_Top">
						<div class="SBScript_Top_Left"></div>
						<div class="SBScript_Top_Right"></div>
						<?php echo stripslashes($v['name']); ?>
					</div>
					<div class="SBScript_Middle">
						<div class="SBScript_Event_Left">
							<div class="SBScript_Event SBScript_Event_Description"><?php echo stripslashes(nl2br($v['description'])); ?></div>
						</div>
						<div class="SBScript_Event_Right">
							<div class="SBScript_Event SBScript_Event_Book">
								<input type="button" rev="<?php echo $v['id']; ?>" class="SBScript_Event SBScript_Event_Button SBScript_Button" value="<?php echo $SBS_LANG['front']['services']['availability']; ?>" />
							</div>
						</div>
						<div class="SBScript_Clear_Both SBScript_Overflow SBScript_Price_Length">
							<div class="SBScript_Event_Detail"><span><?php echo $SBS_LANG['front']['services']['price']; ?>:</span> <?php echo Util::formatCurrencySign(number_format($v['price'], 2), $tpl['option_arr']['currency']); ?></div>
							<div class="SBScript_Event_Detail"><span><?php echo $SBS_LANG['front']['services']['length']; ?>:</span> <?php echo $v['length']; ?> <?php echo $SBS_LANG['front']['services']['minutes']; ?></div>
						</div>
					</div>
					<div class="SBScript_Bottom">
						<div class="SBScript_Bottom_Left"></div>
						<div class="SBScript_Bottom_Right"></div>
					</div>
				</div>
				<?php
			}

		} else {
			?>
			<div class="SBScript_Holder">
				<div class="SBScript_Top">
					<div class="SBScript_Top_Left"></div>
					<div class="SBScript_Top_Right"></div>
					<?php echo $_GET['date']; ?>
				</div>
				<div class="SBScript_Middle">
					<span class="SBScript_Font"><?php echo $SBS_LANG['front']['event_empty']; ?></span>
				</div>
				<div class="SBScript_Bottom">
					<div class="SBScript_Bottom_Left"></div>
					<div class="SBScript_Bottom_Right"></div>
				</div>
			</div>
			<?php
		}
	} else {
		# Date/day is off
		?>
		<div class="SBScript_Holder">
			<div class="SBScript_Top">
				<div class="SBScript_Top_Left"></div>
				<div class="SBScript_Top_Right"></div>
				<?php echo $_GET['date']; ?>
			</div>
			<div class="SBScript_Middle">
				<span class="SBScript_Font"><?php echo $SBS_LANG['booking_dayoff']; ?></span>
			</div>
			<div class="SBScript_Bottom">
				<div class="SBScript_Bottom_Left"></div>
				<div class="SBScript_Bottom_Right"></div>
			</div>
		</div>
		<?php
	}
}
?>