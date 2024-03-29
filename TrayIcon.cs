﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using OctopusAgileNotification.Properties;

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

		private readonly Thresholds thresholds;
		private float currentPrice;

		private NotifyContext context;

		// propagate mouseclicks up to the parent
		public event MouseEventHandler MouseClick
		{
			add { notifyIcon.MouseClick += value; }
			remove { notifyIcon.MouseClick -= value; }
		}


		public TrayIcon(NotifyContext ctx)
		{
			context = ctx;
			notifyIcon = new NotifyIcon()
			{
				Icon = Resources.Constantine32,
				ContextMenuStrip = new ContextMenuStrip()
				{
					Items = { 
						new ToolStripMenuItem("Log", null, ShowLog),
						new ToolStripMenuItem("Settings", null, ChangeSettings),
						new ToolStripMenuItem("Exit", null, Exit)
					}
				},
				Visible = true,
			};

			thresholds = new Thresholds();
		}


		// Change the value shown on the tray icon
		public void SetTextIcon(float price)
		{
			currentPrice = price;
			ColourSettings currentColour = thresholds.GetColours(price);

			notifyIcon.Text = $"{price:F2}p";

			using Brush brushToUse = new SolidBrush(currentColour.textColour);
			using Bitmap bitmapText = new Bitmap(SystemInformation.IconSize.Width, SystemInformation.IconSize.Height);
			using Graphics g = Graphics.FromImage(bitmapText);

			g.Clear(currentColour.backColour);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			var text = Math.Round(price).ToString("F0");
			float valWidth = g.MeasureString(text, thresholds.GetFont()).Width;
			g.DrawString(text, thresholds.GetFont(), brushToUse, (SystemInformation.IconSize.Width-valWidth)/2F, 1);

			// get the icon via Win32, clone it to be managed and then destroy our original
			IntPtr hIcon = bitmapText.GetHicon();
			Icon newIcon = Icon.FromHandle(hIcon);
			newIcon = newIcon.Clone() as Icon;
			DestroyIcon(hIcon);

			notifyIcon.Icon = newIcon;
		}


		void ShowLog(object sender, EventArgs e)
		{
			LogUI logUI = new LogUI(context.logger);
			logUI.Show();
		}

		void ChangeSettings(object sender, EventArgs e)
		{
			Preferences settingsForm = new Preferences();
			settingsForm.ShowDialog();

			thresholds.Refresh();

			SetTextIcon(currentPrice);
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
