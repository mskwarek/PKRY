using System.Collections.Generic;
using System.Windows.Forms;


namespace Voter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.EAConnectButton = new System.Windows.Forms.Button();
            this.ProxyConnectButton = new System.Windows.Forms.Button();
            this.logsListView = new System.Windows.Forms.ListView();
            this.logColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.configButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.getSLandSRButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.getCandidateListButton = new System.Windows.Forms.Button();
            this.sendVoteButton = new System.Windows.Forms.Button();
            this.confirmationBox = new System.Windows.Forms.ComboBox();
            this.ConfBox = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // EAConnectButton
            // 
            this.EAConnectButton.Enabled = false;
            this.EAConnectButton.Location = new System.Drawing.Point(12, 307);
            this.EAConnectButton.Name = "EAConnectButton";
            this.EAConnectButton.Size = new System.Drawing.Size(163, 25);
            this.EAConnectButton.TabIndex = 0;
            this.EAConnectButton.Text = "Connect to Election Authority";
            this.EAConnectButton.UseVisualStyleBackColor = true;
            this.EAConnectButton.Click += new System.EventHandler(this.EAConnectButton_Click);
            // 
            // ProxyConnectButton
            // 
            this.ProxyConnectButton.Enabled = false;
            this.ProxyConnectButton.Location = new System.Drawing.Point(12, 338);
            this.ProxyConnectButton.Name = "ProxyConnectButton";
            this.ProxyConnectButton.Size = new System.Drawing.Size(163, 25);
            this.ProxyConnectButton.TabIndex = 1;
            this.ProxyConnectButton.Text = "Connect to Proxy";
            this.ProxyConnectButton.UseVisualStyleBackColor = true;
            this.ProxyConnectButton.Click += new System.EventHandler(this.ProxyConnectButton_Click);
            // 
            // logsListView
            // 
            this.logsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logColumn});
            this.logsListView.FullRowSelect = true;
            this.logsListView.Location = new System.Drawing.Point(12, 12);
            this.logsListView.Name = "logsListView";
            this.logsListView.Size = new System.Drawing.Size(331, 238);
            this.logsListView.TabIndex = 2;
            this.logsListView.UseCompatibleStateImageBehavior = false;
            this.logsListView.View = System.Windows.Forms.View.Details;
            // 
            // logColumn
            // 
            this.logColumn.Text = "Log";
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(12, 276);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(163, 25);
            this.configButton.TabIndex = 3;
            this.configButton.Text = "Load configuration";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // getSLandSRButton
            // 
            this.getSLandSRButton.Enabled = false;
            this.getSLandSRButton.Location = new System.Drawing.Point(13, 370);
            this.getSLandSRButton.Name = "getSLandSRButton";
            this.getSLandSRButton.Size = new System.Drawing.Size(162, 23);
            this.getSLandSRButton.TabIndex = 5;
            this.getSLandSRButton.Text = "Get SL and SR ";
            this.getSLandSRButton.UseVisualStyleBackColor = true;
            this.getSLandSRButton.Click += new System.EventHandler(this.getSLandSRButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(367, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 450);
            this.panel1.TabIndex = 4;
            // 
            // getCandidateListButton
            // 
            this.getCandidateListButton.Enabled = false;
            this.getCandidateListButton.Location = new System.Drawing.Point(13, 399);
            this.getCandidateListButton.Name = "getCandidateListButton";
            this.getCandidateListButton.Size = new System.Drawing.Size(162, 23);
            this.getCandidateListButton.TabIndex = 6;
            this.getCandidateListButton.Text = "Get candidate list";
            this.getCandidateListButton.UseVisualStyleBackColor = true;
            this.getCandidateListButton.Click += new System.EventHandler(this.getCandidateListButton_Click);
            // 
            // sendVoteButton
            // 
            this.sendVoteButton.Enabled = false;
            this.sendVoteButton.Location = new System.Drawing.Point(181, 307);
            this.sendVoteButton.Name = "sendVoteButton";
            this.sendVoteButton.Size = new System.Drawing.Size(163, 25);
            this.sendVoteButton.TabIndex = 8;
            this.sendVoteButton.Text = "Send vote";
            this.sendVoteButton.UseVisualStyleBackColor = true;
            this.sendVoteButton.Click += new System.EventHandler(this.sendVoteButton_Click);
            // 
            // confirmationBox
            // 
            this.confirmationBox.Enabled = false;
            this.confirmationBox.FormattingEnabled = true;
            this.confirmationBox.Items.AddRange(new object[] {
            "Column A",
            "Column B",
            "Column C",
            "Column D"});
            this.confirmationBox.Location = new System.Drawing.Point(181, 279);
            this.confirmationBox.Name = "confirmationBox";
            this.confirmationBox.Size = new System.Drawing.Size(163, 21);
            this.confirmationBox.TabIndex = 9;
            this.confirmationBox.Text = "Choose confirmation";
            this.confirmationBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ConfBox
            // 
            this.ConfBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.ConfBox.FullRowSelect = true;
            this.ConfBox.Location = new System.Drawing.Point(181, 334);
            this.ConfBox.Name = "ConfBox";
            this.ConfBox.Size = new System.Drawing.Size(163, 117);
            this.ConfBox.TabIndex = 10;
            this.ConfBox.UseCompatibleStateImageBehavior = false;
            this.ConfBox.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Your Confirmation";
            this.columnHeader1.Width = 105;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 478);
            this.Controls.Add(this.ConfBox);
            this.Controls.Add(this.confirmationBox);
            this.Controls.Add(this.sendVoteButton);
            this.Controls.Add(this.getCandidateListButton);
            this.Controls.Add(this.getSLandSRButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.logsListView);
            this.Controls.Add(this.ProxyConnectButton);
            this.Controls.Add(this.EAConnectButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Voter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EAConnectButton;
        private System.Windows.Forms.Button ProxyConnectButton;
        private System.Windows.Forms.ListView logsListView;
        private System.Windows.Forms.ColumnHeader logColumn;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Button getSLandSRButton;
        private Panel panel1;
        private Button getCandidateListButton;
        private Button sendVoteButton;
        private ComboBox confirmationBox;
        private ListView ConfBox;
        private ColumnHeader columnHeader1;
    }
}

