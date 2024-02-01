using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

		private readonly ColourSettings[] colours;
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

			// set defaults
			colours =
			[
				new ColourSettings() { backColour = Color.Blue,			textColour = Color.White,	threshold = 0 },
				new ColourSettings() { backColour = Color.Transparent,	textColour = Color.Green,	threshold = 15 },
				new ColourSettings() { backColour = Color.Orange,		textColour = Color.Black,	threshold = 24 },
				new ColourSettings() { backColour = Color.Red,			textColour = Color.White,	threshold = 999 },
			];
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

			using Font fontDecimal = new Font("MS Sans Serif", 30, FontStyle.Regular, GraphicsUnit.Pixel);
			using Brush brushToUse = new SolidBrush(currentColour.textColour);
			using Bitmap bitmapText = new Bitmap(32, 32);
			using Graphics g = Graphics.FromImage(bitmapText);

			g.Clear(currentColour.backColour);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
			g.DrawString(Math.Round(price).ToString("F0"), fontDecimal, brushToUse, -6, -2);

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
			settingsForm.ThresholdChanged += UpdateThresholds;
			settingsForm.ShowDialog();
			settingsForm.ThresholdChanged -= UpdateThresholds;


			SetTextIcon(currentPrice);
		}


		private void UpdateThresholds(object sender, ChangeThresholdEventArgs e)
		{
			if (e.bgColour != null) colours[e.level].backColour = (Color)e.bgColour;
			if (e.fgColour != null) colours[e.level].textColour = (Color)e.fgColour;
			colours[e.level].threshold = e.threshold;
		}

		public void Exit(object sender, EventArgs e)
		{
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
