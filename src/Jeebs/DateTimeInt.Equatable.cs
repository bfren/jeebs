// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public readonly partial struct DateTimeInt
{
	/// <inheritdoc/>
	public bool Equals(DateTimeInt other) =>
		(Year, Month, Day, Hour, Minute) == (other.Year, other.Month, other.Day, other.Hour, other.Minute);

	/// <inheritdoc/>
	public override bool Equals(object? obj) =>
		obj switch
		{
			DateTimeInt x =>
				Equals(other: x),

			_ =>
				false
		};

	/// <inheritdoc/>
	public override int GetHashCode() =>
		Year.GetHashCode() ^ Month.GetHashCode() ^ Day.GetHashCode() ^ Hour.GetHashCode() ^ Minute.GetHashCode();
}
