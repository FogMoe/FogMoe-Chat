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

namespace FogMoeChatting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                string input = textBox1.Text;
                string postValue = Uri.EscapeUriString(input);
                var values = new NameValueCollection();
                values["cspost"] = postValue;

                var response = client.UploadValues("https://fog.moe/cschat/command.php", values);

                var responseString = Encoding.Default.GetString(response);
            }
            LoadMessage();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMessage();
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void LoadMessage() //同步聊天消息
        {
            string path = Directory.GetCurrentDirectory() + @"\message\";
            using (var client = new WebClient())
            {
                client.DownloadFile("https://fog.moe/cschat/message.fmc", path + "message.fmc");
            }
            string message = File.ReadAllText(path + "message.fmc");
            textBox2.AppendText(message + System.Environment.NewLine);
        }
    }

}
