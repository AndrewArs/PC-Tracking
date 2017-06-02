<!html>
<header>
	<title>PC Tracking</title>
	<meta charset = "utf-8">
</header>
<body>

<?php
$db_driver = "mysql";
$host = "localhost";
$database = "id1718001_logdb";
$dsn = "$db_driver:host=$host; dbname=$database";

$username = "id1718001_admin";
$password = "admin";
$options = array(PDO::ATTR_PERSISTENT => true, PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES utf8');

try {
	$dbh = new PDO ($dsn, $username, $password, $options);
	if(isset($_GET["hwid"]))
	{
		$sth = $dbh->prepare("SELECT Date, Operation FROM LogDB WHERE HWID = ?");
		    if ($sth->execute(array($_GET["hwid"]))) 
			{
				while ($row = $sth->fetch()) 
				{
					echo $row['Date'] . '  ---  ' . $row['Operation'] . '<br/>';
				}
			}
	}
	else
	{
		$sth = $dbh->prepare("SELECT Date, Operation, HWID FROM LogDB");
		    if ($sth->execute()) 
			{
				while ($row = $sth->fetch()) 
				{
					echo $row['Date'] . '  ---  ' . $row['Operation'] . '  ---  from machine '. $row['HWID'] . '<br/>';
				}
			}
    }
	}	catch (PDOException $e) {
        echo "Error!: " . $e->getMessage() . "<br/>";
        die();
}
?>

</body>