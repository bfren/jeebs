// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth;
using Jeebs.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using static F.OptionF;

namespace Jeebs.Mvc.Auth.Jwt;

/// <summary>
/// JWT Authorisation Handler - extracts and validates JWT from the authorisation header
/// </summary>
public class JwtHandler : AuthorizationHandler<JwtRequirement>
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
			GetAuthorisedPrincipal(filterContext).Switch(
				some: principal =>
				{
					filterContext.HttpContext.User = principal;
					context.Succeed(requirement);
				},
				none: reason =>
				{
					Log.Message(reason);
					context.Fail();
				}
			);
		}

		return Task.CompletedTask;
	}

	/// <summary>
	/// Attempt to get the authorised ClaimsPrincipal
	/// </summary>
	/// <param name="ctx">AuthorizationFilterContext</param>
	private static Option<ClaimsPrincipal> GetAuthorisedPrincipal(AuthorizationFilterContext ctx) =>
		from authorisationHeader in
			GetAuthorisationHeader(ctx.HttpContext.Request.Headers)
		from token in
			GetToken(authorisationHeader)
		from principal in
			GetPrincipal(ctx.HttpContext.RequestServices.GetRequiredService<IAuthJwtProvider>(), token)
		select principal;

	/// <summary>
	/// Retrieve the authorisation header (if it exists)
	/// </summary>
	/// <param name="headers">Dictionary of header values</param>
	internal static Option<string> GetAuthorisationHeader(IDictionary<string, StringValues> headers) =>
		headers.TryGetValue("Authorization", out var authorisationHeader) switch
		{
			true when !string.IsNullOrEmpty(authorisationHeader) =>
				authorisationHeader.ToString(),

			_ =>
				None<string, M.MissingAuthorisationHeaderMsg>()
		};

	/// <summary>
	/// Extract the token from the authorisation header
	/// </summary>
	/// <param name="authorisationHeader">Authorisation header</param>
	internal static Option<string> GetToken(string authorisationHeader) =>
		authorisationHeader.StartsWith("Bearer ") switch
		{
			true =>
				authorisationHeader["Bearer ".Length..].Trim(),

			_ =>
				None<string, M.InvalidAuthorisationHeaderMsg>()
		};

	/// <summary>
	/// Retrieve the user from the supplied token
	/// </summary>
	/// <param name="auth">IJwtAuthProvider</param>
	/// <param name="token">Token value</param>
	internal static Option<ClaimsPrincipal> GetPrincipal(IAuthJwtProvider auth, string token) =>
		auth.ValidateToken(token);

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Unable to find Authorization header in headers dictionary</summary>
		public sealed record class MissingAuthorisationHeaderMsg : Msg;

		/// <summary>The Authorization header was not a valid Bearer</summary>
		public sealed record class InvalidAuthorisationHeaderMsg : Msg;
	}
}
