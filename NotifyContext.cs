using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;


namespace OctopusAgileNotification
{
	public class NotifyContext : ApplicationContext
	{
		private readonly System.Timers.Timer timerNext30 = new System.Timers.Timer();
		private readonly System.Timers.Timer timerRefreshPrices = new System.Timers.Timer();

		private readonly PriceFetch dataFetcher;
		private readonly TrayIcon trayIcon;

		public NotifyContext()
		{
			Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

			dataFetcher = new PriceFetch();
			trayIcon = new TrayIcon();

			dataFetcher.GetPrices();

			trayIcon.SetTextIcon(dataFetcher.GetCurrentPrice());

			// set the next time to trigger at the 30-min or 60-min mark. 
			int Next30 = 60 - DateTime.Now.Minute;
			if (Next30 > 30)
				Next30 -= 30;

			timerNext30.Interval = new TimeSpan(0, Next30, 0).TotalMilliseconds;
			timerNext30.AutoReset = false;
			timerNext30.Elapsed += TimerNext30_Elapsed;
			timerNext30.Start();

			timerRefreshPrices.Interval = GetNext4pmInMs();
			timerRefreshPrices.AutoReset = false;
			timerRefreshPrices.Elapsed += TimerRefreshPrices_Elapsed;
			timerRefreshPrices.Start();
		}


		private static double GetNext4pmInMs()
		{
			DateTime tomorrow = DateTime.Now.AddDays(1);
			DateTime tomorrow4PM = new DateTime(new DateOnly(tomorrow.Year, tomorrow.Month, tomorrow.Day), new TimeOnly(16, 1), DateTimeKind.Local);
			return tomorrow4PM.Subtract(DateTime.Now).TotalMilliseconds;
		}


		// Get the next days prices.
		// If we fail to dataFetcher new ones, try again in an hour, otherwise wait until tomorrow's update
		private void TimerRefreshPrices_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (dataFetcher.GetPrices())
				timerRefreshPrices.Interval = GetNext4pmInMs();
			else
				timerRefreshPrices.Interval = 60 * 60 * 1000; // 1 hr in milliseconds
			timerRefreshPrices.Start();
		}


		// triggered once to get to the next 30 min mark, the resets itself to trigger every 30 min
		private void TimerNext30_Elapsed(object sender, ElapsedEventArgs e)
		{
			trayIcon.SetTextIcon(dataFetcher.GetCurrentPrice());
			timerNext30.Interval = new TimeSpan(0, 30, 0).TotalMilliseconds;
			timerNext30.Start();
		}

		

		public void OnApplicationExit(object sender, EventArgs e)
		{
			timerNext30.Stop();
			timerNext30.Dispose();
			timerRefreshPrices.Stop();
			timerRefreshPrices.Dispose();

			trayIcon.Dispose();

			Application.ApplicationExit -= new EventHandler(this.OnApplicationExit);
		}

	}
}
