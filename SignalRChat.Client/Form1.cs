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
    public partial class Form1 : Form
    {
        IHubProxy myHub;
        HubConnection connection;
        
        public Form1()
        {
            InitializeComponent();
        }

private void Form1_Load(object sender, EventArgs e)
{
    SetStatus("Connecting");
    connection = new HubConnection("http://localhost:8080/chatserver", false);

    myHub = connection.CreateHubProxy("ChatHub");

    frmLogin login = new frmLogin(myHub);
    login.ShowDialog();

    connection.Start().ContinueWith(task => 
        {
            if (task.IsFaulted == false)
            {
                if (connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                {
                    SetStatus("Connected");

                    myHub.Invoke("Join", login.Username).ContinueWith(task_join =>
                    {
                        if (task_join.IsFaulted)
                        {
                            MessageBox.Show("Error during joining the server!");
                        }
                        else
                        {
                            Subscription sub = myHub.Subscribe("addMessage");
                            sub.Data += args =>
                            {
                                Message(args[0].ToString());
                            };
                                    
                            UpdateUsers();

                            timer1.Enabled = true;
                        }
                    });
                }
                       
            }

        });
}

        private void SetStatus(string status)
        {
           if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke(new MethodInvoker(delegate { toolStripStatusLabel1.Text = status; }));
            }
            else
            {
                toolStripStatusLabel1.Text = status;
            }
        }

        private void Message(string message)
        {
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new MethodInvoker(delegate { listView1.Items.Add(message); }));
            }
            else
            {
                listView1.Items.Add(message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myHub.Invoke("SendMessage", textBox1.Text).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MessageBox.Show("Error during sending the message!");
                }
                else
                {
                    textBox1.Invoke(new MethodInvoker(delegate { textBox1.Text = String.Empty; }));
                }
                
            });
           
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateUsers();
        }

        private void UpdateUsers()
        {
            myHub.Invoke<List<String>>("GetUsers").ContinueWith(task =>
            {
                if (task.IsFaulted == false)
                {
                    listBoxUsers.Invoke(new MethodInvoker(delegate 
                        {
                            listBoxUsers.Items.Clear();
                            listBoxUsers.DataSource = task.Result;
                        }));
                 }
            });
        }
    }
}
