// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth;
using Jm.Mvc.Auth.Jwt.JwtHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Jeebs.Mvc.Auth.Jwt
{
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
		/// <param name="handlerContext">AuthorizationHandlerContext</param>
		/// <param name="requirement">JwtRequirement</param>
		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext handlerContext, JwtRequirement requirement)
		{
			if (handlerContext.Resource is AuthorizationFilterContext filterContext)
			{
				GetAuthorisedPrincipal(filterContext).Switch(
					some: principal =>
					{
						filterContext.HttpContext.User = principal;
						handlerContext.Succeed(requirement);
					},
					none: reason =>
					{
						Log.Message(reason);
						handlerContext.Fail();
					}
				);
			}
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
				GetPrincipal(ctx.HttpContext.RequestServices.GetRequiredService<IJwtAuthProvider>(), token)
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
					Option.None<string>(new MissingAuthorisationHeaderMsg())
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
					Option.None<string>(new InvalidAuthorisationHeaderMsg())
			};

		/// <summary>
		/// Retrieve the user from the supplied token
		/// </summary>
		/// <param name="auth">IJwtAuthProvider</param>
		/// <param name="token">Token value</param>
		internal static Option<ClaimsPrincipal> GetPrincipal(IJwtAuthProvider auth, string token) =>
			auth.ValidateToken(token);
	}
}
