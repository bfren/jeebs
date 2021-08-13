// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Linq;
using System.Security.Claims;
using Jeebs.Auth;
using static F.OptionF;

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
				return @this.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.UserId) switch
				{
					Claim idClaim =>
						long.TryParse(idClaim.Value, out long userId) switch
						{
							true =>
								userId,

							false =>
								None<long, Msg.InvalidUserIdMsg>()
						},

					_ =>
						None<long, Msg.UnableToFindUserIdClaimMsg>()
				};
			}

			return None<long, Msg.UserIsNotAuthenticatedMsg>();
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
			&& @this.Claims.Any(c => string.Equals(c.Value, value, StringComparison.OrdinalIgnoreCase));

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>The User ID Claim was not a valid number</summary>
			public sealed record class InvalidUserIdMsg : IMsg { }

			/// <summary>Unable to find the User ID Claim</summary>
			public sealed record class UnableToFindUserIdClaimMsg : IMsg { }

			/// <summary>The current User is not correctly authenticated</summary>
			public sealed record class UserIsNotAuthenticatedMsg : IMsg { }
		}
	}
}
