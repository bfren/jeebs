// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Logging;

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
		public virtual LogLevel Level =>
			Defaults.Log.Level;

		/// <summary>
		/// Use <see cref="Format"/> and <see cref="ParamArray"/>
		/// </summary>
		public override string ToString() =>
			Format.FormatWith(ParamArray);
	}
}
