namespace OctopusAgileNotification
{
	partial class LogUI
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
			listLog = new System.Windows.Forms.ListView();
			Time = new System.Windows.Forms.ColumnHeader();
			Message = new System.Windows.Forms.ColumnHeader();
			btnClose = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// listLog
			// 
			listLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			listLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { Time, Message });
			listLog.Location = new System.Drawing.Point(12, 12);
			listLog.Name = "listLog";
			listLog.Size = new System.Drawing.Size(261, 425);
			listLog.TabIndex = 0;
			listLog.UseCompatibleStateImageBehavior = false;
			listLog.View = System.Windows.Forms.View.Details;
			// 
			// Time
			// 
			Time.Text = "Time";
			Time.Width = 70;
			// 
			// Message
			// 
			Message.Text = "Message";
			Message.Width = 180;
			// 
			// btnClose
			// 
			btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClose.Location = new System.Drawing.Point(193, 443);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(75, 23);
			btnClose.TabIndex = 1;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// LogUI
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(280, 478);
			Controls.Add(btnClose);
			Controls.Add(listLog);
			Name = "LogUI";
			Text = "LogUI";
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ListView listLog;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ColumnHeader Time;
		private System.Windows.Forms.ColumnHeader Message;
	}
}