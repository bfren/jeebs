using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Apps.WebApps.Config
{
	/// <summary>
	/// Site Veritifaction Configuration
	/// </summary>
	public sealed class SiteVerificationConfig
	{
		/// <summary>
		/// Google Site Verification page
		/// </summary>
		public string? Google
		{
			get => googleCode is null ? null : $"google{googleCode}.html";
			set => googleCode = value;
		}

		private string? googleCode;
	}
}
