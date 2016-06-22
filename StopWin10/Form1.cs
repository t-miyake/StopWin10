using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace StopWin10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteReg(@"SOFTWARE\Policies\Microsoft\Windows\Gwx", "DisableGwx", 1);
            WriteReg(@"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "DisableOSUpgrade", 1);
            MessageBox.Show("設定が完了しました。");
            CheckStatus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WriteReg(@"SOFTWARE\Policies\Microsoft\Windows\Gwx", "DisableGwx", 0);
            WriteReg(@"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "DisableOSUpgrade", 0);
            MessageBox.Show("設定が完了しました。");
            CheckStatus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckStatus();
        }

        void WriteReg(string SubKey, string KeyName,int Value)
        {
            var key = Registry.LocalMachine.CreateSubKey(SubKey);
            key.SetValue(KeyName, Value, RegistryValueKind.DWord);
            key.Close();
        }

        int ReadReg(string SubKey, string KeyName)
        {
            try
            {
                var key = Registry.LocalMachine.OpenSubKey(SubKey);
                return (int)key.GetValue(KeyName);
            }
            catch
            {
                return 0;
            }
        }

        void CheckStatus()
        {
            switch (ReadReg(@"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate", "DisableOSUpgrade"))
            {
                case 0:
                    label1.Text = "Windows10への自動アップデート：有効";
                    break;
                case 1:
                    label1.Text = "Windows10への自動アップデート：無効";
                    break;
                default:
                    label1.Text = "Windows10への自動アップデート：不明";
                    break;
            }
        }

    }
}
