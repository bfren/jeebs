// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Functions.JwtF.ValidateToken
{
	/// <summary>
	/// See <see cref="JeebsF.JwtF.ValidateToken(Jeebs.Config.JwtConfig, string)"/>
	/// </summary>
	public sealed class ErrorValidatingTokenMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public ErrorValidatingTokenMsg(Exception e) : base(e) { }
	}
}
