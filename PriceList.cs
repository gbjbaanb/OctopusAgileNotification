using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OctopusAgileNotification
{
	public partial class PriceList : Form
	{
		public PriceList(JsonPriceOverview prices)
		{
			InitializeComponent();

			using Thresholds thresholds = new Thresholds();

			foreach (var item in prices.results.Where(i => i.valid_to > DateTime.Now))
			{
				ListViewItem i = new ListViewItem() { 
					BackColor = thresholds.GetColours(item.value_inc_vat).backColour,
					Text = $"{item.valid_from:t} to {item.valid_to:t}  :  {item.value_inc_vat:F2}p",
					ForeColor = thresholds.GetColours(item.value_inc_vat).textColour
				};

				listViewPrices.Items.Add(i);
			}

			if (listViewPrices.Items.Count > 0)
			{
				var lastItem = listViewPrices.Items[listViewPrices.Items.Count - 1];
				this.ClientSize = new Size(ClientSize.Width, listViewPrices.Top + (lastItem.Bounds.Height * listViewPrices.Items.Count));
			}
		}

	}
}
