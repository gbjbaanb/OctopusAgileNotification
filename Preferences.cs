using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using OctopusAgileNotification.Properties;
using ServiceStack;

namespace OctopusAgileNotification
{
	partial class Preferences : Form
	{
		private ColourSettings[] thresholdPrefs;
		private Font thresholdFont = null;

		public Preferences()
		{
			InitializeComponent();
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion

		private Regex parser = new Regex("(?<baseapi>https[A-Za-z0-9:\\./-]+)/(?<product>[A-Za-z0-9-]+)/electricity-tariffs/(?<tariff>[A-Za-z0-9-]+)/");

		private void textProduct_TextChanged(object sender, EventArgs e)
		{
			Settings.Default.ProductCode = textProduct.Text;
		}

		private void textTariff_TextChanged(object sender, EventArgs e)
		{
			Settings.Default.TariffCode = textTariff.Text;
		}

		private void textAPI_TextChanged(object sender, EventArgs e)
		{
			// parse the input into base, product and tariff
			Match m = parser.Match(textAPI.Text);

			if (m.Success)
			{
				Settings.Default.OctopusBaseURL = m.Groups[1].Captures[0].Value;
				textProduct.Text = m.Groups[2].Captures[0].Value;
				textTariff.Text = m.Groups[3].Captures[0].Value;
			}

			// https://api.octopus.energy/v1/products/AGILE-FLEX-22-11-25/electricity-tariffs/E-1R-AGILE-FLEX-22-11-25-H/standard-unit-rates/?period_from=2024-01-31T15:37:19
		}


		private void btnFont_Click(object sender, EventArgs e)
		{
			FontDialog fnt = new FontDialog();
			fnt.ShowEffects = false;
			fnt.FontMustExist = true;

			try
			{
				TypeConverter cvt = TypeDescriptor.GetConverter(typeof(Font));
				fnt.Font = (Font)cvt.ConvertFromInvariantString(Settings.Default.Font);
			}
			catch (Exception)
			{
				//default
				fnt.Font = new Font("Segoe UI", SystemInformation.IconSize.Height * 8 / 10, FontStyle.Regular, GraphicsUnit.Pixel);
			}

			if (fnt.ShowDialog() == DialogResult.OK && fnt.Font != null)
				thresholdFont = fnt.Font;
		}


		private void Preferences_FormClosed(object sender, FormClosedEventArgs e)
		{
			var options = new JsonSerializerOptions() { Converters = { new ColorJsonConverter() } };
			try
			{
				string thresholds = JsonSerializer.Serialize(thresholdPrefs, options);
				Settings.Default.Thresholds = thresholds;

				if (thresholdFont != null)
				{
					TypeConverter cvt = TypeDescriptor.GetConverter(typeof(Font));
					Settings.Default.Font = cvt.ConvertToInvariantString(thresholdFont);
				}
			}
			catch (Exception)
			{
				// nothing to do, we'll reset to previous or defaults later
			}
		}

		private void Preferences_Load(object sender, EventArgs e)
		{
			textProduct.Text = Settings.Default.ProductCode;
			textTariff.Text = Settings.Default.TariffCode;

			// load thresholds
			try
			{
				var options = new JsonSerializerOptions() { Converters = { new ColorJsonConverter() } };
				thresholdPrefs = JsonSerializer.Deserialize<ColourSettings[]>(Settings.Default.Thresholds, options);
			}
			catch (Exception)
			{
				// set defaults
				thresholdPrefs =
				[
					new ColourSettings() { backColour = Color.Blue, textColour = Color.White, threshold = 0 },
					new ColourSettings() { backColour = Color.Transparent, textColour = Color.Green, threshold = 15 },
					new ColourSettings() { backColour = Color.Orange, textColour = Color.Black, threshold = 24 },
					new ColourSettings() { backColour = Color.Red, textColour = Color.White, threshold = 999 },
				];
			}

			// update controls
			textBoxThreshold0.Text = thresholdPrefs[0].threshold.ToString();
			btnColourFg0.BackColor = thresholdPrefs[0].textColour;
			btnColourBg0.BackColor = thresholdPrefs[0].backColour;
			textBoxThreshold1.Text = thresholdPrefs[1].threshold.ToString();
			btnColourFg1.BackColor = thresholdPrefs[1].textColour;
			btnColourBg1.BackColor = thresholdPrefs[1].backColour;
			textBoxThreshold2.Text = thresholdPrefs[2].threshold.ToString();
			btnColourFg2.BackColor = thresholdPrefs[2].textColour;
			btnColourBg2.BackColor = thresholdPrefs[2].backColour;
			textBoxThreshold3.Text = thresholdPrefs[3].threshold.ToString();
			btnColourFg3.BackColor = thresholdPrefs[3].textColour;
			btnColourBg3.BackColor = thresholdPrefs[3].backColour;
		}



		private void textBoxThreshold0_TextChanged(object sender, EventArgs e)
		{
			thresholdPrefs[0].threshold = ((TextBox)sender).Text.ToInt();
		}

		private void textBoxThreshold1_TextChanged(object sender, EventArgs e)
		{
			thresholdPrefs[1].threshold = ((TextBox)sender).Text.ToInt();
		}

		private void textBoxThreshold2_TextChanged(object sender, EventArgs e)
		{
			thresholdPrefs[2].threshold = ((TextBox)sender).Text.ToInt();
		}


		private void ClickBackgroundBtn(object sender, int lev)
		{
			ColorPickerDialog dlg = new ();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				thresholdPrefs[lev].backColour = dlg.Color;
				((Button)sender).BackColor = dlg.Color;
			}
		}
		private void ClickForegroundBtn(object sender, int lev)
		{
			ColorPickerDialog dlg = new ();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				thresholdPrefs[lev].textColour = dlg.Color;
				((Button)sender).ForeColor = dlg.Color;
			}
		}

		private void btnColourFg0_Click(object sender, EventArgs e) { ClickForegroundBtn(sender, 0); }
		private void btnColourBg0_Click(object sender, EventArgs e) { ClickBackgroundBtn(sender, 0); }
		private void btnColourFg1_Click(object sender, EventArgs e) { ClickForegroundBtn(sender, 1); }
		private void btnColourBg1_Click(object sender, EventArgs e) { ClickBackgroundBtn(sender, 1); }
		private void btnColourFg2_Click(object sender, EventArgs e) { ClickForegroundBtn(sender, 2); }
		private void btnColourBg2_Click(object sender, EventArgs e) { ClickBackgroundBtn(sender, 2); }
		private void btnColourFg3_Click(object sender, EventArgs e) { ClickForegroundBtn(sender, 3); }
		private void btnColourBg3_Click(object sender, EventArgs e) { ClickBackgroundBtn(sender, 3); }

	}
}
