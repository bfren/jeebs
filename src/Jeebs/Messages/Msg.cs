// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs.Functions;
using Jeebs.Logging;
using Jeebs.Messages.Exceptions;

namespace Jeebs.Messages;

/// <summary>
/// Framework message - compatible with <see cref="IMsg"/>
/// </summary>
public abstract record class Msg() : IMsg
{
	/// <summary>
	/// Default LogLevel for messages
	/// </summary>
	public static readonly LogLevel DefaultLevel =
		LogLevel.Warning;

	/// <inheritdoc/>
	public virtual LogLevel Level =>
		level ?? DefaultLevel;

	private readonly LogLevel? level;

	/// <inheritdoc/>
	public virtual string Format =>
		format ?? string.Empty;

	private readonly string? format;

	/// <inheritdoc/>
	public string FormatWithType =>
		$"{{MsgType}} {Format}".Trim();

	/// <inheritdoc/>
	public virtual object[]? Args =>
		args;

	private readonly object[]? args;

	/// <inheritdoc/>
	public object[] ArgsWithType
	{
		get
		{
			// Add type to list of arguments
			var list = Args?.ToList() ?? [];
			list.Insert(0, GetTypeName());

			// Return as array
			return list.ToArray();
		}
	}

	/// <summary>
	/// For testing, allow <see cref="level"/>, <see cref="format"/>, and <see cref="args"/> to be set via constructor
	/// </summary>
	/// <param name="level">Log Level</param>
	/// <param name="format">Format</param>
	/// <param name="args"></param>
	private protected Msg(LogLevel? level, string? format, object[]? args) : this() =>
		(this.level, this.format, this.args) = (level, format, args);

	/// <inheritdoc/>
	public string GetTypeName() =>
		GetType().ToString();

	/// <summary>
	/// Override using formatted message
	/// </summary>
	public sealed override string ToString() =>
		string.IsNullOrWhiteSpace(Format) switch
		{
			true =>
				GetTypeName(),

			false =>
				StringF.Format(FormatWithType, ArgsWithType)
		};

	/// <summary>
	/// Create <see cref="MsgException{TMsg}"/>
	/// </summary>
	/// <typeparam name="TMsg">Message type</typeparam>
	/// <param name="msg">Message</param>
	public static MsgException<TMsg> CreateException<TMsg>(TMsg msg)
		where TMsg : IMsg =>
		new(msg);
}
