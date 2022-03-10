// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs.Functions;
using Jeebs.Logging;

namespace Jeebs.Messages;

/// <inheritdoc cref="IMsg"/>
public abstract record class Msg : IMsg
{
	/// <summary>
	/// Default LogLevel for messages
	/// </summary>
	public static readonly LogLevel DefaultLevel = LogLevel.Information;

	/// <inheritdoc/>
	public virtual LogLevel Level { get; protected init; } = DefaultLevel;

	/// <inheritdoc/>
	public virtual string Format { get; protected init; } = string.Empty;

	/// <inheritdoc/>
	public string FormatWithType =>
		$"{{MsgType}} {Format}".Trim();

	/// <inheritdoc/>
	public virtual object[]? Args { get; protected init; }

	/// <inheritdoc/>
	public object[] ArgsWithType
	{
		get
		{
			// Add type to list of arguments
			var list = Args?.ToList() ?? new List<object>();
			list.Insert(0, GetTypeName());

			// Return as array
			return list.ToArray();
		}
	}

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
}
