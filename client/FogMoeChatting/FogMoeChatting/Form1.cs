using System;
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

namespace FogMoeChatting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMessage(true);
        }

        public void ChangeKeyColor(RichTextBox rBox, string key, Color color)
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
            else
            {
                if (richTextBox1.Text == "暂无聊天消息")
                {
                    richTextBox1.Text = (message + System.Environment.NewLine);
                }
                else
                {
                    richTextBox1.AppendText(message + System.Environment.NewLine);
                }
                ChangeKeyColor(richTextBox1, "发送：", Color.FromArgb(0,47,147));
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
