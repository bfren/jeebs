using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Site Veritifaction Configuration
	/// </summary>
	public record VerificationConfig
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
			get =>
				F.StringF.Format("google{0}.html", googleCode);

			init =>
				googleCode = value;
		}

		private string? googleCode;
	}
}
