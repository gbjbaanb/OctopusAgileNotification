using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OctopusAgileNotification
{
	public partial class LogUI : Form
	{
		public LogUI(FixedSizedQueue logger)
		{
			InitializeComponent();

			listLog.BeginUpdate();
			listLog.Items.Clear();

			foreach (var item in logger)
			{
				ListViewItem.ListViewSubItem s = new() { Text = item.message, };
				ListViewItem i = new()
				{
					Text = $"{item.when:t}",
					SubItems = { s }
				};

				listLog.Items.Add(i);
			}

			listLog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listLog.EndUpdate();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
