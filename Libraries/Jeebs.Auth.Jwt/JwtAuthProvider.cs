// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Security.Claims;
using Jeebs.Config;
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
		/// <param name="config">JeebsConfig</param>
		public JwtAuthProvider(IOptions<JeebsConfig> config) : this(config.Value.Web.Jwt) { }

		internal JwtAuthProvider(JwtConfig config) =>
			this.config = config;

		/// <inheritdoc/>
		public Option<string> CreateToken(ClaimsPrincipal principal) =>
			F.JwtF.CreateToken(config, principal);

		/// <inheritdoc/>
		public Option<ClaimsPrincipal> ValidateToken(string token) =>
			F.JwtF.ValidateToken(config, token);
	}
}
