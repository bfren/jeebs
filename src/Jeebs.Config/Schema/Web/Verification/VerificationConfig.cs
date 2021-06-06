// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
{
	/// <summary>
	/// Site Verification Configuration
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

		/// <summary>
		/// True if there are any verification configurations
		/// </summary>
		public bool Any =>
			Google is not null;
	}
}
