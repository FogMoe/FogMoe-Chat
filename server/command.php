<?php 
//初始化接受数据
date_default_timezone_set(PRC);
$content = urldecode($_POST['cspost']);
$userName = urldecode($_POST['userName']);
$dateNow = date("m/d G:i:s");
//消息记录
$myfile = fopen("message.fmc", "a");
$strvalue = $dateNow.' （'.$userName.'）'." 发送：".$content."\n";
fwrite($myfile, $strvalue);
fclose($myfile);
?>