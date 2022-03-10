// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Random;

public static partial class Rnd
{
	public static partial class TimeF
	{
		/// <summary>
		/// Return a random Time
		/// </summary>
		public static TimeOnly Get() =>
			new(
				hour: NumberF.GetInt32(0, 23),
				minute: NumberF.GetInt32(0, 59),
				second: NumberF.GetInt32(0, 59)
			);
	}
}
