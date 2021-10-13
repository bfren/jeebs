// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace F;

public static partial class Rnd
{
	/// <summary>
	/// Random DateTime function
	/// </summary>
	public static partial class DateTimeF
	{
		/// <summary>
		/// Return a random DateTime
		/// </summary>
		public static DateTime Get() =>
			new(
				year: NumberF.GetInt32(1, 9999),
				month: NumberF.GetInt32(1, 12),
				day: NumberF.GetInt32(1, 28),
				hour: NumberF.GetInt32(0, 23),
				minute: NumberF.GetInt32(0, 59),
				second: NumberF.GetInt32(0, 59)
			);
	}
}
