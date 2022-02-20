<?php
$userIP = $_POST['userIP'];
$userName = urldecode($_POST['userName']);
$onlineuservalue = '['.$userIP.'] '.$userName."\n";
$userfile = fopen("OnlineUserList.fmc", "r");
if (strstr($userfile,$onlineuservalue)!=true) 
{
fclose($userfile);
$userfile = fopen("OnlineUserList.fmc", "a");
fwrite($userfile, $onlineuservalue);
}
fclose($userfile);
?>