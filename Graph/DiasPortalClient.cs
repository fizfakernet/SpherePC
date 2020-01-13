using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Threading;


namespace Graph
{
    class DiasPortalClient
    {
        public String server;
        public String login;
        public String password;
        public String fileServer;
        public DiasPortalClient()
        {
            this.server = Properties.Settings.Default.server;
            this.login = Properties.Settings.Default.login;
            this.password = Properties.Settings.Default.password;
            this.fileServer = Properties.Settings.Default.fileServer;
        }

        public void setTemp(string serv, string user, string pass,string fileServ)
        {
            this.server = serv;
            this.login = user;
            this.password = pass;
            this.fileServer = fileServ;
        }
        public void save()
        {
            Properties.Settings.Default.server = this.server;
            Properties.Settings.Default.login = this.login;
            Properties.Settings.Default.password = this.password;
            Properties.Settings.Default.fileServer = this.fileServer;
            Properties.Settings.Default.Save();
        }
        public void set(string serv, string user, string pass, string fileServ)
        {
            this.setTemp(serv, user, pass, fileServ);
            this.save();
        }
        public void getUserInfo()
        {

        }

    }
}
