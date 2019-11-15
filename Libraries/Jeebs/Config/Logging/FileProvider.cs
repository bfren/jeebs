using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config.Logging
{
	/// <summary>
	/// File Provider
	/// </summary>
	public sealed class FileProvider : LoggingProvider
	{
		/// <summary>
		/// Path to log file
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Whether or not this provider's configuraiton is valid
		/// </summary>
		/// <returns>Returns true if a path is specified and that file exists</returns>
		public override bool IsValid() => !string.IsNullOrEmpty(Path) && System.IO.File.Exists(Path);

		/// <summary>
		/// Create object
		/// </summary>
		public FileProvider()
		{
			Path = string.Empty;
		}
	}
}
