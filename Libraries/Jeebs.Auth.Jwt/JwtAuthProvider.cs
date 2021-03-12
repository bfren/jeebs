// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Security.Claims;
using Jeebs.Config;
using JeebsF;
using Microsoft.Extensions.Options;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IJwtAuthProvider"/>
	public class JwtAuthProvider : IJwtAuthProvider
	{
		private readonly JwtConfig config;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="config">JwtConfig</param>
		public JwtAuthProvider(IOptions<JwtConfig> config) : this(config.Value) { }

		internal JwtAuthProvider(JwtConfig config) =>
			this.config = config;

		/// <inheritdoc/>
		public Option<string> CreateToken(ClaimsPrincipal principal) =>
			JwtF.CreateToken(config, principal);

		/// <inheritdoc/>
		public Option<ClaimsPrincipal> ValidateToken(string token) =>
			JwtF.ValidateToken(config, token);
	}
}
