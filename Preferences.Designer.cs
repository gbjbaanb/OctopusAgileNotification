namespace OctopusAgileNotification
{
	partial class Preferences
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
			tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			logoPictureBox = new System.Windows.Forms.PictureBox();
			labelTitle = new System.Windows.Forms.Label();
			labelProduct = new System.Windows.Forms.Label();
			labelTariff = new System.Windows.Forms.Label();
			labelAPI = new System.Windows.Forms.Label();
			textBoxDescription = new System.Windows.Forms.TextBox();
			textProduct = new System.Windows.Forms.TextBox();
			textTariff = new System.Windows.Forms.TextBox();
			textAPI = new System.Windows.Forms.TextBox();
			okButton = new System.Windows.Forms.Button();
			btnColourFg0 = new System.Windows.Forms.Button();
			textBoxThreshold0 = new System.Windows.Forms.TextBox();
			btnColourBg0 = new System.Windows.Forms.Button();
			textBoxThreshold1 = new System.Windows.Forms.TextBox();
			btnColourFg1 = new System.Windows.Forms.Button();
			btnColourBg1 = new System.Windows.Forms.Button();
			textBoxThreshold2 = new System.Windows.Forms.TextBox();
			btnColourFg2 = new System.Windows.Forms.Button();
			btnColourBg2 = new System.Windows.Forms.Button();
			btnColourFg3 = new System.Windows.Forms.Button();
			btnColourBg3 = new System.Windows.Forms.Button();
			textBoxThreshold3 = new System.Windows.Forms.TextBox();
			groupBoxThresholds = new System.Windows.Forms.GroupBox();
			tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
			groupBoxThresholds.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			tableLayoutPanel.ColumnCount = 3;
			tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.94773F));
			tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.7695351F));
			tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.2827339F));
			tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
			tableLayoutPanel.Controls.Add(labelTitle, 1, 0);
			tableLayoutPanel.Controls.Add(labelProduct, 1, 1);
			tableLayoutPanel.Controls.Add(labelTariff, 1, 2);
			tableLayoutPanel.Controls.Add(labelAPI, 1, 3);
			tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
			tableLayoutPanel.Controls.Add(textProduct, 2, 1);
			tableLayoutPanel.Controls.Add(textTariff, 2, 2);
			tableLayoutPanel.Controls.Add(textAPI, 2, 3);
			tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel.Location = new System.Drawing.Point(10, 10);
			tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			tableLayoutPanel.Name = "tableLayoutPanel";
			tableLayoutPanel.RowCount = 5;
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857141F));
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857141F));
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857141F));
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857141F));
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.8571434F));
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel.Size = new System.Drawing.Size(487, 207);
			tableLayoutPanel.TabIndex = 0;
			// 
			// logoPictureBox
			// 
			logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			logoPictureBox.Image = (System.Drawing.Image)resources.GetObject("logoPictureBox.Image");
			logoPictureBox.Location = new System.Drawing.Point(4, 3);
			logoPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			logoPictureBox.Name = "logoPictureBox";
			tableLayoutPanel.SetRowSpan(logoPictureBox, 5);
			logoPictureBox.Size = new System.Drawing.Size(118, 201);
			logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			logoPictureBox.TabIndex = 12;
			logoPictureBox.TabStop = false;
			// 
			// labelTitle
			// 
			tableLayoutPanel.SetColumnSpan(labelTitle, 2);
			labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			labelTitle.Location = new System.Drawing.Point(133, 0);
			labelTitle.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
			labelTitle.MaximumSize = new System.Drawing.Size(0, 20);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new System.Drawing.Size(350, 20);
			labelTitle.TabIndex = 19;
			labelTitle.Text = "Agile Prices";
			labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelProduct
			// 
			labelProduct.Dock = System.Windows.Forms.DockStyle.Fill;
			labelProduct.Location = new System.Drawing.Point(133, 29);
			labelProduct.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
			labelProduct.MaximumSize = new System.Drawing.Size(0, 20);
			labelProduct.Name = "labelProduct";
			labelProduct.Size = new System.Drawing.Size(99, 20);
			labelProduct.TabIndex = 0;
			labelProduct.Text = "Product Code";
			labelProduct.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// labelTariff
			// 
			labelTariff.Dock = System.Windows.Forms.DockStyle.Fill;
			labelTariff.Location = new System.Drawing.Point(133, 58);
			labelTariff.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
			labelTariff.MaximumSize = new System.Drawing.Size(0, 20);
			labelTariff.Name = "labelTariff";
			labelTariff.Size = new System.Drawing.Size(99, 20);
			labelTariff.TabIndex = 21;
			labelTariff.Text = "Tariff Code";
			labelTariff.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// labelAPI
			// 
			labelAPI.Dock = System.Windows.Forms.DockStyle.Fill;
			labelAPI.Location = new System.Drawing.Point(133, 87);
			labelAPI.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
			labelAPI.MaximumSize = new System.Drawing.Size(0, 20);
			labelAPI.Name = "labelAPI";
			labelAPI.Size = new System.Drawing.Size(99, 20);
			labelAPI.TabIndex = 22;
			labelAPI.Text = "API URL";
			labelAPI.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textBoxDescription
			// 
			tableLayoutPanel.SetColumnSpan(textBoxDescription, 2);
			textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			textBoxDescription.Location = new System.Drawing.Point(133, 119);
			textBoxDescription.Margin = new System.Windows.Forms.Padding(7, 3, 4, 3);
			textBoxDescription.Multiline = true;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ReadOnly = true;
			textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			textBoxDescription.Size = new System.Drawing.Size(350, 85);
			textBoxDescription.TabIndex = 23;
			textBoxDescription.TabStop = false;
			textBoxDescription.Text = "Enter the Tariff or Product codes from your account, or paste the API for Unit Rates (not consumption) from https://octopus.energy/dashboard/new/accounts/personal-details/api-access";
			// 
			// textProduct
			// 
			textProduct.Location = new System.Drawing.Point(239, 32);
			textProduct.Name = "textProduct";
			textProduct.Size = new System.Drawing.Size(229, 23);
			textProduct.TabIndex = 25;
			textProduct.Text = "AGILE-18-02-21";
			textProduct.TextChanged += textProduct_TextChanged;
			// 
			// textTariff
			// 
			textTariff.Location = new System.Drawing.Point(239, 61);
			textTariff.Name = "textTariff";
			textTariff.Size = new System.Drawing.Size(229, 23);
			textTariff.TabIndex = 26;
			textTariff.Text = "E-1R-AGILE-18-02-21-C";
			textTariff.TextChanged += textTariff_TextChanged;
			// 
			// textAPI
			// 
			textAPI.Location = new System.Drawing.Point(239, 90);
			textAPI.Name = "textAPI";
			textAPI.Size = new System.Drawing.Size(229, 23);
			textAPI.TabIndex = 27;
			textAPI.TextChanged += textAPI_TextChanged;
			// 
			// okButton
			// 
			okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			okButton.Location = new System.Drawing.Point(431, 277);
			okButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			okButton.Name = "okButton";
			okButton.Size = new System.Drawing.Size(66, 27);
			okButton.TabIndex = 24;
			okButton.Text = "&OK";
			// 
			// btnColourFg0
			// 
			btnColourFg0.Location = new System.Drawing.Point(9, 23);
			btnColourFg0.Name = "btnColourFg0";
			btnColourFg0.Size = new System.Drawing.Size(28, 23);
			btnColourFg0.TabIndex = 25;
			btnColourFg0.Tag = "0";
			btnColourFg0.Text = "T";
			btnColourFg0.UseVisualStyleBackColor = true;
			btnColourFg0.Click += btnColourFg0_Click;
			// 
			// textBoxThreshold0
			// 
			textBoxThreshold0.Location = new System.Drawing.Point(63, 52);
			textBoxThreshold0.Name = "textBoxThreshold0";
			textBoxThreshold0.Size = new System.Drawing.Size(62, 23);
			textBoxThreshold0.TabIndex = 4;
			textBoxThreshold0.Tag = "0";
			textBoxThreshold0.Text = "0";
			textBoxThreshold0.TextChanged += textBoxThreshold0_TextChanged;
			// 
			// btnColourBg0
			// 
			btnColourBg0.Location = new System.Drawing.Point(43, 23);
			btnColourBg0.Name = "btnColourBg0";
			btnColourBg0.Size = new System.Drawing.Size(28, 23);
			btnColourBg0.TabIndex = 27;
			btnColourBg0.Tag = "0";
			btnColourBg0.Text = "B";
			btnColourBg0.UseVisualStyleBackColor = true;
			btnColourBg0.Click += btnColourBg0_Click;
			// 
			// textBoxThreshold1
			// 
			textBoxThreshold1.Location = new System.Drawing.Point(166, 53);
			textBoxThreshold1.Name = "textBoxThreshold1";
			textBoxThreshold1.Size = new System.Drawing.Size(62, 23);
			textBoxThreshold1.TabIndex = 28;
			textBoxThreshold1.Tag = "1";
			textBoxThreshold1.Text = "0";
			textBoxThreshold1.TextChanged += textBoxThreshold1_TextChanged;
			// 
			// btnColourFg1
			// 
			btnColourFg1.Location = new System.Drawing.Point(112, 23);
			btnColourFg1.Name = "btnColourFg1";
			btnColourFg1.Size = new System.Drawing.Size(28, 23);
			btnColourFg1.TabIndex = 29;
			btnColourFg1.Tag = "1";
			btnColourFg1.Text = "T";
			btnColourFg1.UseVisualStyleBackColor = true;
			btnColourFg1.Click += btnColourFg1_Click;
			// 
			// btnColourBg1
			// 
			btnColourBg1.Location = new System.Drawing.Point(146, 23);
			btnColourBg1.Name = "btnColourBg1";
			btnColourBg1.Size = new System.Drawing.Size(28, 23);
			btnColourBg1.TabIndex = 30;
			btnColourBg1.Tag = "1";
			btnColourBg1.Text = "B";
			btnColourBg1.UseVisualStyleBackColor = true;
			btnColourBg1.Click += btnColourBg1_Click;
			// 
			// textBoxThreshold2
			// 
			textBoxThreshold2.Location = new System.Drawing.Point(269, 52);
			textBoxThreshold2.Name = "textBoxThreshold2";
			textBoxThreshold2.Size = new System.Drawing.Size(62, 23);
			textBoxThreshold2.TabIndex = 31;
			textBoxThreshold2.Tag = "2";
			textBoxThreshold2.Text = "0";
			textBoxThreshold2.TextChanged += textBoxThreshold2_TextChanged;
			// 
			// btnColourFg2
			// 
			btnColourFg2.Location = new System.Drawing.Point(215, 23);
			btnColourFg2.Name = "btnColourFg2";
			btnColourFg2.Size = new System.Drawing.Size(28, 23);
			btnColourFg2.TabIndex = 32;
			btnColourFg2.Tag = "2";
			btnColourFg2.Text = "T";
			btnColourFg2.UseVisualStyleBackColor = true;
			btnColourFg2.Click += btnColourFg2_Click;
			// 
			// btnColourBg2
			// 
			btnColourBg2.Location = new System.Drawing.Point(249, 23);
			btnColourBg2.Name = "btnColourBg2";
			btnColourBg2.Size = new System.Drawing.Size(28, 23);
			btnColourBg2.TabIndex = 33;
			btnColourBg2.Tag = "2";
			btnColourBg2.Text = "B";
			btnColourBg2.UseVisualStyleBackColor = true;
			btnColourBg2.Click += btnColourBg2_Click;
			// 
			// btnColourFg3
			// 
			btnColourFg3.Location = new System.Drawing.Point(318, 23);
			btnColourFg3.Name = "btnColourFg3";
			btnColourFg3.Size = new System.Drawing.Size(28, 23);
			btnColourFg3.TabIndex = 35;
			btnColourFg3.Tag = "3";
			btnColourFg3.Text = "T";
			btnColourFg3.UseVisualStyleBackColor = true;
			btnColourFg3.Click += btnColourFg3_Click;
			// 
			// btnColourBg3
			// 
			btnColourBg3.Location = new System.Drawing.Point(352, 23);
			btnColourBg3.Name = "btnColourBg3";
			btnColourBg3.Size = new System.Drawing.Size(28, 23);
			btnColourBg3.TabIndex = 36;
			btnColourBg3.Tag = "3";
			btnColourBg3.Text = "B";
			btnColourBg3.UseVisualStyleBackColor = true;
			btnColourBg3.Click += btnColourBg3_Click;
			// 
			// textBoxThreshold3
			// 
			textBoxThreshold3.Location = new System.Drawing.Point(435, 220);
			textBoxThreshold3.Name = "textBoxThreshold3";
			textBoxThreshold3.ReadOnly = true;
			textBoxThreshold3.Size = new System.Drawing.Size(62, 23);
			textBoxThreshold3.TabIndex = 34;
			textBoxThreshold3.Tag = "3";
			textBoxThreshold3.Text = "999";
			textBoxThreshold3.Visible = false;
			// 
			// groupBoxThresholds
			// 
			groupBoxThresholds.Controls.Add(textBoxThreshold1);
			groupBoxThresholds.Controls.Add(btnColourFg3);
			groupBoxThresholds.Controls.Add(btnColourBg0);
			groupBoxThresholds.Controls.Add(btnColourBg3);
			groupBoxThresholds.Controls.Add(btnColourFg0);
			groupBoxThresholds.Controls.Add(textBoxThreshold2);
			groupBoxThresholds.Controls.Add(textBoxThreshold0);
			groupBoxThresholds.Controls.Add(btnColourFg2);
			groupBoxThresholds.Controls.Add(btnColourBg1);
			groupBoxThresholds.Controls.Add(btnColourBg2);
			groupBoxThresholds.Controls.Add(btnColourFg1);
			groupBoxThresholds.Location = new System.Drawing.Point(14, 223);
			groupBoxThresholds.Name = "groupBoxThresholds";
			groupBoxThresholds.Size = new System.Drawing.Size(389, 82);
			groupBoxThresholds.TabIndex = 37;
			groupBoxThresholds.TabStop = false;
			groupBoxThresholds.Text = "Thresholds";
			// 
			// Preferences
			// 
			AcceptButton = okButton;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(507, 314);
			Controls.Add(textBoxThreshold3);
			Controls.Add(okButton);
			Controls.Add(tableLayoutPanel);
			Controls.Add(groupBoxThresholds);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "Preferences";
			Padding = new System.Windows.Forms.Padding(10);
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Preferences";
			FormClosed += Preferences_FormClosed;
			Load += Preferences_Load;
			tableLayoutPanel.ResumeLayout(false);
			tableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
			groupBoxThresholds.ResumeLayout(false);
			groupBoxThresholds.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.PictureBox logoPictureBox;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Label labelProduct;
		private System.Windows.Forms.Label labelTariff;
		private System.Windows.Forms.Label labelAPI;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TextBox textProduct;
		private System.Windows.Forms.TextBox textTariff;
		private System.Windows.Forms.TextBox textAPI;
		private System.Windows.Forms.TextBox textBoxThreshold0;
		private System.Windows.Forms.Button btnColourFg0;
		private System.Windows.Forms.Button btnColourBg0;
		private System.Windows.Forms.TextBox textBoxThreshold1;
		private System.Windows.Forms.Button btnColourFg1;
		private System.Windows.Forms.Button btnColourBg1;
		private System.Windows.Forms.TextBox textBoxThreshold2;
		private System.Windows.Forms.Button btnColourFg2;
		private System.Windows.Forms.Button btnColourBg2;
		private System.Windows.Forms.Button btnColourFg3;
		private System.Windows.Forms.Button btnColourBg3;
		private System.Windows.Forms.TextBox textBoxThreshold3;
		private System.Windows.Forms.GroupBox groupBoxThresholds;
	}
}
