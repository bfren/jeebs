// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Diagnostics.CodeAnalysis;

namespace Jeebs;

public readonly partial struct DateTimeInt
{
	/// <inheritdoc/>
	public static DateTimeInt Parse(string s, IFormatProvider? provider)
	{
		ArgumentException.ThrowIfNullOrEmpty(s);

		if (s.Length != 12)
		{
			throw new ArgumentException($"{nameof(DateTimeInt)} value must be a 12 characters long", nameof(s));
		}

		if (!ulong.TryParse(s, out _))
		{
			throw new ArgumentException("Not a valid number", nameof(s));
		}

		return new(
			year: int.Parse(s[0..4], provider: provider),
			month: int.Parse(s[4..6], provider: provider),
			day: int.Parse(s[6..8], provider: provider),
			hour: int.Parse(s[8..10], provider: provider),
			minute: int.Parse(s[10..], provider: provider)
		);
	}

	/// <inheritdoc/>
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DateTimeInt result)
	{
		try
		{
			result = Parse(s!, provider);
			return true;
		}
		catch (Exception)
		{
			result = MinValue;
			return false;
		}
	}
}
