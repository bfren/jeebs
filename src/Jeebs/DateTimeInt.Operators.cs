// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public readonly partial struct DateTimeInt : IEquatable<DateTimeInt>
{
	/// <inheritdoc/>
	public static bool operator ==(DateTimeInt left, DateTimeInt right) =>
		left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(DateTimeInt left, DateTimeInt right) =>
		!left.Equals(right);

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
