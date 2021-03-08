// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using AppMvc.Models;
using Jeebs;
using Jeebs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers
{
	public class AuthController : Jeebs.Mvc.Auth.Controllers.AuthController<UserModel>
	{
		public AuthController(IDataAuthProvider auth, ILog<AuthController> log) : base(auth, log) { }

		[Authorize]
		public IActionResult Index()
		{
			return View();
		}
	}
}
