<?php
$filename = 'message.fmc';
$userOnlineList = 'OnlineUserList.fmc';
while (true) 
{
	if(filesize($filename)>3000)
	{
		copy("message.fmc","FMCLastMsglog.fmc");
		unlink($filename);
		$myfile = fopen("message.fmc", "w");		
	}
	sleep(120);
	unlink($userOnlineList);
	echo "即将执行清理";
}
?>