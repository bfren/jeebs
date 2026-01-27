// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public readonly partial struct DateTimeInt
{
	/// <inheritdoc/>
	public int CompareTo(DateTimeInt other) =>
		string.Compare(ToString(), other.ToString(), System.StringComparison.Ordinal);
}
