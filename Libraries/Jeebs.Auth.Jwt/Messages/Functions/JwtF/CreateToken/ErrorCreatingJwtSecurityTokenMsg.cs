// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Functions.JwtF.CreateToken
{
	/// <summary>
	/// See <see cref="JeebsF.JwtF.CreateToken(Jeebs.Config.JwtConfig, System.Security.Claims.ClaimsPrincipal, System.DateTime, System.DateTime)"/>
	/// </summary>
	public sealed class ErrorCreatingJwtSecurityTokenMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public ErrorCreatingJwtSecurityTokenMsg(Exception e) : base(e) { }
	}
}
