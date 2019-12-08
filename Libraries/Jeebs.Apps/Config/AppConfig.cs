using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Apps.Config
{
	/// <summary>
	/// Jeebs Application Configuraiton
	/// </summary>
	public sealed class AppConfig
	{
		/// <summary>
		/// Project Name
		/// </summary>
		public string Project { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		public AppConfig()
		{
			Project = string.Empty;
		}
	}
}
