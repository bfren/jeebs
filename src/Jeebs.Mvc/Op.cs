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

/// <inheritdoc cref="IOp{T}"/>
public record class Op<T> : Op, IOp<T>
{
	/// <summary>
	/// Create with value.
	/// </summary>
	/// <param name="value">Result value.</param>
	public Op(Result<T> value) =>
		Result = value;

	/// <summary>
	/// Result object.
	/// </summary>
	internal Result<T> Result { get; private init; }

	/// <summary>
	/// Returns true if the operation was a success - or in the special case that <typeparamref name="T"/>
	/// is <see cref="bool"/> and <see cref="Result"/> is <see cref="Ok{T}"/>, returns that value.
	/// </summary>
	public override bool Success =>
		Result switch
		{
			Ok<bool> some =>
				some.Value,

			Ok<T> =>
				true,

			_ =>
				false
		};

	/// <inheritdoc/>
	public Alert Message
	{
		get => field switch
		{
			Alert message =>
				message,

			_ =>
				Result.Match(
					ok: _ => Alert.Success(nameof(AlertType.Success)),
					fail: r => Alert.Error(r.ToString() ?? r.GetType().Name)
				)
		};
		init;
	}

	/// <inheritdoc/>
	public T? Value =>
		Result.Unwrap(_ => default!);

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
/// Easily create <see cref="Op{T}"/> objects.
/// </summary>
public abstract record class Op : IOp
{
	/// <inheritdoc/>
	public abstract bool Success { get; }

	/// <inheritdoc/>
	public abstract int StatusCode { get; init; }

	/// <inheritdoc cref="ActionResult.ExecuteResultAsync(ActionContext)"/>
	public abstract Task ExecuteResultAsync(ActionContext context);

	#region Static Methods

	/// <summary>
	/// Create an error result with a message (and a <see langword="false"/> value).
	/// </summary>
	/// <param name="message">Error message.</param>
	public static Op Error(string message) =>
		new Op<bool>(false) { Message = Alert.Error(message) };

	/// <summary>
	/// Create an error result with a failure message.
	/// </summary>
	/// <param name="failure">Failure value.</param>
	public static Op Error(FailureValue failure) =>
		new Op<bool>(R.Fail(failure));

	/// <summary>
	/// Create with value.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="value">Value.</param>
	public static Op Create<T>(T value) =>
		new Op<T>(value);

	/// <summary>
	/// Create with value.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="value">Value.</param>
	public static Op Create<T>(Result<T> value) =>
		new Op<T>(value);

	/// <summary>
	/// Create with value and message.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="value"></param>
	/// <param name="message"></param>
	public static Op Create<T>(T value, Alert message) =>
		new Op<T>(value) { Message = message };

	/// <summary>
	/// Create with value and message.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="value"></param>
	/// <param name="message"></param>
	public static Op Create<T>(Result<T> value, Alert message) =>
		new Op<T>(value) { Message = message };

	#endregion Static Methods
}
