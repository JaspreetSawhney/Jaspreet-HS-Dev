<?php 
//$opts['username'] = $_GET['lgr1'];   //$_GET['lgr2']
echo "got in good!";
$con = mysql_connect("65.23.153.35","ash_user","deLari99");
if (!$con)
  {
  die('Could not connect: ' . mysql_error());
  }
  
 mysql_select_db("a_sched", $con);
$logg_good = false;
$r = mysql_query("SELECT * FROM service_booking_users WHERE username = '".$_GET['lgr1']."' AND password = '".$_GET['lgr2']."'");
echo "<p>SELECT * FROM service_booking_users WHERE username = '".$_GET['lgr1']."' AND password = '".$_GET['lgr2']."'</p>";
//if (mysql_num_rows($r) == 1)
while ($row = mysql_fetch_array($r))
{
	$_SESSION[default_user] = $row['username'];
	$_SESSION[default_user]['calendar_id'] = $row['id'];
	$_SESSION[default_user]['role_id'] = 2;
	$_SESSION[default_user]['id'] = $row['id'];
	$logg_good = true;

	$dd = date("Y-m-d H:i:s");
	$res = mysql_query("UPDATE service_booking_users SET last_login = '$dd'");
	echo "Got In!";
}

if ($logg_good)
{
	header("Location: index.php?controller=AdminBookings&action=schedule");
}
else
{
	echo "Not as planned: Failed Login";
	//header("Location: index.php?controller=Admin&action=login&err=1");
}
mysql_close($con);
?>

<html>
<head>
<title>Untitled Document</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body>

</body>
</html>
