using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Apps.Config.Logging
{
	/// <summary>
	/// Console Provider
	/// </summary>
	public sealed class ConsoleProvider : LoggingProvider
	{
		/// <summary>
		/// Whether or not this provider's configuraiton is valid
		/// </summary>
		/// <returns>Always returns true - there is no configuraiton</returns>
		public override bool IsValid() => true;
	}
}
