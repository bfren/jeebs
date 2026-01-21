// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Data;
using Jeebs.Logging;
using MaybeF;
using MaybeF.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RndF;
using StrongId;

namespace AppMvc.Controllers;

public class AuthController : Jeebs.Mvc.Auth.Controllers.AuthController
{
	public AuthController(AuthDataProvider auth, ILog<AuthController> log) : base(auth, log) { }

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
				x => View(x),
				F.DefaultHandler
			)
			.UnwrapAsync(
				x => x.Value(() => View("Unknown"))
			);

	[HttpPost]
	public async Task<IActionResult> UpdateUser(UpdateUserModel model) =>
		await Auth
			.User.UpdateAsync(
				model
			)
			.MapAsync(
				_ => RedirectToAction("ShowUser", new { id = model.Id.Value }),
				F.DefaultHandler
			)
			.UnwrapAsync(
				x => x.Value(() => throw new Exception())
			);

	public async Task<IActionResult> UpdateUser() =>
		await Auth
			.User.RetrieveAsync<UpdateUserModel>(
				new AuthUserId { Value = 1 }
			)
			.SwitchAsync(
				some: async x => await Auth.User.UpdateAsync(x with { FriendlyName = Rnd.Str }),
				none: r => F.None<bool>(r).AsTask()
			)
			.BindAsync(
				_ => Auth.User.RetrieveAsync<AuthUserModel>(new AuthUserId { Value = 1 })
			)
			.MapAsync(
				x => Content(x.ToString()),
				F.DefaultHandler
			)
			.UnwrapAsync(
				x => x.Value(Content("Ooops"))
			);

	public async Task<IActionResult> ShowUserByEmail(string email) =>
		await Auth
			.User.RetrieveAsync<UpdateUserModel>(
				email
			)
			.MapAsync(
				x => View("ShowUser", x),
				F.DefaultHandler
			)
			.UnwrapAsync(
				x => x.Value(() => View("Unknown"))
			);

	public sealed record class UpdateUserModel : IWithId<AuthUserId>, IWithVersion
	{
		public AuthUserId Id { get; init; } = new();

		public long Version { get; init; }

		public string FriendlyName { get; init; } = string.Empty;
	}
}
