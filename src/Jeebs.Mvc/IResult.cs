// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc;

/// <summary>
/// Represents the result of an operation, and can be returned by MVC / Razor Page methods
/// </summary>
public interface IResult : IActionResult
{
	/// <summary>
	/// HTTP status code - by default returns 200 on success or 500 on failure
	/// </summary>
	int StatusCode { get; }

	/// <summary>
	/// Optional URL to redirect to on success
	/// </summary>
	string? RedirectTo { get; }
}

/// <inheritdoc cref="IResult"/>
/// <typeparam name="T">Value type</typeparam>
public interface IResult<out T> : IResult
{
	/// <summary>
	/// Returns true if the operation was a success
	/// </summary>
	bool Success { get; }

	/// <summary>
	/// User feedback alert message - by default returns 'Success' or the Reason message,
	/// but can be set to display something else
	/// </summary>
	Alert Message { get; }

	/// <summary>
	/// Returns the value if the operation succeeded, or null if not
	/// </summary>
	T? Value { get; }
}
