using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Voter
{
    public class Logs
    {
        private ListView logsListView;
        private string voterName;
        public string VoterName
        {
            set { voterName = value; }
            get { return voterName; }
        }

        public Logs(ListView logsListView)
        {
            this.logsListView = logsListView;
            this.voterName = "Voter";
        }

        public void addLog(string log, bool time, int flag, bool anotherThread = false)
        {
            ListViewItem item = new ListViewItem();
            item.ForeColor = int_get_log_color(flag);
            item.Text = int_get_log_message(time, log);
        
            int_add_log_to_ui(anotherThread, item);

            try
            {               
                using (System.IO.StreamWriter file = new StreamWriter(@"Logs\" + voterName  +".txt" , true))
                {
                    file.Write(" [" + DateTime.Now.ToString("HH:mm:ss") + "] " + log + Environment.NewLine);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("You should use bat file to save logs to file");
            }

        }

        private Color int_get_log_color(int flag)
        {
            Color r = Color.White;
            switch (flag)
            {
                case 0:
                    r = Color.Blue;
                    break;
                case 1:
                    r = Color.Black;
                    break;
                case 2:
                    r = Color.Red;
                    break;
                case 3:
                    r = Color.Green;
                    break;
            }

            return r;
        }

        private string int_get_log_message(bool time, string log)
        {
            string msg = "";
            if (time)
            {
                msg += "[" + DateTime.Now.ToString("HH:mm:ss") + "] ";
            }
            msg += log;

            return msg;
        }

        private void int_add_log_to_ui(bool anotherThread, ListViewItem item)
        {
            if (!anotherThread)
            {
                logsListView.Items.Add(item);
                logsListView.Items[logsListView.Items.Count - 1].EnsureVisible();
            }
            else
            {
                try
                {
                    logsListView.Invoke(new MethodInvoker(delegate ()
                    {
                        logsListView.Items.Add(item);
                        logsListView.Items[logsListView.Items.Count - 1].EnsureVisible();
                    })
                    );

                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
            }
        }
    }
}