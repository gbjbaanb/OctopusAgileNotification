using System;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using OctopusAgileNotification.Properties;

namespace OctopusAgileNotification
{
	internal class PriceFetch
	{
		private JsonPriceOverview prices;

		private readonly HttpClient _httpClient;


		public PriceFetch()
		{
			_httpClient = new() { BaseAddress = new Uri(Settings.Default.OctopusBaseURL) };
		}

		// returns true if prices were fetched and changed from previous set
		public bool FetchPrices()
		{
			bool ret = false;
			
			try
			{
				using var task = _httpClient.GetAsync($"{Settings.Default.ProductCode}/electricity-tariffs/{Settings.Default.TariffCode}/standard-unit-rates/?period_from={DateTime.UtcNow:u}");
				if (!task.Wait(10000))
					return false;

				if (task.IsCompletedSuccessfully)
				{
					var jsonResponse = task.Result.Content.ReadAsStringAsync();
					if (task.Result.IsSuccessStatusCode)
					{
						DateTime lastHighestTime = DateTime.Now;
						if (prices != null && prices.results != null && prices.results[0] != null)
						{
							 lastHighestTime = prices.results[0].valid_from;
						}

						if (jsonResponse.Wait(10000))
						{
							var newPrices = JsonSerializer.Deserialize<JsonPriceOverview>(jsonResponse.Result);
							if (newPrices != null && newPrices.results != null)
							{
								newPrices.results.ForEach(d => { d.valid_from = d.valid_from.ToLocalTime(); d.valid_to = d.valid_to.ToLocalTime(); });
								if (newPrices.results[0].valid_from > lastHighestTime)
								{
									ret = true;
									newPrices.lastFetched = DateTime.Now;

									prices = newPrices;
								}
							}
						}
					}
					else
						prices = new JsonPriceOverview() { lastFetched = DateTime.Now, count = 0 };
				}
			}
			catch (Exception)
			{
				return false;
			}
			return ret;
		}

		// gets price for the current half-hour, note we return the on-the-half-hour value (eg 22:00->22:29)
		public float GetCurrentPrice()
		{
			return prices.results.SingleOrDefault(i => i.valid_from <= DateTime.Now && i.valid_to > DateTime.Now)?.value_inc_vat ?? 0;
		}

		public JsonPriceOverview GetPrices()
		{
			return prices;
		}
	}
}
