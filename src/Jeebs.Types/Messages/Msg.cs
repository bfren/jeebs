// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using Jeebs.Logging;

namespace Jeebs;

/// <summary>
/// Framework message
/// </summary>
public abstract record class Msg
{
	/// <summary>
	/// Default LogLevel for messages
	/// </summary>
	public const LogLevel DefaultLevel = LogLevel.Information;

	/// <summary>
	/// Log level
	/// </summary>
	public virtual LogLevel Level { get; protected init; } = DefaultLevel;

	/// <summary>
	/// Output format
	/// </summary>
	public virtual string Format { get; protected init; } = string.Empty;

	/// <summary>
	/// Output format, including the message type
	/// </summary>
	public string FormatWithType =>
		$"{{MsgType}} {Format}".Trim();

	/// <summary>
	/// Output arguments
	/// </summary>
	public virtual object[]? Args { get; protected init; }

	/// <summary>
	/// Output arguments, including the message type
	/// </summary>
	public object[] ArgsWithType
	{
		get
		{
			// Add type to liste of arguments
			var list = Args?.ToList() ?? new List<object>();
			list.Insert(0, GetTypeName());

			// Return as array
			return list.ToArray();
		}
	}

	/// <summary>
	/// Return message type name
	/// </summary>
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
				F.MsgF.Format(FormatWithType, ArgsWithType)
		};
}
