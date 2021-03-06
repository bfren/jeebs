using AppMvc.Models;
using Jeebs;
using Jeebs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers
{
	public class AuthController : Jeebs.Mvc.Auth.Controllers.AuthController<UserModel>
	{
		public AuthController(IDataAuthProvider auth, ILog<AuthController> log) : base(auth, log) { }

		public IActionResult Index()
		{
			return View();
		}
	}
}
