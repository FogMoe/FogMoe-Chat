<?php
$filename = 'message.fmc';
while (filesize($filename)>3000) 
{
copy("message.fmc","FMCLastMsglog.fmc");
unlink($filename);
$myfile = fopen("message.fmc", "w");
sleep(120);
echo "����ִ�����������¼";
}
?>