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
using Newtonsoft.Json;

namespace Graph
{
    public partial class FormGetFile : Form
    {
        private Form1 parentWindow;
        private DiasPortalClient diasPortalClient;
        public FormGetFile(Form1 parent)
        {
            InitializeComponent();
            this.parentWindow = parent;
            this.diasPortalClient = new DiasPortalClient();
        }

        private void FormGetFile_Load(object sender, EventArgs e)
        {
            String serv = this.diasPortalClient.server + "/download_my_data.php";
            var postData = "login=botanic&password=123";// "submit=True";
//            postData += "login=" + this.diasPortalClient.login;
//            postData += "&password=" + this.diasPortalClient.password;
 //           postData += "&not_attach_ip=True";

            var data = Encoding.ASCII.GetBytes(postData);
            this.webBrowser1.Navigate(serv, string.Empty, data, string.Empty);
            
            //this.webBrowser1.Navigate("http://alex-r519-r719/download_raw_data.php");
 //           this.webBrowser1.
/*            var request = (HttpWebRequest)WebRequest.Create(serv);
            var postData = "submit=True";
            postData += "&login=" + this.diasPortalClient.login;
            postData += "&password=" + this.diasPortalClient.password;
            
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            { stream.Write(data, 0, data.Length); }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            label1.Text = responseString;
            //           responseString = "{'device':'device','date_timeStart':'2019-07-17','file_name':'test3.da','placeXStart':'60','placeYStart':'30','placeXFinish':'60','placeYFinish':'30'}";
            RawSphere reserche = JsonConvert.DeserializeObject<RawSphere>(responseString);
            label1.Text = reserche.file_name+" "+ reserche.date_timeStart;
            */
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string ps = /*this.diasPortalClient.fileServer+*/FindTeg.getLinkFile( this.webBrowser1.DocumentText);
            label1.Text = ps;
            //          string tempFileName = "c:/tmp.f";
            //            File.Copy(ps, tempFileName, true);
            try
            {
                this.parentWindow.open_File(ps);
                this.parentWindow.button1_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Не удалось открыть удалённый файл. Попробуйте скачать по ссылке в окне и открыть Файл->Открыть\nадрес файла: "+ps);
            }
            finally
            {
                this.Close();
            }
        }
    }
}
