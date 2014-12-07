using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ElectionAuthority
{
    public partial class Form1 : Form
    {
        private Logs logs;
        private Server server;
        private Configuration configuration;

        public Form1()
        {
            InitializeComponent();
            setColumnWidth();            
            logs = new Logs(this.logsListView);
            configuration = new Configuration(this.logs);
            configuration.loadConfiguration(Constants.PATH_TO_CONFIG);
            server = new Server(this.logs);
        }

        private void startElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            this.server.startServer(configuration.ElectionAuthorityPort);
            this.startElectionAuthorityButton.Enabled = false;
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonAfterConfiguration();
        }


        private void enableButtonAfterConfiguration()
        {
            this.startElectionAuthorityButton.Enabled = true;
            this.configButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.server.stopServer();
        }

        private void setColumnWidth()
        {
            this.logColumn.Width = this.logsListView.Width - 5;
        }
    }
}
