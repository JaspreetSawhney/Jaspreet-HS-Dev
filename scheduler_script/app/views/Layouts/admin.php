<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.Layouts
 */
?>
<!doctype html>
<html>
	<head>
		<title>Hairslayer Appointment Scheduler</title>
		<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
		<?php
		foreach ($controller->css as $css)
		{
			echo '<link type="text/css" rel="stylesheet" href="'.(isset($css['remote']) && $css['remote'] ? NULL : BASE_PATH).$css['path'].htmlspecialchars($css['file']).'" />';
		}
		foreach ($controller->js as $js)
		{
			echo '<script type="text/javascript" src="'.(isset($js['remote']) && $js['remote'] ? NULL : BASE_PATH).$js['path'].htmlspecialchars($js['file']).'"></script>';
		}
		?>
	</head>
	<body>
	
		<div id="container">
    		<div id="ebc-header">
				<?php
				if ($controller->isMultiCalendar())
				{
					?>
					<!-- <select name="_calendar_id" id="ebc-calendar-id" class="select w200">
						<option value=""><?php echo $SBS_LANG['menu_choose_calendar']; ?></option>
					<?php
					if (isset($tpl['_calendar_arr']))
					{
						foreach ($tpl['_calendar_arr'] as $v)
						{
							if (isset($_SESSION[$controller->default_user]['calendar_id']) && $_SESSION[$controller->default_user]['calendar_id'] == $v['id'])
							{
								?><option value="<?php echo $v['id']; ?>" selected="selected"><?php echo stripslashes($v['calendar_title']); ?></option><?php
							} else {
								?><option value="<?php echo $v['id']; ?>"><?php echo stripslashes($v['calendar_title']); ?></option><?php
							}
						}
					}
					?>
					</select> -->
					<?php
				}
				?>
			</div>
			
			<div class="ebc-page">
				<div class="ebc-page-top"></div>
				<div class="ebc-page-middle">
					<?php include_once VIEWS_PATH . 'Layouts/elements/leftmenu.php'; ?>
					<div id="content">

					<?php require $content_tpl; ?>

					</div> <!-- content -->
					<div class="clear_both"></div>
				</div> <!-- ebc-page-middle -->
				<div class="ebc-page-bottom"></div>
			</div> <!-- ebc-page -->
			
			<div id="ebc-footer">
		   	Copyright &copy; <?php echo date("Y"); ?>
        	</div>
		
		</div> <!-- container -->
	</body>
</html>