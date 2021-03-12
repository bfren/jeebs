// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jm.Functions.JwtF.CreateToken
{
	/// <summary>
	/// See <see cref="F.JwtF.CreateToken(Jeebs.Config.JwtConfig, System.Security.Claims.ClaimsPrincipal, System.DateTime, System.DateTime)"/>
	/// </summary>
	public sealed class EncryptionAlgorithmNotSupportedMsg : WithValueMsg<string>
	{
		/// <summary>
		/// Set message value
		/// </summary>
		/// <param name="value">Value</param>
		public EncryptionAlgorithmNotSupportedMsg(string value) : base(value) { }
	}
}
