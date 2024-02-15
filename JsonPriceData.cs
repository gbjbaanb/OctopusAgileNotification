using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusAgileNotification
{
	public class JsonPriceOverview
	{
		public DateTime lastFetched { get; set; }
		public int count { get; set; }
		public string next { get; set; }
		public string previous { get; set; }
		public List<JsonPriceData> results { get; set; }
	}

	public class JsonPriceData
	{
		public float value_exc_vat { get; set; }
		public float value_inc_vat { get; set; }
		public DateTime valid_from { get; set; }
		public DateTime valid_to { get; set; }
	}
}
