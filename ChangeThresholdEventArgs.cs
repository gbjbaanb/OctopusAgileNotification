using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace OctopusAgileNotification
{
	public class ChangeThresholdEventArgs : EventArgs
	{
		public ChangeThresholdEventArgs(int lev, string qty, Color? fg, Color? bg)
		{
			level = lev;
			threshold = qty.ToInt();
			fgColour = fg;
			bgColour = bg;
		}

		public int level { get; private set; }
		public int threshold { get; private set; }
		public Color? fgColour { get; private set; }
		public Color? bgColour { get; private set; }
	}
}
