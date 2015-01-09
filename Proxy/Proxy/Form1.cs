using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proxy
{
    /// <summary>
    /// graphical user interface 
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// display logs in console, they help to understand what's going on in 
        /// </summary>
        private Logs logs;
        
        /// <summary>
        /// loads configuration from xml file
        /// </summary>
        private Configuration configuration;

        /// <summary>
        /// main logic of Proxy application 
        /// </summary>
        private Proxy proxy;
        
        /// <summary>
        /// constructor 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.logs = new Logs(this.logsListView);
            this.configuration = new Configuration(this.logs);

           
            
            

        }
        /// <summary>
        /// shows dialog to choose a configuration file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        /// <summary>
        /// accept a xml configuration file which was choosen from xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            configuration.loadConfiguration(openFileDialog.FileName);
            enableButtonsAfterConfiguration();
            this.proxy = new Proxy(this.logs, this.configuration,this);

        }
        /// <summary>
        /// connect with Election Authority application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectElectionAuthorityButton_Click(object sender, EventArgs e)
        {
            this.proxy.Client.connect(configuration.ElectionAuthorityIP, configuration.ElectionAuthorityPort);
        }


        /// <summary>
        /// form closing actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.proxy != null)
            {
                if (this.proxy.Server !=  null )
                    this.proxy.Server.stopServer();
                if (this.proxy.Client != null)
                    this.proxy.Client.disconnectFromElectionAuthority();
            }
            
        }
        /// <summary>
        /// starts running a Proxy server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startProxyButton_Click(object sender, EventArgs e)
        {
            this.proxy.Server.startServer(configuration.ProxyPort);
            this.proxy.generateSR();
            this.proxy.generateYesNoPosition();


            this.startProxyButton.Enabled = false;
            this.configButton.Enabled = false;
        }

        /// <summary>
        /// enable buttons after loading a configuration from xml file
        /// </summary>
        private void enableButtonsAfterConfiguration()
        {
            this.startProxyButton.Enabled = true;
            this.connectElectionAuthorityButton.Enabled = true;
        }

        /// <summary>
        /// disable connection with Election Authority
        /// </summary>
        public void disableConnectElectionAuthorityButton()
        {
            this.connectElectionAuthorityButton.Enabled = false;


        }
    }
}
