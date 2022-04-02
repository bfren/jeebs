// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions;

/// <summary>
/// C# Base32 Converter
/// Copyright (c) 2014 Oleg Ignat - licensed under https://creativecommons.org/licenses/by/2.0/
/// See https://olegignat.com/base32/
/// Modifications to make consistent with Jeebs coding style and conventions (c) bfren as above
/// </summary>
public static partial class Base32F
{
	/// <summary>
	/// Size of the regular byte in bits
	/// </summary>
	private const int InByteSize = 8;

	/// <summary>
	/// Size of converted byte in bits
	/// </summary>
	private const int OutByteSize = 5;

	/// <summary>
	/// Alphabet - see https://datatracker.ietf.org/doc/html/rfc3548#section-5
	/// </summary>
	private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

	/// <summary>Messages</summary>
	public static partial class M { }
}
