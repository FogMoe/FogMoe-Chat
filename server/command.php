<?php 
$content = urldecode($_POST['cspost']);
$userName = urldecode($_POST['userName']);
$myfile = fopen("message.fmc", "a");
$strvalue = '（'.$userName.'）'." 发送： ".$content."\n";
fwrite($myfile, $strvalue);
fclose($myfile);
?>