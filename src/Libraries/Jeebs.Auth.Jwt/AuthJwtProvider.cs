// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Security.Claims;
using Jeebs.Config;
using Microsoft.Extensions.Options;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthJwtProvider"/>
	public class AuthJwtProvider : IAuthJwtProvider
	{
		private readonly JwtConfig config;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="config">JwtConfig</param>
		public AuthJwtProvider(IOptions<JwtConfig> config) : this(config.Value) { }

		internal AuthJwtProvider(JwtConfig config) =>
			this.config = config;

		/// <inheritdoc/>
		public Option<string> CreateToken(ClaimsPrincipal principal) =>
			F.JwtF.CreateToken(config, principal);

		/// <inheritdoc/>
		public Option<ClaimsPrincipal> ValidateToken(string token) =>
			F.JwtF.ValidateToken(config, token);
	}
}
