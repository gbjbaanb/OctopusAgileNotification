using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OctopusAgileNotification.Properties;

namespace OctopusAgileNotification
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new NotifyContext());

			Settings.Default.Save();
		}
	}
}



// Octpus API
// https://developer.octopus.energy/docs/api/

// example personal
// https://octopus.energy/dashboard/new/accounts/personal-details/api-access

// example price URL
// https://api.octopus.energy/v1/products/AGILE-FLEX-22-11-25/electricity-tariffs/E-1R-AGILE-FLEX-22-11-25-H/standard-unit-rates/?period_from=2024-01-31T15:37:19
