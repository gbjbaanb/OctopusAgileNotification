using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using OctopusAgileNotification.Properties;
using ServiceStack;
using static ServiceStack.Svg;

namespace OctopusAgileNotification
{
	/// <summary>
	/// class wrapping a little icon in the taskbar's notify area.
	/// This updates every half hour with current Octopus Agile price data, and fetches new data every day at around 4pm
	/// </summary>
	internal class TrayIcon : IDisposable
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		extern static bool DestroyIcon(IntPtr handle);

		private readonly NotifyIcon notifyIcon;
		private bool disposedValue;

		private ColourSettings[] colours;
		private Font thresholdFont;
		private float currentPrice;

		public TrayIcon()
		{
			notifyIcon = new NotifyIcon()
			{
				Icon = Resources.Constantine32,
				ContextMenuStrip = new ContextMenuStrip()
				{
					Items = { 
						new ToolStripMenuItem("Settings", null, ChangeSettings),
						new ToolStripMenuItem("Exit", null, Exit)
					}
				},
				Visible = true,
			};

			notifyIcon.Click += new EventHandler(Click);

			try
			{
				var options = new JsonSerializerOptions() { Converters = { new ColorJsonConverter() } };
				colours = JsonSerializer.Deserialize<ColourSettings[]>(Settings.Default.Thresholds, options);
			}
			catch (Exception)
			{
				// set defaults
				colours =
				[
					new ColourSettings() { backColour = Color.Blue, textColour = Color.White, threshold = 0 },
					new ColourSettings() { backColour = Color.Transparent, textColour = Color.Green, threshold = 15 },
					new ColourSettings() { backColour = Color.Orange, textColour = Color.Black, threshold = 24 },
					new ColourSettings() { backColour = Color.Red, textColour = Color.White, threshold = 999 },
				];
			}

			try
			{
				TypeConverter cvt = TypeDescriptor.GetConverter(typeof(Font));
				thresholdFont = (Font)cvt.ConvertFromInvariantString(Settings.Default.Font);
			}
			catch (Exception)
			{
				thresholdFont = new Font("Segoe UI", SystemInformation.IconSize.Height * 8 / 10, FontStyle.Regular, GraphicsUnit.Pixel);
			}
		}


		private ColourSettings GetColours(float price)
		{
			foreach (var c in colours)
			{
				if (c.threshold >= price)
					return c;
			}
			return colours[0];
		}


		// Change the value shown on the tray icon
		public void SetTextIcon(float price)
		{
			currentPrice = price;
			ColourSettings currentColour = GetColours(price);

			notifyIcon.Text = $"{price:F2}p";

			using Brush brushToUse = new SolidBrush(currentColour.textColour);
			using Bitmap bitmapText = new Bitmap(SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);
			using Graphics g = Graphics.FromImage(bitmapText);

			g.Clear(currentColour.backColour);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			g.DrawString(Math.Round(price).ToString("F0"), thresholdFont, brushToUse, 0, 0);

			// get the icon via Win32, clone it to be managed and then destroy our original
			IntPtr hIcon = bitmapText.GetHicon();
			Icon newIcon = Icon.FromHandle(hIcon);
			newIcon = newIcon.Clone() as Icon;
			DestroyIcon(hIcon);

			notifyIcon.Icon = newIcon;
		}


		void Click(object sender, EventArgs e)
		{
			/*			System.Drawing.Size windowSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
						System.Drawing.Point menuPoint = new System.Drawing.Point(windowSize.Width - 180, windowSize.Height - 5);
						menuPoint = this.PointToClient(menuPoint);
						NotifyIcon1.ContextMenu.Show(this, menuPoint);*/

		}


		void ChangeSettings(object sender, EventArgs e)
		{
			Preferences settingsForm = new Preferences();
			settingsForm.ShowDialog();

			// fetch the changes from global settings
			try
			{
				var options = new JsonSerializerOptions() { Converters = { new ColorJsonConverter() } };
				colours = JsonSerializer.Deserialize<ColourSettings[]>(Settings.Default.Thresholds, options);

				TypeConverter cvt = TypeDescriptor.GetConverter(typeof(Font));
				thresholdFont = (Font)cvt.ConvertFromInvariantString(Settings.Default.Font);
			}
			catch (Exception)
			{
				// ignore and leave previous values
			}

			SetTextIcon(currentPrice);
		}


		public void Exit(object sender, EventArgs e)
		{
			thresholdFont.Dispose();
			Application.Exit();
		}



		// Dispose handler garbage
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					notifyIcon.Click -= new EventHandler(Click);
					notifyIcon.Visible = false;
					notifyIcon.Dispose();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
