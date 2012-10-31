<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.AdminCalendars
 */
if (isset($tpl['status']))
{
	switch ($tpl['status'])
	{
		case 1:
			?><p class="status_err"><?php echo $SBS_LANG['status'][1]; ?></p><?php
			break;
		case 2:
			?><p class="status_err"><?php echo $SBS_LANG['status'][2]; ?></p><?php
			break;
		case 8:
			Util::printNotice($SBS_LANG['status'][8]);
			break;
	}
} else {
	include VIEWS_PATH . 'AdminCalendars/index.php';
}
?>