// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace F;

public static partial class Rnd
{
	public static partial class DateF
	{
		/// <summary>
		/// Return a random Date
		/// </summary>
		public static DateOnly Get() =>
			new(
				year: NumberF.GetInt32(1, 9999),
				month: NumberF.GetInt32(1, 12),
				day: NumberF.GetInt32(1, 28)
			);
	}
}
