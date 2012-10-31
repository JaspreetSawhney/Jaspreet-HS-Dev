<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.Layouts
 */
?>
<!doctype html>
<html>
	<head>
		<title>STIVA Appointment Scheduler | Login</title>
		<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
		<?php
		foreach ($controller->css as $css)
		{
			echo '<link type="text/css" rel="stylesheet" href="'.(isset($css['remote']) && $css['remote'] ? NULL : BASE_PATH).$css['path'].$css['file'].'" />';
		}
		
		foreach ($controller->js as $js)
		{
			echo '<script type="text/javascript" src="'.(isset($js['remote']) && $js['remote'] ? NULL : BASE_PATH).$js['path'].$js['file'].'"></script>';
		}
		?>
	</head>
	<body>
		<div id="container">
			<div id="wrapper">
				<div id="login-content">
				<?php require $content_tpl; ?>
				</div>
			</div> <!-- wrapper -->
		</div> <!-- container -->
	</body>
</html>