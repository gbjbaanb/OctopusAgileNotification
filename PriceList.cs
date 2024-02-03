using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OctopusAgileNotification
{
	public partial class PriceList : Form
	{
		static Color taskbarColour = WinColours.GetColourAt(WinColours.GetTaskbarPosition().Location);
		//static Color taskbarContrast = WinColours.CalcContrastColor(taskbarColour);
		static Color taskbarContrast = WinColours.GetContrastingColor(taskbarColour);

		public PriceList(JsonPriceOverview prices)
		{
			InitializeComponent();

			using Thresholds thresholds = new Thresholds();

			if (WinColours.GetWindowsColorMode() == 0)
			{
				this.BackColor = listViewPrices.BackColor = taskbarColour;
			}

			listViewPrices.BeginUpdate();
			foreach (var item in prices.results.Where(i => i.valid_to > DateTime.Now))
			{
				ListViewItem.ListViewSubItem s = new()
				{
					BackColor = thresholds.GetColours(item.value_inc_vat).backColour,
					Text = $"{item.value_inc_vat:F2}p",
					ForeColor = thresholds.GetColours(item.value_inc_vat).textColour,
				};

				ListViewItem i = new ListViewItem()
				{
					Text = $"{item.valid_from:t}",
					UseItemStyleForSubItems = false,
					SubItems = { s }
				};

				if (WinColours.GetWindowsColorMode() == 0)
				{
					if (s.BackColor.A < 255)
						s.BackColor = taskbarColour;
					i.BackColor = taskbarColour;
					i.ForeColor = taskbarContrast;
				}

				listViewPrices.Items.Add(i);
			}
			listViewPrices.EndUpdate();

			// resize to fit all prices
			if (listViewPrices.Items.Count > 0)
			{
				listViewPrices.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				var lastItem = listViewPrices.Items[0];
				this.MinimumSize = new Size(10, 10);
				this.ClientSize = new Size(listViewPrices.Columns[0].Width + listViewPrices.Columns[1].Width + this.Padding.Size.Width,
					(lastItem.Bounds.Height * listViewPrices.Items.Count) + this.Padding.Size.Height);
			}

			this.Location = new Point(MousePosition.X - this.Width / 2, MousePosition.Y - this.Height - SystemInformation.IconSize.Height);
		}


	}
}
