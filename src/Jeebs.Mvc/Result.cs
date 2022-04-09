// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;
using Jeebs.Mvc.Enums;
using Jeebs.Mvc.Models;
using MaybeF.Internals;

namespace Jeebs.Mvc;

/// <summary>
/// Represents the result of an operation
/// </summary>
public abstract record class Result
{
	/// <summary>
	/// Returns true if the operation was a success
	/// </summary>
	public abstract bool Success { get; }

	/// <summary>
	/// User feedback alert message - by default returns 'Success' or the Reason message,
	/// but can be set to display something else
	/// </summary>
	public abstract Alert Message { get; init; }

	/// <summary>
	/// Returns the value if the operation succeeded, or null if not
	/// </summary>
	public object? Value { get; }

	/// <summary>
	/// If set, tells the client to redirect to this URL
	/// </summary>
	public string? RedirectTo { get; init; }

	/// <summary>
	/// Return value serialised as JSON
	/// </summary>
	public sealed override string ToString() =>
		JsonF.Serialise(this).Unwrap(() => JsonF.Empty);

	#region Static Create

	/// <summary>
	/// Create with value
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="value"></param>
	public static Result<T> Create<T>(T value) =>
		new(value);

	/// <summary>
	/// Create with value
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="value"></param>
	public static Result<T> Create<T>(Maybe<T> value) =>
		new(value);

	/// <summary>
	/// Create with value and message
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="value"></param>
	/// <param name="message"></param>
	public static Result<T> Create<T>(T value, Alert message) =>
		new(value) { Message = message };

	/// <summary>
	/// Create with value and message
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="value"></param>
	/// <param name="message"></param>
	public static Result<T> Create<T>(Maybe<T> value, Alert message) =>
		new(value) { Message = message };

	#endregion Static Create
}

/// <inheritdoc cref="Result"/>
/// <typeparam name="T">Value type</typeparam>
public sealed record class Result<T> : Result
{
	/// <inheritdoc/>
	public override Alert Message
	{
		get => message switch
		{
			Alert message =>
				message,

			_ =>
				Maybe.Switch(
					some: _ => Alert.Success(nameof(AlertType.Success)),
					none: r => Alert.Error(r.ToString() ?? r.GetType().Name)
				)
		};
		init => message = value;
	}

	private Alert? message;

	/// <summary>
	/// Maybe result object
	/// </summary>
	internal Maybe<T> Maybe { get; private init; }

	/// <summary>
	/// Returns true if the operation was a success - or in the special case that <typeparamref name="T"/>
	/// is <see cref="bool"/> and <see cref="Maybe"/> is <see cref="Some{T}"/>, returns that value
	/// </summary>
	public override bool Success =>
		Maybe switch
		{
			Some<bool> some =>
				some.Value,

			Some<T> =>
				true,

			_ =>
				false
		};

	/// <inheritdoc cref="Result.Value"/>
	public new T? Value =>
		Maybe.IsSome(out var value) switch
		{
			true =>
				value,

			_ =>
				default
		};

	/// <summary>
	/// Create with value
	/// </summary>
	/// <param name="value"></param>
	internal Result(Maybe<T> value) =>
		Maybe = value;
}
