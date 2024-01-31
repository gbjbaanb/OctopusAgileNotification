﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OctopusAgileNotification.Properties;
using static ServiceStack.Svg;

namespace OctopusAgileNotification
{
	internal class ColourSettings
	{
		public Color text { get; set; }
		public Color background { get; set; }
		public int threshold { get; set; }
	}


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

		List<ColourSettings> colours;


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
			colours = new List<ColourSettings>()
			{
				new ColourSettings() { background = Color.Blue,			text = Color.White,	threshold = 0 },
				new ColourSettings() { background = Color.Transparent,	text = Color.Green,	threshold = 15 },
				new ColourSettings() { background = Color.Orange,		text = Color.Black,	threshold = 24 },
				new ColourSettings() { background = Color.Red,			text = Color.White,	threshold = 999 },
			};
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
			ColourSettings currentColour = GetColours(price);

			notifyIcon.Text = $"{price:F2}p";

			using Font fontDecimal = new Font("MS Sans Serif", 30, FontStyle.Regular, GraphicsUnit.Pixel);
			using Brush brushToUse = new SolidBrush(currentColour.text);
			using Bitmap bitmapText = new Bitmap(32, 32);
			using Graphics g = Graphics.FromImage(bitmapText);

			g.Clear(currentColour.background);
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
			Form settingsForm = new Form();
			settingsForm.ShowDialog();
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
