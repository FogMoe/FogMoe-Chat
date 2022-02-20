﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Threading;

namespace FogMoeChatting
{
    public partial class ChatBox : Form
    {
        public ChatBox()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMessage(true);            

            Thread t = new Thread(TestOnlineStatus);
            t.Start();
        }

        void TestOnlineStatus()
        {
            while (true)
            {
                if (OnlineStatus() == true)
                {
                    label2.Text = "已连接到通讯服务器";
                    LoadMessage(false);
                }
                else
                {
                    label2.Text = "连接通讯服务器失败";
                }
                Thread.Sleep(3000);
            }

        }




        public bool OnlineStatus() //判断在线状态连接服务器
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send("fog.moe");
            if (pingReply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void ChangeKeyColor(RichTextBox rBox, string key, Color color) //改字符特定颜色
        {
            Regex regex = new Regex(key);
            MatchCollection collection = regex.Matches(rBox.Text);
            foreach (Match match in collection)
            {
                rBox.SelectionStart = match.Index;
                rBox.SelectionLength = key.Length;
                rBox.SelectionColor = color;
            }
        }

        public string EncoideUrI(string _encodeValue) //转换编码
        {
            string encodeResult = Uri.EscapeUriString(_encodeValue);
            return encodeResult;
        }

        public void LoadMessage(bool _init) //同步聊天消息
        {
            string path = Directory.GetCurrentDirectory() + @"\message\";
            using (var client = new WebClient())
            {
                client.DownloadFile("https://fog.moe/cschat/message.fmc", path + "message.fmc");
            }
            string message = File.ReadAllText(path + "message.fmc");
            if (_init == true)
            {
                richTextBox1.Clear();
                richTextBox1.Text = ("暂无聊天消息");
            }
            else if (message == "")
            {
                richTextBox1.Text = ("暂无聊天消息");
            }
            else if (richTextBox1.Text != message && message != "")
            {
                richTextBox1.Text = message;
                ChangeKeyColor(richTextBox1, "发送：", Color.FromArgb(0,47,147));
                //MessageBox.Show(richTextBox1.Text.LastIndexOf("∝").ToString());
                //MessageBox.Show(richTextBox1.Text.LastIndexOf("№").ToString());
                //string dateStr = richTextBox1.Text.Substring(richTextBox1.Text.LastIndexOf("∝"), richTextBox1.Text.LastIndexOf("№"));
                //ChangeKeyColor(richTextBox1, dateStr, Color.FromArgb(0, 47, 147));
                //richTextBox1.Text.Replace("∝", "");
                //richTextBox1.Text.Replace("№", "");
            }
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (OnlineStatus() == true)
            {
                label2.Text = "已连接到通讯服务器";

                string userName = textBox2.Text;
                using (var client = new WebClient())
                {
                    string input = textBox1.Text;
                    var values = new NameValueCollection();
                    values["cspost"] = EncoideUrI(input);
                    values["userName"] = EncoideUrI(userName);

                    var response = client.UploadValues("https://fog.moe/cschat/command.php", values);

                    var responseString = Encoding.Default.GetString(response);
                }
                LoadMessage(false);
                textBox1.Clear();
            }
            else
            {
                label2.Text = "连接通讯服务器失败";
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


    }

}
