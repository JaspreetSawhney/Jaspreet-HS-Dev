<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.AdminEmployees
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
		case 9:
			Util::printNotice($SBS_LANG['status'][9]);
			break;
	}
} else {
	if (isset($_GET['err']))
	{
		switch ($_GET['err'])
		{
			case 0:
				Util::printNotice($SBS_LANG['employee_err'][0]);
				break;
			case 1:
				Util::printNotice($SBS_LANG['employee_err'][1]);
				break;
			case 2:
				Util::printNotice($SBS_LANG['employee_err'][2]);
				break;
			case 3:
				Util::printNotice($SBS_LANG['employee_err'][3]);
				break;
			case 4:
				Util::printNotice($SBS_LANG['employee_err'][4]);
				break;
			case 5:
				Util::printNotice($SBS_LANG['employee_err'][5]);
				break;
			case 7:
				Util::printNotice($SBS_LANG['status'][7]);
				break;
			case 8:
				Util::printNotice($SBS_LANG['employee_err'][8]);
				break;
		}
	}
	?>
	
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1"><?php echo $SBS_LANG['employee_list']; ?></a></li>
			<li><a href="#tabs-2"><?php echo $SBS_LANG['employee_create']; ?></a></li>
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
								<th class="sub"><?php echo $SBS_LANG['employee_name']; ?></th>
								<th class="sub"><?php echo $SBS_LANG['employee_email']; ?></th>
								<th class="sub"><?php echo $SBS_LANG['employee_phone']; ?></th>
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
							<td><?php echo stripslashes($tpl['arr'][$i]['email']); ?></td>
							<td><?php echo stripslashes($tpl['arr'][$i]['phone']); ?></td>
							<td><a class="icon icon-edit" href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminEmployees&amp;action=update&amp;id=<?php echo $tpl['arr'][$i]['id']; ?>"><?php echo $SBS_LANG['_edit']; ?></a></td>
							<td><a class="icon icon-delete" rel="<?php echo $tpl['arr'][$i]['id']; ?>" href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminEmployees&amp;action=delete&amp;id=<?php echo $tpl['arr'][$i]['id']; ?>"><?php echo $SBS_LANG['_delete']; ?></a></td>
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
						<div id="dialogDelete" title="<?php echo htmlspecialchars($SBS_LANG['employee_del_title']); ?>" style="display:none">
							<p><?php echo $SBS_LANG['employee_del_body']; ?></p>
						</div>
						<?php
					}
				} else {
					Util::printNotice($SBS_LANG['employee_empty']);
				}
			}
		}
		?>
		</div> <!-- tabs-1 -->
		<div id="tabs-2">
			<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminEmployees&amp;action=create" method="post" id="frmCreateEmployee" class="ebc-form">
				<input type="hidden" name="employee_create" value="1" />
				<p><label class="title"><?php echo $SBS_LANG['employee_name']; ?></label><input type="text" name="name" id="name" class="text w200 required" /></p>
				<p><label class="title"><?php echo $SBS_LANG['employee_email']; ?></label><input type="text" name="email" id="email" class="text w200 email" />
				<input type="checkbox" name="send_email" id="send_email" value="1" /> <label for="send_email"><?php echo $SBS_LANG['employee_send_email']; ?></label>
				</p>
				<p><label class="title"><?php echo $SBS_LANG['employee_phone']; ?></label><input type="text" name="phone" id="phone" class="text w200" /></p>
				<p><label class="title"><?php echo $SBS_LANG['employee_notes']; ?></label><textarea name="notes" id="notes" class="textarea w500 h150"></textarea></p>
				<div class="m10 overflow">
					<label class="title"><?php echo $SBS_LANG['employee_services']; ?></label>
					<div class="float_left">
				<?php 
				foreach ($tpl['service_arr'] as $service)
				{
					?>
					<input type="checkbox" name="service_id[]" id="service_id_<?php echo $service['id']; ?>" value="<?php echo $service['id']; ?>" />
					<label for="service_id_<?php echo $service['id']; ?>"><?php echo stripslashes($service['name']); ?></label>
					<br />
					<?php
				}
				?>
					</div>
				</div>
				<p>
					<label class="title">&nbsp;</label>
					<input type="submit" value="" class="button button_save" />
				</p>
			</form>
		</div> <!-- tabs-2 -->
	</div>
	<?php
}
?>