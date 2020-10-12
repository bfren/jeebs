using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Site Veritifaction Configuration
	/// </summary>
	public class VerificationConfig
	{
		/// <summary>
		/// Path to this configuration section
		/// </summary>
		public const string Key = WebConfig.Key + ":verification";

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
