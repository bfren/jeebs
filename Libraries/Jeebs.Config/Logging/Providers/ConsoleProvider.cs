using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config.Logging
{
	/// <summary>
	/// Console Provider
	/// </summary>
	public sealed class ConsoleProvider : LoggingProvider
	{
		/// <inheritdoc/>
		public override bool IsValid()
			=> true;
	}
}
