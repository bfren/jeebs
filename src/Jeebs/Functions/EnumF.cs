// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions;

/// <summary>
/// Enum functions.
/// </summary>
public static partial class EnumF
{
	/// <summary>
	/// Creates a failed result indicating that a specified value is not a valid enumeration type.
	/// </summary>
	/// <typeparam name="T">Enum type.</typeparam>
	/// <param name="class">The name of the class where the validation failed.</param>
	/// <param name="function">The name of the function where the validation failed.</param>
	/// <returns>A failed result containing an error message that the value is not a valid enumeration of type <typeparamref
	/// name="T"/>.</returns>
	internal static Result<T> FailNotAValidEnum<T>(string @class, string function) =>
		R.Fail("'{Type}' Type is not a valid Enum.", new { Type = typeof(T).FullName }).Ctx(@class, function);

	/// <summary>
	/// Creates a failed result indicating that a null value was encountered during parsing.
	/// </summary>
	/// <typeparam name="T">Enum type.</typeparam>
	/// <param name="class">The name of the class where the null value was encountered.</param>
	/// <param name="function">The name of the function where the null value was encountered.</param>
	/// <returns>A failed result containing an error message about the null value.</returns>
	internal static Result<T> FailNullValue<T>(string @class, string function) =>
		R.Fail("Attempting to parse a null value.").Ctx(@class, function);

	/// <summary>
	/// Creates a failed result indicating that the specified value is not a valid value for the given type.
	/// </summary>
	/// <typeparam name="T">Enum type.</typeparam>
	/// <param name="class">The name of the class where the validation failure occurred.</param>
	/// <param name="function">The name of the function where the validation failure occurred.</param>
	/// <param name="value">The value that failed validation.</param>
	/// <returns>A failed result containing an error message that the specified value is not valid for the type <typeparamref
	/// name="T"/>.</returns>
	internal static Result<T> FailNotAValidValue<T>(string @class, string function, string? value) =>
		R.Fail("'{Value}' is not a valid value of {Type}.", new { value, Type = typeof(T).FullName }).Ctx(@class, function);
}
