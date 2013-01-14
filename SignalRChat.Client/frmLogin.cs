using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRChat.Client
{
    public partial class frmLogin : Form
    {
        IHubProxy myHub;

        public String Username { get; set; }
        
        public frmLogin(IHubProxy hubProxy)
        {
            this.myHub = hubProxy;
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Username = textBox1.Text;
            this.Close();
        }
    }
}
