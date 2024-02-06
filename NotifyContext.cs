using System;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Win32;
using OctopusAgileNotification.Properties;


namespace OctopusAgileNotification
{
	public class NotifyContext : ApplicationContext
	{
		private readonly System.Timers.Timer timerNext30 = new System.Timers.Timer();
		private readonly System.Timers.Timer timerRefreshPrices = new System.Timers.Timer();

		private readonly PriceFetch dataFetcher;
		private readonly TrayIcon trayIcon;
		private PriceList priceList = null;


		public NotifyContext()
		{
			Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
			SystemEvents.PowerModeChanged += OnPowerChange;

			dataFetcher = new PriceFetch();
			trayIcon = new TrayIcon();

			trayIcon.MouseClick += new MouseEventHandler(this.TrayIconClick);

			if (dataFetcher.FetchPrices())
				trayIcon.SetTextIcon(dataFetcher.GetCurrentPrice());

			// timer to rupdate the UI with current price
			timerNext30.Interval = GetNext30MinInMs();
			timerNext30.AutoReset = false;
			timerNext30.Elapsed += TimerNext30_Elapsed;
			timerNext30.Start();

			timerRefreshPrices.Interval = GetNext4pmInMs();
			timerRefreshPrices.AutoReset = false;
			timerRefreshPrices.Elapsed += TimerRefreshPrices_Elapsed;
			timerRefreshPrices.Start();
		}


		private void TrayIconClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (priceList == null)
				{
					priceList = new(dataFetcher.GetPrices());
					priceList.Show();
					priceList.ActiveControl = null;
					if (!Settings.Default.ClickToClose)
						priceList.LostFocus += new EventHandler(LostFocus);
				}
				else
				{
					priceList.Close();
					priceList = null;
				}
			}
		}

		private void LostFocus(object sender, EventArgs e)
		{
			Form f = sender as Form;
			priceList.LostFocus -= new EventHandler(LostFocus);
			f.Close();
			f.Dispose();
			priceList = null;
		}


		private static double GetNext30MinInMs()
		{
			// set the next time to trigger at the 30-min or 60-min mark. 
			int Next30 = 60 - DateTime.Now.Minute;
			if (Next30 > 30)
				Next30 -= 30;
			return (Next30 * 60 * 1000) - (DateTime.Now.Second * 1000);
		}

		private static double GetNext4pmInMs()
		{
			// check the next 4pm which may be today if the PC has been asleep too long.
			DateTime tomorrow = DateTime.Now;
			if (DateTime.Now.TimeOfDay >= new TimeSpan(16,0,0))
				tomorrow = tomorrow.AddDays(1);
			DateTime tomorrow4PM = new DateTime(new DateOnly(tomorrow.Year, tomorrow.Month, tomorrow.Day), new TimeOnly(16, 1), DateTimeKind.Local);
			return tomorrow4PM.Subtract(DateTime.Now).TotalMilliseconds;
		}


		// Get the next days prices.
		// If we fail to dataFetcher new ones, try again in an hour, otherwise wait until tomorrow's update
		private void TimerRefreshPrices_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (dataFetcher.FetchPrices())
			{
				timerRefreshPrices.Interval = GetNext4pmInMs();
				if (priceList != null)
					priceList.Invoke(new MethodInvoker(delegate () { priceList.UpdatePrices(dataFetcher.GetPrices()); }));
			}
			else
				timerRefreshPrices.Interval = 60 * 60 * 1000; // 1 hr in milliseconds
			timerRefreshPrices.Start();
		}


		// triggered once to get to the next 30 min mark, the resets itself to trigger every 30 min
		private void TimerNext30_Elapsed(object sender, ElapsedEventArgs e)
		{
			trayIcon.SetTextIcon(dataFetcher.GetCurrentPrice());
			if (priceList != null)
				priceList.Invoke(new MethodInvoker(delegate() { priceList.RemoveLastEntry(); }));
			timerNext30.Interval = new TimeSpan(0, 30, 0).TotalMilliseconds;
			timerNext30.Start();
		}


		private void OnPowerChange(object s, PowerModeChangedEventArgs e)
		{
			switch (e.Mode)
			{
				case PowerModes.Resume:
					// reset the timers and update the current display
					timerNext30.Interval = GetNext30MinInMs();
					timerRefreshPrices.Interval = GetNext4pmInMs();
					if (dataFetcher.GetCurrentPrice() == 0)
						dataFetcher.FetchPrices();
					trayIcon.SetTextIcon(dataFetcher.GetCurrentPrice());
					break;
				case PowerModes.Suspend:
					break;
			}
		}


		public void OnApplicationExit(object sender, EventArgs e)
		{
			timerNext30.Stop();
			timerNext30.Dispose();
			timerRefreshPrices.Stop();
			timerRefreshPrices.Dispose();

			trayIcon.Dispose();

			trayIcon.MouseClick -= new MouseEventHandler(this.TrayIconClick);
			SystemEvents.PowerModeChanged -= OnPowerChange;
			Application.ApplicationExit -= new EventHandler(this.OnApplicationExit);
		}

	}
}
