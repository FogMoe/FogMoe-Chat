<?php
$userIP = $_POST['userIP'];
$userName = urldecode($_POST['userName']);
$onlineuservalue = '['.$userIP.'] '.$userName."\n";
if( strpos(file_get_contents("./OnlineUserList.fmc"),$_GET[$onlineuservalue]) !== true) {
$userfile = fopen("OnlineUserList.fmc", "a");
fwrite($userfile, $onlineuservalue);
fclose($userfile);
}
?>