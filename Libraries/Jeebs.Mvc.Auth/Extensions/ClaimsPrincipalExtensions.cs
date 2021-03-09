// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using System.Security.Claims;
using Jeebs.Auth;
using Jm.Mvc.Auth.ClaimsPrincipalExtensions;

namespace Jeebs.Mvc.Auth
{
	/// <summary>
	/// <see cref="ClaimsPrincipal"/> extension methods
	/// </summary>
	public static class ClaimsPrincipalExtensions
	{
		/// <summary>
		/// Returns the ID of the current user
		/// </summary>
		/// <param name="this">CLaimsPrincipal</param>
		public static Option<long> GetUserId(this ClaimsPrincipal @this)
		{
			if (@this.Identity?.IsAuthenticated == true)
			{
				return getId();
			}

			return Option.None<long>(new UserIsNotAuthenticatedMsg());

			// Find and parse ID from the list of claims
			Option<long> getId() =>
				@this.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.UserId) switch
				{
					Claim idClaim =>
						long.TryParse(idClaim.Value, out long userId) switch
						{
							true =>
								userId,

							false =>
								Option.None<long>(new InvalidUserIdMsg())
						},

					_ =>
						Option.None<long>(new UnableToFindUserIdClaimMsg())
				};
		}

		/// <summary>
		/// Returns whether or not the current user is a Super user
		/// </summary>
		/// <param name="this">CLaimsPrincipal</param>
		public static bool IsSuper(this ClaimsPrincipal @this) =>
			@this.Identity?.IsAuthenticated == true
			&& @this.Claims.Any(c => c.Type == JwtClaimTypes.IsSuper);

		/// <summary>
		/// Returns whether or not the current user has the specified claim
		/// </summary>
		/// <param name="this">ClaimsPrincipal</param>
		/// <param name="value">Claim</param>
		public static bool HasClaim(this ClaimsPrincipal @this, string value) =>
			@this.Identity?.IsAuthenticated == true
			&& @this.Claims.Any(c => string.Equals(c.Value, value, StringComparison.InvariantCultureIgnoreCase));
	}
}
