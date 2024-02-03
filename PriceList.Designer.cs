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
			colDate = new System.Windows.Forms.ColumnHeader();
			colPrice = new System.Windows.Forms.ColumnHeader();
			SuspendLayout();
			// 
			// listViewPrices
			// 
			listViewPrices.AutoArrange = false;
			listViewPrices.BackColor = System.Drawing.SystemColors.Window;
			listViewPrices.BorderStyle = System.Windows.Forms.BorderStyle.None;
			listViewPrices.CausesValidation = false;
			listViewPrices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colDate, colPrice });
			listViewPrices.Dock = System.Windows.Forms.DockStyle.Fill;
			listViewPrices.FullRowSelect = true;
			listViewPrices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			listViewPrices.HideSelection = true;
			listViewPrices.Location = new System.Drawing.Point(2, 2);
			listViewPrices.MultiSelect = false;
			listViewPrices.Name = "listViewPrices";
			listViewPrices.Scrollable = false;
			listViewPrices.Size = new System.Drawing.Size(96, 446);
			listViewPrices.TabIndex = 0;
			listViewPrices.UseCompatibleStateImageBehavior = false;
			listViewPrices.View = System.Windows.Forms.View.Details;
			listViewPrices.MouseDown += listViewPrices_MouseDown;
			listViewPrices.MouseMove += listViewPrices_MouseMove;
			listViewPrices.MouseUp += listViewPrices_MouseUp;
			// 
			// colDate
			// 
			colDate.Text = "Date";
			// 
			// colPrice
			// 
			colPrice.Text = "Price";
			// 
			// PriceList
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			ClientSize = new System.Drawing.Size(100, 450);
			ControlBox = false;
			Controls.Add(listViewPrices);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "PriceList";
			Padding = new System.Windows.Forms.Padding(2);
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "PriceList";
			TopMost = true;
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ListView listViewPrices;
		private System.Windows.Forms.ColumnHeader colDate;
		private System.Windows.Forms.ColumnHeader colPrice;
	}
}