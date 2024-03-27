// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public readonly partial struct DateTimeInt
{
	/// <inheritdoc/>
	public static bool operator ==(DateTimeInt left, DateTimeInt right) =>
		left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(DateTimeInt left, DateTimeInt right) =>
		!(left == right);

	/// <inheritdoc/>
	public static bool operator <(DateTimeInt left, DateTimeInt right) =>
		left.CompareTo(right) < 0;

	/// <inheritdoc/>
	public static bool operator <=(DateTimeInt left, DateTimeInt right) =>
		left.CompareTo(right) <= 0;

	/// <inheritdoc/>
	public static bool operator >(DateTimeInt left, DateTimeInt right) =>
		left.CompareTo(right) > 0;

	/// <inheritdoc/>
	public static bool operator >=(DateTimeInt left, DateTimeInt right) =>
		left.CompareTo(right) >= 0;
}
