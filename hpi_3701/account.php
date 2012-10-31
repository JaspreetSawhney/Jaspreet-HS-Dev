<?php 
if (isset($_POST["username"]) && isset($_POST["password"])) {
	$email = $_POST["username"];
	$pass = $_POST["password"];
	 $mysqli = new MySQLi("10.80.65.195", "hslayer_admin", "diM_3281@99", "hairslayer");
	 
	 $mysqli->query("sp_Login($email,$pass)");
	}
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
</body>
</html>