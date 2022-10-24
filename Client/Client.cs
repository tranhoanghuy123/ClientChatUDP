using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            ShowMessage("Chat UDP Sử Dụng Server: vpswindow10.ddns.net");
            ShowMessage("Author: Trần Hoàng Huy ^_^");
            ShowMessage("Connected vpswindow10.ddns.net!");
        }

        private void txt_Message_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress[] IPServer = Dns.GetHostAddresses("vpswindow10.ddns.net"); // this is my server

            EndPoint remoteEP = new IPEndPoint(IPServer[0], 9999);
            //Note -- need to use EndPoint for the ReceiveFrom to work!
            byte[] data;
            int recv;

            client.SendTo(Encoding.UTF8.GetBytes(txt_Message.Text), remoteEP);
            data = new byte[1024];
            recv = client.ReceiveFrom(data, ref remoteEP);
            txt_Result.Text += "Receive From " + remoteEP.ToString() + ": ";
            ShowMessage(Encoding.UTF8.GetString(data, 0, recv));
            //client.Close();
        }
        public void ShowMessage(string message)
        {
            txt_Result.Text += message + "\r\n";
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Result.Text = null;
        }
    }
}
