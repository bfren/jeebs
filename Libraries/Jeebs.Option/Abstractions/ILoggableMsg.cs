// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Logging
{
	/// <summary>
	/// Represents a framework message supporting logging
	/// </summary>
	public interface ILoggableMsg : IMsg
	{
		/// <summary>
		/// Log message format
		/// </summary>
		string Format { get; }

		/// <summary>
		/// Log message parameters
		/// </summary>
		object[] ParamArray { get; }

		/// <summary>
		/// Log level
		/// </summary>
		LogLevel Level { get; }
	}
}
