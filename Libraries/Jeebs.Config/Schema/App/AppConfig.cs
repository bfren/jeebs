using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Jeebs Application Configuraiton
	/// </summary>
	public record AppConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":app";

		/// <summary>
		/// Application Name
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// The Application Suite property to add to log messages
		/// </summary>
		public string? Suite { get; set; }

		/// <summary>
		/// The full name - if <see cref="Suite"/> is set, returns <see cref="Suite"/>/<see cref="Name"/>,
		/// otherwise simply <see cref="Name"/>
		/// </summary>
		public string FullName =>
			Suite switch
			{
				string suite =>
					$"{suite}/",

				_ =>
					string.Empty
			} + Name;

		/// <summary>
		/// Application Version
		/// </summary>
		public Version Version { get; set; } = new Version(0, 1, 0, 0);
	}
}
