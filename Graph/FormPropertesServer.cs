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
using System.IO;

namespace Graph
{
    public partial class FormPropertesServer : Form
    {
        private DiasPortalClient diasPortalClient;
        public FormPropertesServer()
        {
            InitializeComponent();
            this.diasPortalClient = new DiasPortalClient();
            textBoxServer.Text = this.diasPortalClient.server;
            textBoxLogin.Text = this.diasPortalClient.login;
            textBoxPass.Text = this.diasPortalClient.password;
            textBoxFileServer.Text = this.diasPortalClient.fileServer;
        }

        private void TestConnectionbutton_Click(object sender, EventArgs e)
        {
            const String CorrectAuthText = "ok";

            //this.diasPortalClient.set(textBoxServer.Text, textBoxLogin.Text, textBoxPass.Text);
            String serv = textBoxServer.Text + "/getuserinfo.php";
            var request = (HttpWebRequest)WebRequest.Create(serv);
            var postData = "submit=True";
            postData += "&login=" + textBoxLogin.Text;
            postData += "&password=" + textBoxPass.Text;
            //            postData += "&userfile=" + openFileDialog1.FileName;
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            { stream.Write(data, 0, data.Length); }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            label4.Text = responseString;
            if (responseString.Contains(CorrectAuthText))
            {
        //        MessageBox.Show("Авторизованы!");
            }
            else
            {
         //       MessageBox.Show("Не удалось авторизоваться!");
            }
        }

        private void SaveEditionsbutton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.server = textBoxServer.Text;
            Properties.Settings.Default.login = textBoxLogin.Text;
            Properties.Settings.Default.password = textBoxPass.Text;
            Properties.Settings.Default.fileServer = textBoxFileServer.Text;
            Properties.Settings.Default.Save();
            //diasPortalClient.save();
            MessageBox.Show("настройки сохранены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
