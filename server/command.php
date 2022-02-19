<?php 
$content = urldecode($_POST['cspost']);
$userName = urldecode($_POST['userName']);
$myfile = fopen("message.fmc", "w");
$strvalue = '（'.$userName.'）'." 发送： ".$content;
fwrite($myfile, $strvalue);
fclose($myfile);
?>