<?php 
$content = urldecode($_POST['cspost']);
$myfile = fopen("message.fmc", "w");
$strvalue = $content;
fwrite($myfile, $strvalue);
fclose($myfile);
?>