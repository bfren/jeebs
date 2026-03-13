// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Auth.Jwt;

/// <summary>
/// JWT Authorisation Handler - extracts and validates JWT from the HTTP Authorization header.
/// </summary>
public sealed class JwtHandler : AuthorizationHandler<JwtRequirement>
{
	private ILog Log { get; init; }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="log">ILog.</param>
	public JwtHandler(ILog<JwtHandler> log) =>
		Log = log;

	/// <summary>
	/// Handle Requirement.
	/// </summary>
	/// <param name="context">AuthorizationHandlerContext.</param>
	/// <param name="requirement">JwtRequirement.</param>
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtRequirement requirement)
	{
		if (context.Resource is DefaultHttpContext http)
		{
			// Attempt to retrieve the authorised principal
			var auth = http.RequestServices.GetRequiredService<IAuthJwtProvider>();
			var principal = from h in HttpF.GetAuthorizationHeader(http.Request.Headers)
							from t in HttpF.GetToken(h)
							from p in auth.ValidateToken(t)
							select p;

			// Set user on success
			principal.Match(
				fOk: x =>
				{
					http.User = x;
					context.Succeed(requirement);
				},
				fFail: f =>
				{
					Log.Failure(f);
					context.Fail();
				}
			);
		}

		return Task.CompletedTask;
	}
}
