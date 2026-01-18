// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Security.Claims;
using Jeebs.Auth.Data;
using Jeebs.Auth.Jwt.Constants;
using Jeebs.Messages;

namespace Jeebs.Mvc.Auth;

/// <summary>
/// <see cref="ClaimsPrincipal"/> extension methods.
/// </summary>
public static class ClaimsPrincipalExtensions
{
	/// <summary>
	/// Returns a specified claim for the current user.
	/// </summary>
	/// <param name="this">ClaimsPrincipal.</param>
	/// <param name="type">Claim type.</param>
	public static Maybe<string> GetClaim(this ClaimsPrincipal @this, string type)
	{
		if (@this.Identity?.IsAuthenticated != true)
		{
			return F.None<string, M.UserIsNotAuthenticatedMsg>();
		}

		return @this.Claims
			.SingleOrNone(c => c.Type == type)
			.Select(x => x.Value);
	}
	/// <summary>
	/// Returns the ID of the current user.
	/// </summary>
	/// <param name="this">CLaimsPrincipal.</param>
	public static Maybe<AuthUserId> GetUserId(this ClaimsPrincipal @this) =>
		@this
			.GetClaim(
				JwtClaimTypes.UserId
			)
			.Bind(
				x => F.ParseInt64(x).Switch(
					some: y => F.Some(new AuthUserId { Value = y }),
					none: _ => F.None<AuthUserId, M.InvalidUserIdMsg>()
				)
			);

	/// <summary>
	/// Returns whether or not the current user has the specified claim.
	/// </summary>
	/// <param name="this">ClaimsPrincipal.</param>
	/// <param name="type">Claim type.</param>
	public static bool HasClaim(this ClaimsPrincipal @this, string type) =>
		@this.GetClaim(type).IsSome(out var _);

	/// <summary>
	/// Returns whether or not the current user is a Super user.
	/// </summary>
	/// <param name="this">CLaimsPrincipal.</param>
	public static bool IsSuper(this ClaimsPrincipal @this) =>
		@this.HasClaim(JwtClaimTypes.IsSuper);

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>The User ID Claim was not a valid number</summary>
		public sealed record class InvalidUserIdMsg : Msg;

		/// <summary>Unable to find the User ID Claim</summary>
		public sealed record class UnableToFindUserIdClaimMsg : Msg;

		/// <summary>The current User is not correctly authenticated</summary>
		public sealed record class UserIsNotAuthenticatedMsg : Msg;
	}
}
