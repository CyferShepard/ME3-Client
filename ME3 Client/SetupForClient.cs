using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace Setup
{
    public partial class SetupForClient: Form
    {
        private static string loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
        

        public SetupForClient()
        {
            InitializeComponent();
            string[] lines = File.ReadAllLines(loc + "conf\\executable.txt");
            try { textBox2.Text = lines[0]; } catch { }
            button3.Enabled = false;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string IP = textBox1.Text;
            string[] lines = File.ReadAllLines(loc + "conf\\conf.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("#"))
                    continue;
                string[] parts = lines[i].Split('=');
                if (parts[0].Trim() == "IP")
                    lines[i] = "IP=" + IP;
                if (parts[0].Trim() == "RedirectIP")
                    lines[i] = "RedirectIP=" + IP;
            }
            button3.Enabled = true;
            File.WriteAllLines(loc + "conf\\conf.txt", lines);
            ME3Server_WV.Frontend.ActivateRedirection(IP);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ME3Server_WV.Frontend.DeactivateRedirection();
            button3.Enabled = false;
        }

        private void SetupForClient_Load(object sender, EventArgs e)
        {

        }


        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\Program Files (x86)\";
            fdlg.Filter = "MassEffect3.exe|*MassEffect3.exe*|All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                string file = fdlg.FileName;
                textBox2.Text =file;
                
                File.WriteAllText(loc + "conf\\executable.txt",file);
            }

          //  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string file = textBox2.Text;
            if (file == "")
            {
                MessageBox.Show("Locate MassEffect3.exe First.", "Error: No File Selected.",
    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try { System.Diagnostics.Process.Start(@"" + file); }
                catch {
                    MessageBox.Show("MassEffect3.exe Not Found", "Error: File Not Found.",
MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
           

        }


        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mass Effect 3 Private Server Switcher By Cyfer\n\nFirst Set The IP Address of your Server. You can find it by opening CMD and typing the Command ipconfig on the Device Hosting The Sever.\n\nThen Click Set Ip and Redirect.\n\nNow Click Browse and Locate The MassEffect3.exe, it should be in your game folder\\Binaries\\Win32.\n\nNow Click Launch Mass Effect 3.exe", "Help.",
MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
