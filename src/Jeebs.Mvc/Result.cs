// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Jeebs.Functions;
using Jeebs.Mvc.Enums;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc;

/// <inheritdoc cref="IResult{T}"/>
public record class Result<T> : Result, IResult<T>
{
	/// <summary>
	/// Create with value
	/// </summary>
	/// <param name="value"></param>
	public Result(Maybe<T> value) =>
		Maybe = value;

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

	/// <inheritdoc/>
	public Alert Message
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

	/// <inheritdoc/>
	public T? Value =>
		Maybe.IsSome(out var value) switch
		{
			true =>
				value,

			_ =>
				default
		};

	/// <inheritdoc/>
	[JsonIgnore]
	public override int StatusCode
	{
		get => statusCode switch
		{
			int statusCode =>
				statusCode,

			_ =>
				Success switch
				{
					true =>
						(int)HttpStatusCode.OK,

					false =>
						(int)HttpStatusCode.InternalServerError
				}
		};
		init => statusCode = value;
	}

	private int? statusCode;

	/// <inheritdoc cref="ActionResult.ExecuteResultAsync(ActionContext)"/>
	public override Task ExecuteResultAsync(ActionContext context)
	{
		// Create MVC JsonResult from this result's properties
		var jsonResult = new JsonResult(this, JsonF.CopyOptions())
		{
			ContentType = "application/json",
			StatusCode = StatusCode
		};

		// Get result executor
		var services = context.HttpContext.RequestServices;
		var executor = services.GetRequiredService<IActionResultExecutor<JsonResult>>();

		// Execute JsonResult
		return executor.ExecuteAsync(context, jsonResult);
	}
}

/// <summary>
/// Easily create <see cref="Result{T}"/> objects
/// </summary>
public abstract record class Result : IResult
{
	/// <inheritdoc/>
	public abstract bool Success { get; }

	/// <inheritdoc/>
	public abstract int StatusCode { get; init; }

	/// <inheritdoc cref="ActionResult.ExecuteResultAsync(ActionContext)"/>
	public abstract Task ExecuteResultAsync(ActionContext context);

	#region Static Methods

	/// <summary>
	/// Create an error result with a message (and a <see langword="false"/> value)
	/// </summary>
	/// <param name="message"></param>
	public static Result Error(string message) =>
		new Result<bool>(false) { Message = Alert.Error(message) };

	/// <summary>
	/// Create an error result with a Reason message
	/// </summary>
	/// <typeparam name="T">Reason message type</typeparam>
	/// <param name="reason"></param>
	public static Result Error<T>(T reason)
		where T : IMsg =>
		new Result<bool>(F.None<bool>(reason));

	/// <summary>
	/// Create with value
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="value"></param>
	public static Result Create<T>(T value) =>
		new Result<T>(value);

	/// <summary>
	/// Create with value
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="value"></param>
	public static Result Create<T>(Maybe<T> value) =>
		new Result<T>(value);

	/// <summary>
	/// Create with value and message
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="value"></param>
	/// <param name="message"></param>
	public static Result Create<T>(T value, Alert message) =>
		new Result<T>(value) { Message = message };

	/// <summary>
	/// Create with value and message
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="value"></param>
	/// <param name="message"></param>
	public static Result Create<T>(Maybe<T> value, Alert message) =>
		new Result<T>(value) { Message = message };

	#endregion Static Methods
}
