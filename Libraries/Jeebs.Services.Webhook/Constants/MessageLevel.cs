using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Services.Webhook
{
	/// <summary>
	/// Message levels
	/// </summary>
	public enum MessageLevel
	{
		/// <summary>
		/// Information, or 'Green' message
		/// </summary>
		Information,

		/// <summary>
		/// Warning, or 'Amber' message
		/// </summary>
		Warning,

		/// <summary>
		/// Error, or 'Red' message
		/// </summary>
		Error
	}
}
