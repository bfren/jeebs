// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth.Jwt.Functions;
using Jeebs.Config.Web.Auth.Jwt;
using Microsoft.Extensions.Options;

namespace Jeebs.Auth;

/// <inheritdoc cref="IAuthJwtProvider"/>
public sealed class AuthJwtProvider : IAuthJwtProvider
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
	public Maybe<string> CreateToken(ClaimsPrincipal principal) =>
		JwtF.CreateToken(config, principal);

	/// <inheritdoc/>
	public Maybe<ClaimsPrincipal> ValidateToken(string token) =>
		JwtF.ValidateToken(config, token);
}
