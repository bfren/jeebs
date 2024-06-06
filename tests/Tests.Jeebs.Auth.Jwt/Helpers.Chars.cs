// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Jwt;

internal static partial class Helpers
{
	internal static class Chars
	{
		internal static readonly char[] Numbers =
			['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

		internal static readonly char[] Lowercase =
			['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];

		internal static readonly char[] Uppercase =
			['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

		internal static readonly char[] Special =
			['!', '#', '@', '+', '-', '*', '^', '=', ':', ';', '£', '$', '~', '`', '¬', '|'];

		internal static readonly char[] Hexadecimal =
			['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'];

		internal static readonly char[] Letters =
			[.. Lowercase, .. Uppercase];

		internal static readonly char[] All =
			[.. Numbers, .. Lowercase, .. Uppercase, .. Special];
	}
}
