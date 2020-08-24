using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Jeebs
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
		/// Log level - default is set in <see cref="Defaults.Log.Level"/>
		/// </summary>
		LogLevel Level { get; }
	}
}
