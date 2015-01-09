using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Voter
{
    /// <summary>
    /// allows to collect and display logs
    /// </summary>
    class Logs
    {
        /// <summary>
        /// Log list view
        /// </summary>
        private ListView logsListView;

        

        private string voterName;
        public string VoterName
        {
            set { voterName = value; }
            get { return voterName; }
        }
        /// <summary>
        /// Logs instance's constructor
        /// </summary>
        /// <param name="logsListView">logs list view</param>
        public Logs(ListView logsListView)
        {
            this.logsListView = logsListView;
            this.voterName = "Voter";
        }

        /// <summary>
        /// adds log
        /// </summary>
        /// <param name="log">log message</param>
        /// <param name="time">if print time</param>
        /// <param name="flag">type of message (error, info...)</param>
        /// <param name="anotherThread">thread flag</param>
        public void addLog(string log, bool time, int flag, bool anotherThread = false)
        {
            ListViewItem item = new ListViewItem();
            switch (flag)
            {
                case 0:
                    item.ForeColor = Color.Blue;
                    break;
                case 1:
                    item.ForeColor = Color.Black;
                    break;
                case 2:
                    item.ForeColor = Color.Red;
                    break;
                case 3:
                    item.ForeColor = Color.Green;
                    break;
            }

            if (time)
                item.Text = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + log;
            else
                item.Text = log;

            if (!anotherThread)
            {
                logsListView.Items.Add(item);
                logsListView.Items[logsListView.Items.Count - 1].EnsureVisible();
            }
            else
            {
                try
                {
                    logsListView.Invoke(new MethodInvoker(delegate()
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
    }
}