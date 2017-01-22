namespace ElectionAuthority
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
            this.startElectionAuthorityButton = new System.Windows.Forms.Button();
            this.logsListView = new System.Windows.Forms.ListView();
            this.logColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.configButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.sendSLTokensAndTokensButton = new System.Windows.Forms.Button();
            this.finishVotingButton = new System.Windows.Forms.Button();
            this.countVotesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startElectionAuthorityButton
            // 
            this.startElectionAuthorityButton.Enabled = false;
            this.startElectionAuthorityButton.Location = new System.Drawing.Point(12, 211);
            this.startElectionAuthorityButton.Name = "startElectionAuthorityButton";
            this.startElectionAuthorityButton.Size = new System.Drawing.Size(123, 35);
            this.startElectionAuthorityButton.TabIndex = 0;
            this.startElectionAuthorityButton.Text = "Start ElectionAuthority";
            this.startElectionAuthorityButton.UseVisualStyleBackColor = true;
            this.startElectionAuthorityButton.Click += new System.EventHandler(this.startElectionAuthorityButton_Click);
            // 
            // logsListView
            // 
            this.logsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logColumn});
            this.logsListView.FullRowSelect = true;
            this.logsListView.Location = new System.Drawing.Point(13, 12);
            this.logsListView.Name = "logsListView";
            this.logsListView.Size = new System.Drawing.Size(740, 143);
            this.logsListView.TabIndex = 1;
            this.logsListView.UseCompatibleStateImageBehavior = false;
            this.logsListView.View = System.Windows.Forms.View.Details;
            // 
            // logColumn
            // 
            this.logColumn.Text = "Logs";
            this.logColumn.Width = 734;
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(13, 170);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(122, 35);
            this.configButton.TabIndex = 2;
            this.configButton.Text = "Load Configuration";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // sendSLTokensAndTokensButton
            // 
            this.sendSLTokensAndTokensButton.Enabled = false;
            this.sendSLTokensAndTokensButton.Location = new System.Drawing.Point(13, 252);
            this.sendSLTokensAndTokensButton.Name = "sendSLTokensAndTokensButton";
            this.sendSLTokensAndTokensButton.Size = new System.Drawing.Size(122, 35);
            this.sendSLTokensAndTokensButton.TabIndex = 3;
            this.sendSLTokensAndTokensButton.Text = "Send SL and tokens to PROXY";
            this.sendSLTokensAndTokensButton.UseVisualStyleBackColor = true;
            this.sendSLTokensAndTokensButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // finishVotingButton
            // 
            this.finishVotingButton.Enabled = false;
            this.finishVotingButton.Location = new System.Drawing.Point(13, 294);
            this.finishVotingButton.Name = "finishVotingButton";
            this.finishVotingButton.Size = new System.Drawing.Size(122, 35);
            this.finishVotingButton.TabIndex = 4;
            this.finishVotingButton.Text = "Finish voting";
            this.finishVotingButton.UseVisualStyleBackColor = true;
            this.finishVotingButton.Click += new System.EventHandler(this.finishVotingButton_Click);
            // 
            // countVotesButton
            // 
            this.countVotesButton.Enabled = false;
            this.countVotesButton.Location = new System.Drawing.Point(13, 336);
            this.countVotesButton.Name = "countVotesButton";
            this.countVotesButton.Size = new System.Drawing.Size(122, 35);
            this.countVotesButton.TabIndex = 5;
            this.countVotesButton.Text = "Count votes";
            this.countVotesButton.UseVisualStyleBackColor = true;
            this.countVotesButton.Click += new System.EventHandler(this.countVotesButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 409);
            this.Controls.Add(this.countVotesButton);
            this.Controls.Add(this.finishVotingButton);
            this.Controls.Add(this.sendSLTokensAndTokensButton);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.logsListView);
            this.Controls.Add(this.startElectionAuthorityButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Election Authority";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startElectionAuthorityButton;
        private System.Windows.Forms.ListView logsListView;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ColumnHeader logColumn;
        private System.Windows.Forms.Button sendSLTokensAndTokensButton;
        private System.Windows.Forms.Button finishVotingButton;
        private System.Windows.Forms.Button countVotesButton;
    }
}

