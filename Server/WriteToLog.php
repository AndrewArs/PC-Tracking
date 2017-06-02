<?php

$db_driver = "mysql";
$host = "localhost";
$database = "id1718001_logdb";
$dsn = "$db_driver:host=$host; dbname=$database";

$username = "id1718001_admin";
$password = "admin";
$options = array(PDO::ATTR_PERSISTENT => true, PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES utf8');

if (isset($_POST['date']) && isset($_POST['operation']) && isset($_POST['hwid'])) {
    try {
        $dbh = new PDO ($dsn, $username, $password, $options);
        $sth = $dbh->prepare("INSERT INTO `LogDB`(`Date`, `Operation`, `HWID`) VALUES (?, ?, ?)");
        $sth->execute(array($_POST['date'], $_POST['operation'], $_POST['hwid']));
    } catch (PDOException $e) {
        echo "Error!: " . $e->getMessage() . "<br/>";
        die();
    }
}