// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jeebs.Mvc.Auth.Jwt;

/// <summary>
/// JWT Authorisation Handler - extracts and validates JWT from the authorisation header
/// </summary>
public sealed class JwtHandler : AuthorizationHandler<JwtRequirement>
{
	private ILog Log { get; init; }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="log">ILog</param>
	public JwtHandler(ILog<JwtHandler> log) =>
		Log = log;

	/// <summary>
	/// Handle Requirement
	/// </summary>
	/// <param name="context">AuthorizationHandlerContext</param>
	/// <param name="requirement">JwtRequirement</param>
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtRequirement requirement)
	{
		if (context.Resource is AuthorizationFilterContext filterContext)
		{
			JwtF.GetAuthorisedPrincipal(filterContext).Switch(
				some: principal =>
				{
					filterContext.HttpContext.User = principal;
					context.Succeed(requirement);
				},
				none: reason =>
				{
					Log.Msg(reason);
					context.Fail();
				}
			);
		}

		return Task.CompletedTask;
	}
}
