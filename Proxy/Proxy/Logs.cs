using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Proxy
{
    /// <summary>
    /// displays and collects logs from proxy's actions
    /// </summary>
    class Logs
    {
        /// <summary>
        /// log list view, to display logs
        /// </summary>
        private ListView logsListView;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logsListView">list view for logs</param>
        public Logs(ListView logsListView)
        {
            this.logsListView = logsListView;
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
                logsListView.Invoke(new MethodInvoker(delegate()
                {
                    logsListView.Items.Add(item);
                    logsListView.Items[logsListView.Items.Count - 1].EnsureVisible();
                })
                    );
            }
        }
    }
}
