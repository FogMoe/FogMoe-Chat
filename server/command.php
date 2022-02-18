<?php 
$content = urldecode($_POST['cspost']);
$str = '<!doctype html><html><head><meta charset="utf-8"><title>Chat</title></head><body><p>'.$content.'</p></body></html>';
file_put_contents("cs.html",$str);
?>