<?php
$userIP = $_POST['userIP'];
$userName = urldecode($_POST['userName']);
$onlineuservalue = '['.$userIP.'] '.$userName."\n";
$b = file_get_contents("OnlineUserList.fmc");
if(strpos($b, $onlineuservalue) !== false) {
}
else {
$userfile = fopen("OnlineUserList.fmc", "a");
fwrite($userfile, $onlineuservalue);
}
fclose($userfile);
?>