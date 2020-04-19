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
		public string Path { get; set; } = string.Empty;

		/// <inheritdoc/>
		public override bool IsValid() => !string.IsNullOrEmpty(Path) && System.IO.File.Exists(Path);
	}
}
