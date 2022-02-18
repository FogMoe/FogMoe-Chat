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
using System.Text;

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
            webBrowser1.Document.ExecCommand("Refresh", false, null);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
