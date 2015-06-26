using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETCONLib;
using System.Text.RegularExpressions;

namespace KillSX
{
    public partial class MainFm : Form
    {
        private System.Diagnostics.Process _pCmd;
        private string _strWrite; 

        public MainFm()
        {
            InitializeComponent();
            _pCmd = new System.Diagnostics.Process();
            _pCmd.StartInfo.FileName = "cmd.exe";
            _pCmd.StartInfo.UseShellExecute = false;
            _pCmd.StartInfo.RedirectStandardOutput = true;
            _pCmd.StartInfo.RedirectStandardInput = true;
            _pCmd.StartInfo.CreateNoWindow = true;
            _pCmd.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"[\u4e00-\u9fa5]");//正则表达式
            if (reg.IsMatch(textBox1.Text) || reg.IsMatch(textBox2.Text))
            {
                MessageBox.Show("不能含有汉字");
                return;
            }
            if (textBox2.Text.Length < 8)
            {
                MessageBox.Show("密码8位以上");
                return;
            }

            _strWrite = String.Format("netsh wlan set hostednetwork mode=allow ssid={0} key={1}", this.textBox1.Text, this.textBox2.Text);
            _pCmd.StandardInput.WriteLine(_strWrite);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KillSX k = new KillSX();
            int shanxun = k.GetProcessInfo(this.textBox6.Text);
            KillSX.EndProcess(shanxun);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _pCmd.StandardInput.WriteLine("netsh wlan start hostednetwork");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionToShare = this.textBox3.Text; // 被共享的网络连接
                string sharedForConnection = this.textBox4.Text; // 需要共享的网络连接

                NetSharingManager manager = new NetSharingManager();
                var connections = manager.EnumEveryConnection;

                foreach (INetConnection c in connections)
                {
                    var props = manager.NetConnectionProps[c];
                    var sharingCfg = manager.INetSharingConfigurationForINetConnection[c];
                    if (props.Name == connectionToShare)
                    {
                        sharingCfg.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
                    }
                    else if (props.Name == sharedForConnection)
                    {
                        sharingCfg.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
                    }
                }
            }
            catch
            {
                MessageBox.Show("请打开网络和共享中心·查看是不是已经连接Internet！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _strWrite = String.Format("shutdown -s -t {0}", this.textBox5.Text);
            _pCmd.StandardInput.WriteLine(_strWrite);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _pCmd.StandardInput.WriteLine("netsh wlan stop hostednetwork");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _pCmd.StandardInput.WriteLine("shutdown -a");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cnblogs.com/KC-Mei/");
        }

    }
}
