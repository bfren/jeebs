// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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
	/// Returns the value if the operation succeeded, or null
	/// </summary>
	public object? Value { get; }

	/// <summary>
	/// Returns the reason for failure, or 'Success' if the operation succeeded
	/// </summary>
	public abstract string Reason { get; }

	/// <summary>
	/// If set, provides a message to give user feedback
	/// </summary>
	public string? Message { get; protected init; }

	/// <summary>
	/// If set, tells the client to redirect to this URL
	/// </summary>
	public string? RedirectTo { get; init; }

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
	public static Result<T> Create<T>(Maybe<T> value, string? message) =>
		new(value, message);
}

/// <inheritdoc cref="Result"/>
/// <typeparam name="T">Value type</typeparam>
public sealed record class Result<T> : Result
{
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

	/// <inheritdoc/>
	public override string Reason =>
		Maybe.IsNone(out var reason) switch
		{
			true =>
				reason.ToString() ?? reason.GetType().Name,

			false =>
				nameof(Success)
		};

	/// <summary>
	/// Create with value
	/// </summary>
	/// <param name="value"></param>
	internal Result(Maybe<T> value) : this(value, null) { }

	/// <summary>
	/// Create with value and message
	/// </summary>
	/// <param name="value"></param>
	/// <param name="message"></param>
	internal Result(Maybe<T> value, string? message) =>
		(Maybe, Message) = (value, message);
}
