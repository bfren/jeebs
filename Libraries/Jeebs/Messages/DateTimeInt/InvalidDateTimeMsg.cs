using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.DateTimeInt
{
	public sealed class InvalidDateTimeMsg : WithValueMsg<(string part, Jeebs.DateTimeInt dt)>
	{
		public InvalidDateTimeMsg(string part, Jeebs.DateTimeInt dt) : base((part, dt)) { }

		public override string ToString()
			=> $"Invalid {Value.part} - 'Y:{Value.dt.Year} M:{Value.dt.Minute} D:{Value.dt.Day} H:{Value.dt.Hour} m:{Value.dt.Minute}'.";
	}
}
