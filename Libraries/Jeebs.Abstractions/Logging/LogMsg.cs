// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Logging;

namespace Jeebs
{
	/// <inheritdoc cref="ILogMsg"/>
	public abstract record LogMsg : ILogMsg
	{
		private const LogLevel DefaultLevel = LogLevel.Information;

		/// <inheritdoc/>
		public virtual LogLevel Level { get; init; }

		/// <inheritdoc/>
		public string Format { get; init; }

		/// <inheritdoc/>
		public abstract Func<object[]> Args { get; }

		/// <summary>Create using specified format</summary>
		/// <param name="format">Adds message type automatically to the start of the message</param>
		protected LogMsg(string format) : this(DefaultLevel, format) { }

		/// <summary>Create using specified format</summary>
		/// <param name="level">Log Level</param>
		/// <param name="format">Adds message type automatically to the start of the message</param>
		protected LogMsg(LogLevel level, string format)
		{
			Level = level;
			Format = ("{MsgType} " + format).Trim();
		}
	}
}
