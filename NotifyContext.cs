using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Win32;
using OctopusAgileNotification.Properties;


namespace OctopusAgileNotification
{
	public class NotifyContext : ApplicationContext
	{
		private readonly System.Timers.Timer timerNext30 = new();
		private readonly System.Timers.Timer timerRefreshPrices = new();

		private readonly PriceFetch dataFetcher;
		private readonly TrayIcon trayIcon;
		private PriceList priceList = null;
		internal readonly FixedSizedQueue logger = new(100);

		public NotifyContext()
		{
			Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
			SystemEvents.PowerModeChanged += OnPowerChange;

			dataFetcher = new PriceFetch();
			trayIcon = new TrayIcon(this);

			trayIcon.MouseClick += new MouseEventHandler(this.TrayIconClick);

			if (dataFetcher.FetchPrices())
				trayIcon.SetTextIcon(dataFetcher.GetCurrentPrice());
			else
				logger.Enqueue("fetched prices OK");

			// timer to rupdate the UI with current price
			timerNext30.Interval = GetNext30MinInMs();
			timerNext30.AutoReset = true;
			timerNext30.Elapsed += TimerNext30_Elapsed;
			timerNext30.Start();
			logger.Enqueue($"UI update set tor {DateTime.Now.AddMilliseconds(timerNext30.Interval):t}");


			timerRefreshPrices.Interval = GetNext4pmInMs();
			timerRefreshPrices.AutoReset = true;
			timerRefreshPrices.Elapsed += TimerRefreshPrices_Elapsed;
			timerRefreshPrices.Start();
			logger.Enqueue($"Price update set tor {DateTime.Now.AddMilliseconds(timerRefreshPrices.Interval):g}");
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
					logger.Enqueue($"Show UI");
				}
				else
				{
					priceList.Close();
					priceList = null;
					logger.Enqueue($"Close UI");
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
			logger.Enqueue($"Autoclose UI");
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
			{
				timerRefreshPrices.Interval = 60 * 60 * 1000; // 1 hr in milliseconds
				logger.Enqueue($"Failed to fetch prices");
			}

			logger.Enqueue($"Price update set tor {DateTime.Now.AddMilliseconds(timerRefreshPrices.Interval):g}");
		}


		// triggered once to get to the next 30 min mark, the resets itself to trigger every 30 min
		private void TimerNext30_Elapsed(object sender, ElapsedEventArgs e)
		{
			trayIcon.SetTextIcon(dataFetcher.GetCurrentPrice());

			if (priceList != null)
				priceList.Invoke(new MethodInvoker(delegate() { priceList.RemoveLastEntry(); }));

			// reset the timer to trigger 30 seconds from now on
			timerNext30.Interval = new TimeSpan(0, 30, 0).TotalMilliseconds;
			logger.Enqueue($"UI update set tor {DateTime.Now.AddMilliseconds(timerNext30.Interval):t}");
		}


		private void OnPowerChange(object s, PowerModeChangedEventArgs e)
		{
			if (e.Mode == PowerModes.Resume)
			{
				// timers *should* auto-refresh if missed, but they don't. If wake after 4pm and not fetched tomorrow's data, get it now.
				if (DateTime.Now.Hour > 16 && dataFetcher.GetPrices().lastFetched.Day != DateTime.Now.Day)
					dataFetcher.FetchPrices();

				// fetch more data if we've been asleep for long time
				if (dataFetcher.GetCurrentPrice() == 0)
					dataFetcher.FetchPrices();

				// we need the 30min to reset at the 30-min mark, not 30 min from wake time
				timerNext30.Interval = GetNext30MinInMs();
				timerRefreshPrices.Interval = GetNext4pmInMs();

				trayIcon.SetTextIcon(dataFetcher.GetCurrentPrice());

				if (priceList != null)
					priceList.Invoke(new MethodInvoker(delegate () { priceList.UpdatePrices(dataFetcher.GetPrices()); }));

				logger.Enqueue($"Resumed from sleep. UI update set tor {DateTime.Now.AddMilliseconds(timerNext30.Interval):t}, Price update set for {DateTime.Now.AddMilliseconds(timerRefreshPrices.Interval):g}");
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
