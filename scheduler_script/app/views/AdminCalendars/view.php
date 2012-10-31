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
			case 10:
				Util::printNotice($SBS_LANG['calendar_err'][10]);
				break;
		}
	}
}
?>