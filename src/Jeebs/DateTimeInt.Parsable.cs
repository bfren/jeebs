// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public readonly partial struct DateTimeInt
{
	/// <summary>
	/// Parse a string as DateTimeInt.
	/// </summary>
	/// <param name="s">ReadOnlySpan.</param>
	/// <param name="provider">IFormatProvider.</param>
	/// <returns>DateTimeInt</returns>
	private static DateTimeInt Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
	{
		if (s.Length == 0)
		{
			return MinValue;
		}

		if (s.Length != 12)
		{
			throw new ArgumentException($"{nameof(DateTimeInt)} value must be a 12 characters long.", nameof(s));
		}

		if (!ulong.TryParse(s, out _))
		{
			throw new ArgumentException("Not a valid number.", nameof(s));
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
	public static DateTimeInt Parse(string s, IFormatProvider? provider) =>
		Parse(s.Trim().AsSpan(), provider);

	/// <inheritdoc/>
	public static bool TryParse(string? s, IFormatProvider? provider, out DateTimeInt result)
	{
		try
		{
			result = Parse(s ?? string.Empty, provider);
			return true;
		}
		catch (Exception)
		{
			result = MinValue;
			return false;
		}
	}
}
