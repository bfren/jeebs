// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Ids;
using Jeebs.Data;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RndF;
using Wrap;
using Wrap.Extensions;
using Wrap.Linq;

namespace AppMvc.Controllers;

public class AuthController(AuthDataProvider auth, ILog<AuthController> log) :
	Jeebs.Mvc.Auth.Controllers.AuthController(auth, log)
{
	[Authorize]
	public IActionResult Index() =>
		View();

	[Authorize(Roles = "One")]
	public IActionResult Allow() =>
		View();

	[Authorize(Roles = "Three")]
	public IActionResult Deny() =>
		View();

	public async Task<IActionResult> InsertTestData()
	{
		var userId = await (
			from user in Auth.User.CreateAsync("ben@bcgdesign.com", "fred", "Ben")
			from r1 in Auth.Role.CreateAsync("One")
			from r2 in Auth.Role.CreateAsync("Two")
			from r3 in Auth.Role.CreateAsync("Three")
			from ur1 in Auth.UserRole.CreateAsync(user, r1)
			from ur2 in Auth.UserRole.CreateAsync(user, r2)
			select user
		);

		return Content(userId.ToString());
	}

	public async Task<IActionResult> ShowUser(AuthUserId id) =>
		await Auth
			.User.RetrieveAsync<UpdateUserModel>(
				id
			)
			.MapAsync(
				View
			)
			.UnwrapAsync(
				f => View("Unknown", f)
			);

	[HttpPost]
	public async Task<IActionResult> UpdateUser(UpdateUserModel model) =>
		await Auth
			.User.UpdateAsync(
				model
			)
			.MapIfAsync<bool, IActionResult>(
				x => x,
				_ => RedirectToAction("ShowUser", new { id = model.Id.Value })
			)
			.UnwrapAsync(
				f => View("Unknown", f)
			);

	public async Task<IActionResult> UpdateUser() =>
		await Auth
			.User.RetrieveAsync<UpdateUserModel>(
				new AuthUserId { Value = 1 }
			)
			.BindAsync(
				x => Auth.User.UpdateAsync(x with { FriendlyName = Rnd.Str })
			)
			.BindAsync(
				_ => Auth.User.RetrieveAsync<AuthUserModel>(new AuthUserId { Value = 1 })
			)
			.MapAsync<AuthUserModel, IActionResult>(
				x => Content(x.ToString())
			)
			.UnwrapAsync(
				_ => Content("Ooops")
			)
			.ConfigureAwait(false);

	public async Task<IActionResult> ShowUserByEmail(string email) =>
		await Auth
			.User.RetrieveAsync<UpdateUserModel>(
				email
			)
			.MapAsync(
				x => View("ShowUser", x)
			)
			.UnwrapAsync(
				f => View("Unknown", f)
			);

	public sealed record class UpdateUserModel : IWithId<AuthUserId, long>, IWithVersion
	{
		public AuthUserId Id { get; init; } = new();

		public long Version { get; init; }

		public string FriendlyName { get; init; } = string.Empty;
	}
}
