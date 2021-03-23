// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static F.OptionF;

namespace AppMvc.Controllers
{
	public class AuthController : Jeebs.Mvc.Auth.Controllers.AuthController
	{
		private readonly AuthDb db;

		public AuthController(
			AuthDataProvider auth,
			AuthDb db,
			ILog<AuthController> log
		) : base(auth, log) =>
			(this.db) = (db);

		[Authorize]
		public IActionResult Index() =>
			View();

		[Authorize(Roles = "One")]
		public IActionResult Allow() =>
			View();

		[Authorize(Roles = "Three")]
		public IActionResult Deny() =>
			View();

		public IActionResult Migrate()
		{
			db.MigrateToLatest();
			return Content("Done");
		}

		public async Task<IActionResult> InsertTestData()
		{
			var id = await Auth.CreateUserAsync(new AuthCreateUserModel("ben@bcgdesign.com", "fred"));
			return Content(id.ToString());
		}

		public async Task<IActionResult> ShowUser(AuthUserId id) =>
			await Auth
				.RetrieveUserAsync<UpdateUserModel>(
					id
				)
				.MapAsync(
					x => View(x),
					DefaultHandler
				)
				.UnwrapAsync(
					x => x.Value(() => View("Unknown"))
				);

		[HttpPost]
		public async Task<IActionResult> UpdateUser(UpdateUserModel model) =>
			await Auth
				.UpdateUserAsync(
					model
				)
				.MapAsync(
					_ => RedirectToAction("ShowUser", new { id = model.Id.Value }),
					DefaultHandler
				)
				.UnwrapAsync(
					x => x.Value(() => throw new System.Exception())
				);

		public async Task<IActionResult> UpdateUser() =>
			await Auth
				.RetrieveUserAsync<UpdateUserModel>(
					new(1)
				)
				.SwitchAsync(
					some: async x => await Auth.UpdateUserAsync(x with { FriendlyName = F.Rnd.Str }),
					none: r => None<bool>(r).AsTask
				)
				.BindAsync(
					_ => Auth.RetrieveUserAsync<AuthUserModel>(new(1))
				)
				.MapAsync(
					x => Content(x.ToString()),
					DefaultHandler
				)
				.UnwrapAsync(
					x => x.Value(r => Content(r.ToString()))
				);

		public sealed record UpdateUserModel : IWithId<AuthUserId>, IWithVersion
		{
			public AuthUserId Id { get; init; } = new();

			public long Version { get; init; }

			public string FriendlyName { get; init; } = string.Empty;
		}
	}
}
