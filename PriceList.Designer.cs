namespace OctopusAgileNotification
{
	partial class PriceList
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
			listViewPrices = new System.Windows.Forms.ListView();
			SuspendLayout();
			// 
			// listViewPrices
			// 
			listViewPrices.AutoArrange = false;
			listViewPrices.BorderStyle = System.Windows.Forms.BorderStyle.None;
			listViewPrices.Dock = System.Windows.Forms.DockStyle.Fill;
			listViewPrices.GridLines = true;
			listViewPrices.Location = new System.Drawing.Point(0, 0);
			listViewPrices.Name = "listViewPrices";
			listViewPrices.Size = new System.Drawing.Size(133, 450);
			listViewPrices.TabIndex = 0;
			listViewPrices.UseCompatibleStateImageBehavior = false;
			listViewPrices.View = System.Windows.Forms.View.List;
			// 
			// PriceList
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(133, 450);
			ControlBox = false;
			Controls.Add(listViewPrices);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "PriceList";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "PriceList";
			TopMost = true;
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ListView listViewPrices;
	}
}