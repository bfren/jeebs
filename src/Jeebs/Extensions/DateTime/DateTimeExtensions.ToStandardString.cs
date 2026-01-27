// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs;

public static partial class DateTimeExtensions
{
	/// <summary>
	/// Return a standard format date/time value (HH:mm dd/MM/yyyy).
	/// </summary>
	/// <param name="this">DateTime object.</param>
	/// <returns>Standard DateTime string.</returns>
	public static string ToStandardString(this DateTime @this) =>
		@this.ToString("HH:mm dd/MM/yyyy", F.DefaultCulture);
}
