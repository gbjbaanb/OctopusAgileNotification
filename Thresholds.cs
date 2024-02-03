using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;
using OctopusAgileNotification.Properties;

namespace OctopusAgileNotification
{
	internal class Thresholds : IDisposable
	{
		private ColourSettings[] colours;
		private Font thresholdFont = null;
		private bool disposedValue;

		public Thresholds()
		{
			Refresh();
		}

		public void Refresh()
		{ 
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
				if (thresholdFont != null)
					thresholdFont.Dispose();

				TypeConverter cvt = TypeDescriptor.GetConverter(typeof(Font));
				thresholdFont = (Font)cvt.ConvertFromInvariantString(Settings.Default.Font);
			}
			catch (Exception)
			{
				thresholdFont = new Font("Segoe UI", SystemInformation.IconSize.Height * 8 / 10, FontStyle.Regular, GraphicsUnit.Pixel);
			}
		}

		public ColourSettings GetColours(float price)
		{
			foreach (var c in colours)
			{
				if (c.threshold >= price)
					return c;
			}
			return colours[0];
		}

		public Font GetFont()
		{
			return thresholdFont;
		}



		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					thresholdFont.Dispose();
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
