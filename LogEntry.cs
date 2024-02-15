using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusAgileNotification
{
	public class LogEntry
	{
		public DateTime when { get; set; }
		public string message { get; set; }
	}

	public sealed class FixedSizedQueue : Queue<LogEntry>
	{
		public int FixedCapacity { get; }
		public FixedSizedQueue(int fixedCapacity)
		{
			this.FixedCapacity = fixedCapacity;
		}

		/// <summary>
		/// If the total number of item exceed the capacity, the oldest ones automatically dequeues.
		/// </summary>
		/// <returns>The dequeued value, if any.</returns>
		public LogEntry Enqueue(string item)
		{
			base.Enqueue(new LogEntry() { when = DateTime.Now, message = item});
			if (base.Count > FixedCapacity)
			{
				return base.Dequeue();
			}
			return default;
		}
	}
}
