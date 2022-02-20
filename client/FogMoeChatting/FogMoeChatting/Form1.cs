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
        Thread tThread;
        Thread cThread;
        public string path = Directory.GetCurrentDirectory() + @"\message\";
        public ChatBox()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            tThread = new Thread(TestOnlineStatus);
        }


        public void Form1_Load(object sender, EventArgs e)
        {
            tThread.Start();
            LoadMessage(true);
            tThread.IsBackground = true;
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
                ChangeKeyColor(richTextBox1, "发送：", Color.FromArgb(254,67,101));
                string[] lines = richTextBox1.Text.Split('\n');
                foreach (var line in lines)
                {
                    if (line.Length >0)
                    {
                        string date = line.Substring(0, 14);
                        ChangeKeyColor(richTextBox1, date, Color.FromArgb(0, 47, 147));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(tThread.IsAlive == false)
            {
                tThread.Resume();
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
            tThread.Suspend();
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

            button1.Enabled = false;
            cThread = new Thread(Frequency);
            cThread.Start();
            cThread.IsBackground = true;
            
        }

        void Frequency()
        {
            Thread.Sleep(5000);
            button1.Enabled = true;
            cThread.Abort();
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

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "雾萌聊天记录文件(*.fmc) | *.fmc | 所有文件(*.*) | *.*";
            sfd.DefaultExt = ".fmc";
            sfd.AddExtension = true;
            sfd.RestoreDirectory = true;
            sfd.FileName = DateTime.Now.ToLongDateString().ToString()+ DateTime.Now.ToLongTimeString().ToString().Replace(":","_") +" 雾萌聊天记录.fmc";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.Copy(path + "message.fmc", sfd.FileName);
            }
        }
    }

}
