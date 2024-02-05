using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OctopusAgileNotification.Properties;

namespace OctopusAgileNotification
{
	public partial class PriceList : Form
	{
		static Color taskbarColour = WinColours.GetColourAt(WinColours.GetTaskbarPosition().Location);
		static Color taskbarContrast = WinColours.GetContrastingColor(taskbarColour); // or CalcContrastColor(taskbarColour);

		public PriceList(JsonPriceOverview prices)
		{
			InitializeComponent();

			using Thresholds thresholds = new Thresholds();

			if (WinColours.GetWindowsColorMode() == 0)
			{
				BackColor = listViewPrices.BackColor = taskbarColour;
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
				MinimumSize = new Size(10, 10);
				ClientSize = new Size(listViewPrices.Columns[0].Width + listViewPrices.Columns[1].Width + Padding.Size.Width,
					(lastItem.Bounds.Height * listViewPrices.Items.Count) + Padding.Size.Height);
			}

			// if not persisting, or if form would be off-screen, position by mouse
			if (!Settings.Default.PersistPosition 
				|| Settings.Default.PopupPositionX < Width || Settings.Default.PopupPositionX > Screen.PrimaryScreen.Bounds.Width + Width
				|| Settings.Default.PopupPositionY < Height || Settings.Default.PopupPositionY > Screen.PrimaryScreen.Bounds.Height + Height)
			{
				Location = new Point(MousePosition.X - Width / 2, MousePosition.Y - Height - SystemInformation.IconSize.Height);
			}
			else
				Location = new Point(Settings.Default.PopupPositionX - Width, Settings.Default.PopupPositionY - Height);
		}


		// drag the form about (as the listview is full-fill, use its events)
		private bool mouseDown = false;
		private Point lastLocation;

		private void listViewPrices_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Clicks == 1)
				mouseDown = true;
			lastLocation = e.Location;
		}

		private void listViewPrices_MouseMove(object sender, MouseEventArgs e)
		{
			if (mouseDown)
			{
				Location = new Point((Location.X - lastLocation.X) + e.X, (Location.Y - lastLocation.Y) + e.Y);
				Update();
			}
		}

		private void listViewPrices_MouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = false;
			if (Settings.Default.PersistPosition)
			{
				// track the bottom right corner as the size will change.
				Settings.Default.PopupPositionX = Location.X + Width;
				Settings.Default.PopupPositionY = Location.Y + Height;
			}
		}
	}
}
