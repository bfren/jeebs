// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Functions;

/// <summary>
/// Enum functions.
/// </summary>
public static partial class EnumF
{
	private static Result<T> FailNotAValidEnum<T>()
		where T : struct, Enum =>
		R.Fail("'{Value}' Type is not a valid Enum.", typeof(T));

	private static Result<T> FailNullValue<T>()
		where T : struct, Enum =>
		R.Fail("Attempting to parse a null value.");

	private static Result<T> FailNotAValidEnumValue<T>(string? value)
		where T : struct, Enum =>
		R.Fail("'{Value}' is not a valid {Type}.", value, typeof(T));
}
