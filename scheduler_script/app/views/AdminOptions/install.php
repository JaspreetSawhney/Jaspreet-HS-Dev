<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.AdminOptions
 */
if (isset($tpl['status']))
{
	switch ($tpl['status'])
	{
		case 1:
			?><p class="status_err"><span>&nbsp;</span><?php echo $SBS_LANG['status'][1]; ?></p><?php
			break;
		case 2:
			?><p class="status_err"><span>&nbsp;</span><?php echo $SBS_LANG['status'][2]; ?></p><?php
			break;
	}
} else {
	?>
    <form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post" class="ebc-form">
	<div id="tabs">
		<ul>
			<li><a href="#tabs-1"><?php echo $SBS_LANG['option_install']; ?></a></li>
            <?php
            if($tpl['isAdmin']){
			?>
            <li><a href="#tabs-2"><?php echo $SBS_LANG['option_user_api']; ?></a></li>
            <?php
			}
			?>
		</ul>
		
		<div id="tabs-1">
					<p><span class="bold block b10"><?php echo $SBS_LANG['o_install']['js'][1]; ?></span>
					<textarea class="textarea textarea-install w600 h150 overflow">
&lt;link href="<?php echo INSTALL_URL; ?>index.php?controller=Front&amp;action=loadCss&amp;cid=<?php echo $controller->getCalendarId(); ?>" type="text/css" rel="stylesheet" /&gt;
&lt;script type="text/javascript" src="<?php echo INSTALL_URL; ?>index.php?controller=Front&amp;action=loadJs"&gt;&lt;/script&gt;
</textarea></p>

					<p><span class="bold block b10"><?php echo $SBS_LANG['o_install']['js'][2]; ?></span>
					<textarea class="textarea textarea-install w600 h80 overflow">
&lt;script type="text/javascript" src="<?php echo INSTALL_FOLDER; ?>index.php?controller=Front&amp;action=load&amp;cid=<?php echo $controller->getCalendarId(); ?>"&gt;&lt;/script&gt;</textarea></p>

		</div> <!-- tabs-1 -->
         <?php
		if($tpl['isAdmin']){
		?>
        <div id="tabs-2">
        	<p><span class="bold block b10"><?php echo $SBS_LANG['o_install']['user_api']; ?></span>
			<textarea class="textarea textarea-install w600 h350 overflow" style="text-align:left;">
require_once "<?php echo INSTALL_PATH; ?>user_api.php";

//add data to array
$data = array();
$data['username'] = "username";
$data['password'] = "password";
$data['calendar_name'] = "calendar name";
$data['status'] = "T"; //T or F

//call function to create user and calendar
$result = create_user($data);

//result equal 1: Calendar and user added successfully
//result equal 2: Username already exists
//result equal 3: Process error. Can't create user. 
//result equal 4: Process error. Can't create calendat. 

			</textarea>
            </p>
        </div>
        <?php
		}
		?>

	</div>
	<?php
	if (isset($_GET['tab_id']) && !empty($_GET['tab_id']))
	{
		$tab_id = explode("-", $_GET['tab_id']);
		$tab_id = (int) $tab_id[1] - 1;
		//$tab_id = (int) $_GET['tab_id'] - 1;
		$tab_id = $tab_id < 0 ? 0 : $tab_id;
		?>
		<script type="text/javascript">
		(function ($) {
			$(function () {
				$("#tabs").tabs("option", "selected", <?php echo $tab_id; ?>);
			});
		})(jQuery);
		</script>
		<?php
	}
}
?>