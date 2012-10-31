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
				Util::printNotice($SBS_LANG['service_err'][1]);
				break;
			case 2:
				Util::printNotice($SBS_LANG['service_err'][2]);
				break;
			case 3:
				Util::printNotice($SBS_LANG['service_err'][3]);
				break;
			case 4:
				Util::printNotice($SBS_LANG['service_err'][4]);
				break;
			case 5:
				Util::printNotice($SBS_LANG['service_err'][5]);
				break;
			case 7:
				Util::printNotice($SBS_LANG['status'][7]);
				break;
			case 8:
				Util::printNotice($SBS_LANG['service_err'][8]);
				break;
		}
	}
	?>
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1"><?php echo $SBS_LANG['service_list']; ?></a></li>
			<!-- <li><a href="#tabs-2"><?php echo $SBS_LANG['service_create']; ?></a></li> -->
		</ul>
		<div id="tabs-1">
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
								<th class="sub"><?php echo $SBS_LANG['service_title']; ?></th>
								<th class="sub align_right"><?php echo $SBS_LANG['service_price']; ?></th>
								<th class="sub align_right"><?php echo $SBS_LANG['service_length']; ?></th>
								<th class="sub align_right"><?php echo $SBS_LANG['service_total']; ?></th>
								<th class="sub" style="width: 10%"></th>
								<th class="sub" style="width: 10%"></th>
							</tr>
						</thead>
						<tbody>
					<?php
					for ($i = 0; $i < $count; $i++)
					{
						?>
						<tr class="<?php echo $i % 2 === 0 ? 'even' : 'odd'; ?>">
							<td><?php echo stripslashes($tpl['arr'][$i]['name']); ?></td>
							<td class="align_right"><?php echo Util::formatCurrencySign(number_format($tpl['arr'][$i]['price'], 2), $tpl['option_arr']['currency']); ?></td>
							<td class="align_right"><?php echo intval($tpl['arr'][$i]['length']); ?></td>
							<td class="align_right"><?php echo intval($tpl['arr'][$i]['total']); ?></td>
							<td><a class="icon icon-edit" href="<?php echo  $_SERVER['PHP_SELF']; ?>?controller=AdminServices&amp;action=update&amp;id=<?php echo $tpl['arr'][$i]['id']; ?>"><?php echo $SBS_LANG['_edit']; ?></a></td>
							<td><a class="icon icon-delete" rel="<?php echo $tpl['arr'][$i]['id']; ?>" href="<?php echo  $_SERVER['PHP_SELF']; ?>?controller=AdminServices&amp;action=delete&amp;id=<?php echo $tpl['arr'][$i]['id']; ?>"><?php echo $SBS_LANG['_delete']; ?></a></td>
						</tr>
						<?php
					}
					?>
						</tbody>
					</table>
					<?php
					if (isset($tpl['paginator']))
					{
						?>
						<ul class="ebc-paginator">
						<?php
						for ($i = 1; $i <= $tpl['paginator']['pages']; $i++)
						{
							if ((isset($_GET['page']) && (int) $_GET['page'] == $i) || (!isset($_GET['page']) && $i == 1))
							{
								?><li><a href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=<?php echo $_GET['controller']; ?>&amp;action=index&amp;page=<?php echo $i; ?>" class="focus"><?php echo $i; ?></a></li><?php
							} else {
								?><li><a href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=<?php echo $_GET['controller']; ?>&amp;action=index&amp;page=<?php echo $i; ?>"><?php echo $i; ?></a></li><?php
							}
						}
						?>
						</ul>
						<?php
					}
					
					if (!$controller->isAjax())
					{
						?>
						<div id="dialogDelete" title="<?php echo htmlspecialchars($SBS_LANG['service_del_title']); ?>" style="display:none">
							<p><?php echo $SBS_LANG['service_del_body']; ?></p>
						</div>
						<?php
					}
				} else {
					Util::printNotice($SBS_LANG['service_empty']);
				}
			}
		}
		?>
		</div>
		<div id="tabs-2">
			<!-- <form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminServices&amp;action=create" method="post" id="frmCreateService" class="ebc-form">
				<input type="hidden" name="service_create" value="1" />
				<p>
					<label class="title"><?php echo $SBS_LANG['service_title']; ?>:</label>
					<input type="text" name="name" id="name" class="text w400 required" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_description']; ?>:</label>
					<textarea name="description" id="description" class="text w600 h150"></textarea>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_price']; ?>:</label>
					<input type="text" name="price" id="price" class="text w80 required number align_right" /> <?php echo $tpl['option_arr']['currency']; ?>
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_length']; ?>:</label>
					<input type="text" name="length" id="length" class="text w50 required align_right digits" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_before']; ?>:</label>
					<input type="text" name="before" id="before" class="text w50 align_right digits" value="0" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_after']; ?>:</label>
					<input type="text" name="after" id="after" class="text w50 align_right digits" value="0" />
				</p>
				<p>
					<label class="title"><?php echo $SBS_LANG['service_total']; ?>:</label>
					<input type="text" name="total" id="total" class="text w50 align_right digits" value="0" readonly="readonly" />
				</p>
				<p>
					<label class="title">&nbsp;</label>
					<input type="submit" value="" class="button button_save" />
				</p>
			</form> -->
		</div>
	</div>
	<?php
}
?>