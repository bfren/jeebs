using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Jeebs
{
	/// <inheritdoc cref="ILoggableMsg"/>
	public abstract class LoggableMsg : ILoggableMsg
	{
		/// <inheritdoc/>
		public abstract string Format { get; }

		/// <inheritdoc/>
		public abstract object[] ParamArray { get; }

		/// <inheritdoc/>
		public virtual LogLevel Level
			=> Defaults.Log.Level;
	}
}
