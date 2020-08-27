using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Service configuration
	/// </summary>
	public abstract class ServiceConfig
	{
		/// <summary>
		/// Whether or not this service configuration is valid
		/// </summary>
		public abstract bool IsValid();
	}
}
