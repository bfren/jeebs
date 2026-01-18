// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc;

/// <summary>
/// Represents the result of an operation, and can be returned by MVC / Razor Page methods.
/// </summary>
public interface IResult : IActionResult
{
	/// <summary>
	/// Whether or not the operation was successful.
	/// </summary>
	bool Success { get; }

	/// <summary>
	/// HTTP status code - by default returns 200 on success or 500 on failure.
	/// </summary>
	int StatusCode { get; }
}

/// <inheritdoc cref="IResult"/>
/// <typeparam name="T">Value type</typeparam>
public interface IResult<out T> : IResult
{
	/// <summary>
	/// Returns the value if the operation succeeded, or null if not.
	/// </summary>
	T? Value { get; }
}
