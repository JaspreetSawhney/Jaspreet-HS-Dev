<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.Layouts.elements
 */
?>
<ul class="ebc-menu">
	<?php
	if ($controller->isMultiCalendar())
	{
		?>
		<!-- <li><a class="<?php //echo $_GET['controller'] == 'AdminCalendars' ? 'ebc-menu-focus' : NULL; ?>" href="<?php //echo $_SERVER['PHP_SELF']; ?>?controller=AdminCalendars"><span class="ebc-menu-calendar">&nbsp;</span><?php //echo $SBS_LANG['menu_calendar']; ?></a></li> -->
		<?php
	}
	?>
	<li><a class="<?php echo $_GET['controller'] == 'AdminBookings' ? 'ebc-menu-focus' : NULL; ?>" href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminBookings&amp;action=schedule"><span class="ebc-menu-bookings">&nbsp;</span><?php echo $SBS_LANG['menu_bookings']; ?></a></li>
	<?php
	//if ($controller->isAdmin())
	if (true)
	{
		?>
		<!--<li><a class="<?php //echo $_GET['controller'] == 'AdminServices' ? 'ebc-menu-focus' : NULL; ?>" href="<?php //echo $_SERVER['PHP_SELF']; ?>?controller=AdminServices"><span class="ebc-menu-services">&nbsp;</span><?php //echo $SBS_LANG['menu_services']; ?></a></li>
		 <li><a class="<?php //echo $_GET['controller'] == 'AdminEmployees' ? 'ebc-menu-focus' : NULL; ?>" href="<?php //echo $_SERVER['PHP_SELF']; ?>?controller=AdminEmployees"><span class="ebc-menu-employees">&nbsp;</span><?php //echo $SBS_LANG['menu_employees']; ?></a></li> -->
		<li><a class="<?php echo $_GET['controller'] == 'AdminTime' ? 'ebc-menu-focus' : NULL; ?>" href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminTime"><span class="ebc-menu-time">&nbsp;</span><?php echo $SBS_LANG['menu_time']; ?></a></li>
		<?php
		if ($controller->isAdmin() && $controller->isMultiUser())
		{
			?> <!-- <li><a class="<?php echo $_GET['controller'] == 'AdminUsers' ? 'ebc-menu-focus' : NULL; ?>" href="<?php echo $_SERVER['PHP_SELF']; ?>?controller=AdminUsers"><span class="ebc-menu-users">&nbsp;</span><?php echo $SBS_LANG['menu_users']; ?></a></li> --><?php
		}
	}
	?>
	<!-- <li><a class="<?php //echo $_GET['controller'] == 'AdminOptions' && $_GET['action'] == 'index' ? 'ebc-menu-focus' : NULL; ?>" href="<?php //echo $_SERVER['PHP_SELF']; ?>?controller=AdminOptions"><span class="ebc-menu-options">&nbsp;</span><?php //echo $SBS_LANG['menu_options']; ?></a></li>  -->
	<!-- <li><a class="<?php //echo $_GET['controller'] == 'AdminOptions' && $_GET['action'] == 'install' ? 'ebc-menu-focus' : NULL; ?>" href="<?php //echo $_SERVER['PHP_SELF']; ?>?controller=AdminOptions&amp;action=install"><span class="ebc-menu-install">&nbsp;</span><?php //echo $SBS_LANG['menu_install']; ?></a></li> -->
	<?php
	if ((int) $controller->getCalendarId() > 0)
	{
		?><li><a href="preview.php?cid=<?php //echo $controller->getCalendarId(); ?>" target="_blank"><span class="ebc-menu-preview">&nbsp;</span><?php echo $SBS_LANG['menu_preview']; ?></a></li><?php
	}
	?>
	<!-- <li><a href="<?php //echo $_SERVER['PHP_SELF']; ?>?controller=Admin&amp;action=logout"><span class="ebc-menu-logout">&nbsp;</span><?php //echo $SBS_LANG['menu_logout']; ?></a></li> -->
</ul>