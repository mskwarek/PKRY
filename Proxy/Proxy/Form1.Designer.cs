namespace Proxy
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
            this.logsListView = new System.Windows.Forms.ListView();
            this.logColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.configButton = new System.Windows.Forms.Button();
            this.startProxyButton = new System.Windows.Forms.Button();
            this.connectElectionAuthorityButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.sendSLAndSRButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logsListView
            // 
            this.logsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logColumn});
            this.logsListView.FullRowSelect = true;
            this.logsListView.Location = new System.Drawing.Point(13, 13);
            this.logsListView.Name = "logsListView";
            this.logsListView.Size = new System.Drawing.Size(380, 220);
            this.logsListView.TabIndex = 0;
            this.logsListView.UseCompatibleStateImageBehavior = false;
            this.logsListView.View = System.Windows.Forms.View.Details;
            // 
            // logColumn
            // 
            this.logColumn.Text = "Logs";
            this.logColumn.Width = 352;
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(13, 253);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(107, 35);
            this.configButton.TabIndex = 1;
            this.configButton.Text = "Load Configuration";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // startProxyButton
            // 
            this.startProxyButton.Enabled = false;
            this.startProxyButton.Location = new System.Drawing.Point(12, 294);
            this.startProxyButton.Name = "startProxyButton";
            this.startProxyButton.Size = new System.Drawing.Size(108, 35);
            this.startProxyButton.TabIndex = 2;
            this.startProxyButton.Text = "Start Proxy";
            this.startProxyButton.UseVisualStyleBackColor = true;
            this.startProxyButton.Click += new System.EventHandler(this.startProxyButton_Click);
            // 
            // connectElectionAuthorityButton
            // 
            this.connectElectionAuthorityButton.Enabled = false;
            this.connectElectionAuthorityButton.Location = new System.Drawing.Point(13, 336);
            this.connectElectionAuthorityButton.Name = "connectElectionAuthorityButton";
            this.connectElectionAuthorityButton.Size = new System.Drawing.Size(107, 35);
            this.connectElectionAuthorityButton.TabIndex = 3;
            this.connectElectionAuthorityButton.Text = "Connect to Election Authority";
            this.connectElectionAuthorityButton.UseVisualStyleBackColor = true;
            this.connectElectionAuthorityButton.Click += new System.EventHandler(this.connectElectionAuthorityButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // sendSLAndSRButton
            // 
            this.sendSLAndSRButton.Enabled = false;
            this.sendSLAndSRButton.Location = new System.Drawing.Point(13, 378);
            this.sendSLAndSRButton.Name = "sendSLAndSRButton";
            this.sendSLAndSRButton.Size = new System.Drawing.Size(107, 35);
            this.sendSLAndSRButton.TabIndex = 4;
            this.sendSLAndSRButton.Text = "Send SR and SL to Voter";
            this.sendSLAndSRButton.UseVisualStyleBackColor = true;
            this.sendSLAndSRButton.Click += new System.EventHandler(this.sendSLAndSR_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 447);
            this.Controls.Add(this.sendSLAndSRButton);
            this.Controls.Add(this.connectElectionAuthorityButton);
            this.Controls.Add(this.startProxyButton);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.logsListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Proxy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView logsListView;
        private System.Windows.Forms.ColumnHeader logColumn;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.Button startProxyButton;
        private System.Windows.Forms.Button connectElectionAuthorityButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button sendSLAndSRButton;
    }
}

