using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;


namespace OctopusAgileNotification
{

	// yeah, about this....
	// a bunch of utility functions felched off the web to manage dark mode colouring.
	// I remember the old days when Windows programming was actually good and consistent
	// and not a massive garbage heap of junk thrown all over the place.
	internal class WinColours
	{

		// check for dark mode, or not = return 0 for dak, 1 for light.
		public static int GetWindowsColorMode()
		{
			try
			{
				return (int)Microsoft.Win32.Registry.GetValue(
					@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize",
					"SystemUsesLightTheme",
					-1);
			}
			catch
			{
				return 1;
			}
		}

		// how do you know what dark mode colour is? You don't!
		// so pick a pixel off thtaskbar and use that as the background colour for the popup form.
		private struct APPBARDATA
		{
			public int cbSize;
			public IntPtr hWnd;
			public int uCallbackMessage;
			public int uEdge;
			public RECT rc;
			public IntPtr lParam;
		}

		private struct RECT
		{
			public int left, top, right, bottom;
		}

		private const int ABM_GETTASKBARPOS = 5;

		[DllImport("shell32.dll")]
		private static extern IntPtr SHAppBarMessage(int msg, ref APPBARDATA data);

		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
		private static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

		public static Rectangle GetTaskbarPosition()
		{
			APPBARDATA data = new APPBARDATA();
			data.cbSize = Marshal.SizeOf(data);

			IntPtr retval = SHAppBarMessage(ABM_GETTASKBARPOS, ref data);
			if (retval == IntPtr.Zero)
			{
				throw new Win32Exception("Please re-install Windows");
			}

			return new Rectangle(data.rc.left, data.rc.top, data.rc.right - data.rc.left, data.rc.bottom - data.rc.top);
		}

		public static Color GetColourAt(Point location)
		{
			using (Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb))
			using (Graphics gdest = Graphics.FromImage(screenPixel))
			{
				using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
				{
					IntPtr hSrcDC = gsrc.GetHdc();
					IntPtr hDC = gdest.GetHdc();
					int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
					gdest.ReleaseHdc();
					gsrc.ReleaseHdc();
				}

				return screenPixel.GetPixel(0, 0);
			}
		}



		// and then its not enough to get the background colour, you also need to workout a constrasting one for text. 
		public static (double, double) GetRelativeLuminance(int r, int g, int b)
		{
			double R = r / 255.0;
			double G = g / 255.0;
			double B = b / 255.0;

			double L = 0.2126 * R + 0.7152 * G + 0.0722 * B;

			return (Math.Max(L, 0.0), Math.Min(L, 1.0));
		}

		public static Color GetContrastingColor(Color color)
		{
			int r = color.R;
			int g = color.G;
			int b = color.B;

			(double L1, double L2) = GetRelativeLuminance(r, g, b);

			double L_max = Math.Max(L1, 1.0 - L2);
			double L_min = Math.Min(L1, 1.0 - L2);

			double contrastRatio = (L_max + 0.05) / (L_min + 0.05);

			if (contrastRatio < 4.5)
			{
				// Swap foreground and background colors
				(L1, L2) = (L2, L1);
			}

			double R = (L1 - 0.05) / (L1 + L2 - 0.1);
			double G = (L1 - 0.05) / (L1 + L2 - 0.1);
			double B = (L1 - 0.05) / (L1 + L2 - 0.1);

			return Color.FromArgb(
				(int)Math.Round(Math.Min(Math.Max(R * 255, 0), 255)),
				(int)Math.Round(Math.Min(Math.Max(G * 255, 0), 255)),
				(int)Math.Round(Math.Min(Math.Max(B * 255, 0), 255)));
		}


		// and another method that seems to work just as well.
		public static Color CalcContrastColor(Color crBg)
		{
			return Color.FromArgb((crBg.ToArgb() ^ 0x808080));
		}
	}
}
