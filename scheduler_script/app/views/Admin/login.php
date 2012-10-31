<?php
/**
 * @package ebc
 * @subpackage ebc.app.views.Admin
 */
?>
<a href="http://www.phpjabbers.com/appointment-scheduler/" id="login-image" target="_blank"><img src="<?php echo BASE_PATH . IMG_PATH; ?>logo-login.png" alt="Appointment Scheduler" /></a>

<h3><?php echo $SBS_LANG['login_login']; ?></h3>

<div class="login-box">
<div id="test"></div>
	<div class="login-top"></div>
	<div class="login-middle">
		<form action="<?php echo $_SERVER['PHP_SELF']; ?>?controller=Admin&amp;action=login" method="post" id="frmLoginAdmin" class="ebc-form">
			<input type="hidden" name="login_user" value="1" />
			<p><label class="title"><?php echo $SBS_LANG['login_username']; ?>:</label><input name="login_username" type="text" class="text" value="<?php $_GET['lrg1']; ?>" id="login_username" /></p>
			<p><label class="title"><?php echo $SBS_LANG['login_password']; ?>:</label><input name="login_password" type="password" class="text" value="<?php $_GET['lrg2']; ?>" id="login_password" /></p>
			<p><label class="title">&nbsp;</label><input type="submit" value="" id="btn_login" class="button button_login" /></p>
			<?php
			if (isset($_GET['err']))
			{
				switch ($_GET['err'])
				{
					case 1:
						?><p><label class="title"><?php echo $SBS_LANG['login_error']; ?>:</label><span class="left"><?php echo $SBS_LANG['login_err'][1]; ?></span></p><?php
						break;
					case 2:
						?><p><label class="title"><?php echo $SBS_LANG['login_error']; ?>:</label><span class="left"><?php echo $SBS_LANG['login_err'][2]; ?></span></p><?php
						break;
					case 3:
						?><p><label class="title"><?php echo $SBS_LANG['login_error']; ?>:</label><span class="left"><?php echo $SBS_LANG['login_err'][3]; ?></span></p><?php
						break;
				}
			}
			?>
		</form>
	</div>
	<div class="login-bottom"></div>
</div>
<script language="Javascript">
	var lrg1 = getQuerystring('lrg1');
	if(lrg1 != "")
	{
	document.getElementById('login_username').value = lrg1;
	document.getElementById('login_password').value = getQuerystring('lrg2');
	document.forms["frmLoginAdmin"].submit();
	}
	
	function getQuerystring(key, default_)
{
  if (default_==null) default_="";
  key = key.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
  var regex = new RegExp("[\\?&]"+key+"=([^&#]*)");
  var qs = regex.exec(window.location.href);
  if(qs == null)
    return default_;
  else
    return qs[1];
}
</script>
<div class="align_right t10">
Copyright &copy; <?php echo date("Y"); ?>
</div>