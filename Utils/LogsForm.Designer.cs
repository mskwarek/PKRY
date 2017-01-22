namespace Utils
{
    partial class LogsForm
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
            this.logsListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // logsListView
            // 
            this.logsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.logsListView.Location = new System.Drawing.Point(0, 0);
            this.logsListView.Name = "logsListView";
            this.logsListView.Size = new System.Drawing.Size(929, 425);
            this.logsListView.TabIndex = 0;
            this.logsListView.UseCompatibleStateImageBehavior = false;
            this.logsListView.View = System.Windows.Forms.View.List;
            // 
            // LogsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 425);
            this.Controls.Add(this.logsListView);
            this.Name = "LogsForm";
            this.Text = "LogsForm";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView logsListView;
    }
}