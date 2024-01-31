using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ServiceStack;
using static ServiceStack.Diagnostics.Events;

namespace OctopusAgileNotification
{
	internal class PriceFetch
	{
		private JsonPriceOverview prices;

		private readonly System.Net.Http.HttpClient _httpClient;
		private const string TariffUrl = "https://api.octopus.energy/v1/products/";
		private const string ProductCode = "AGILE-FLEX-22-11-25";
		private const string TariffCode = "E-1R-AGILE-FLEX-22-11-25-H";

		public PriceFetch()
		{
			_httpClient = new() { BaseAddress = new Uri(TariffUrl) };
		}


		// returns true if prices were fetched and changed from previous set
		public bool GetPrices()
		{
			bool ret = false;
			
			try
			{
				using var task = _httpClient.GetAsync($"{ProductCode}//electricity-tariffs/{TariffCode}/standard-unit-rates/?period_from={DateTime.Now:s}");
				if (!task.Wait(5000))
					return false;

				if (task.IsCompletedSuccessfully)
				{
					var jsonResponse = task.Result.Content.ReadAsString();
					if (task.Result.IsSuccessStatusCode)
					{
						DateTime lastHighestTime = DateTime.Now;
						if (prices != null && prices.results != null && prices.results[0] != null)
						{
							 lastHighestTime = prices.results[0].valid_from;
						}
						var newPrices = JsonSerializer.Deserialize<JsonPriceOverview>(jsonResponse);
						if (newPrices != null && newPrices.results != null && newPrices.results[0].valid_from != lastHighestTime) 
						{
							ret = true;
							prices = newPrices;
						}
					}
					else
						prices = new JsonPriceOverview() { count = 0 };
				}
			}
			catch (Exception)
			{
				return false;
			}
			return ret;
		}

		// gets price for the curent half-hour, note we return the on-the-half-hour value (eg 22:00->22:29)
		public float GetCurrentPrice()
		{
			return prices.results.SingleOrDefault(i => i.valid_from <= DateTime.Now && i.valid_to > DateTime.Now)?.value_inc_vat ?? 0;
		}
	}
}
