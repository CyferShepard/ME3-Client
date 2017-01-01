using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ME3Server_WV
{
    public partial class Frontend : Form
    {
        public static string loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
        public static string sysdir = Environment.SystemDirectory + "\\";
      //  public GUI_Log LogWindow;
      //  public GUI_Player PlayerWindow;
      //  public GUI_GameList GameList;
        private static Frontend frontend;

        public Frontend()
        {
            InitializeComponent();
            frontend = this;
        }

   

        private void Frontend_Load(object sender, EventArgs e)
        {
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = "Mass Effect 3 Private Server by Warranty Voider, build: " + version;
           
        }

      

        

      
        private void aktivateRedirectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivateRedirection(Config.FindEntry("RedirectIP"));
        }

        public static void ActivateRedirection(string hostIP)
        {
            DeactivateRedirection(false);
            try
            {
                List<string> r = new List<string>(File.ReadAllLines(loc + "conf\\redirect.txt"));
                List<string> h = new List<string>(File.ReadAllLines(sysdir + @"drivers\etc\hosts"));
                foreach (string url in r)
                {
                    string s = hostIP + " " + url;
                    if (!h.Contains(s))
                        h.Add(s);
                }
                File.WriteAllLines(sysdir + @"drivers\etc\hosts", h);
                MessageBox.Show("Done.", "Activate Redirection");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.GetType().Name + ": " + ex.Message, "Activate Redirection");
            }
        }

        private void deactivateRedirectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeactivateRedirection();
        }

        public static void DeactivateRedirection(bool bShowMsg = true)
        {
            try
            {
                List<string> r = new List<string>(File.ReadAllLines(loc + "conf\\redirect.txt"));
                List<string> h = new List<string>(File.ReadAllLines(sysdir + @"drivers\etc\hosts"));
                foreach (string url in r)
                {
                    for (int i = (h.Count - 1); i >= 0; i--)
                    {
                        if (h[i].EndsWith(url) && !h[i].StartsWith("#"))
                            h.RemoveAt(i);
                    }
                }
                File.WriteAllLines(sysdir + @"drivers\etc\hosts", h);
                if (bShowMsg)
                    MessageBox.Show("Done.", "Deactivate Redirection");
            }
            catch (Exception ex)
            {
                if (bShowMsg)
                    MessageBox.Show("Error:\n" + ex.GetType().Name + ": " + ex.Message, "Deactivate Redirection");
                else
                    System.Diagnostics.Debug.Print("DeactivateRedirection | " + ex.GetType().Name + ": " + ex.Message);
            }
        }

        public static bool IsRedirectionActive()
        {
            try
            {

                int count = 0;
                List<string> r = new List<string>(File.ReadAllLines(loc + "conf\\redirect.txt"));
                List<string> h = new List<string>(File.ReadAllLines(sysdir + @"drivers\etc\hosts"));
                foreach (string url in r)
                {
                    for (int i = (h.Count - 1); i >= 0; i--)
                    {
                        if (h[i].EndsWith(url) && !h[i].StartsWith("#"))
                            count++;
                    }
                }
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("IsRedirectionActive | Error:\n" + ex.GetType().Name + ": " + ex.Message);
                return false;
            }
        }

        
      
    }
}
