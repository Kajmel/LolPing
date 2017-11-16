using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace LolPingLibrary
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> Servers;
        String currentCheck = "";

        public Form1()
        {
            InitializeComponent();
        }

        public static double PingTimeAvg(string host, int time)
        {
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(host, 120);
                if (reply.Status == IPStatus.Success)
                {
                    return reply.RoundtripTime;
                }
                return -1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void InfoLoad(object sender, EventArgs e)
        {
            Servers = new Dictionary<string, string>()
            {
                {"Eune", "104.160.142.3" },
                {"Euw", "104.160.141.3" }
            };
        }

        private void radio_CheckChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            RadioButton btn = (RadioButton)sender;
            currentCheck = btn.Text.ToString();
            if (!timer1.Enabled)
                timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double temp = PingTimeAvg(Servers[currentCheck], 10);
            if (temp != 0)
            {
                if (temp != -1)
                    label1.Text = temp.ToString();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Connection Error");
            }
        }
    }
}

