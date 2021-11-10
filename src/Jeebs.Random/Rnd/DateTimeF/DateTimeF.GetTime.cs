﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace F;

public static partial class Rnd
{
	public static partial class DateTimeF
	{
		/// <summary>
		/// Return a random Time
		/// </summary>
		public static TimeOnly GetTime() =>
			new(
				hour: NumberF.GetInt32(0, 23),
				minute: NumberF.GetInt32(0, 59),
				second: NumberF.GetInt32(0, 59)
			);
	}
}
