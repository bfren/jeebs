using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Microsoft.AspNetCore.Mvc;

namespace Jm.Mvc
{
	/// <summary>
	/// See <see cref="Jeebs.Mvc.Controller.ProcessResult{T}(IR{T}, Func{T, IActionResult})"/>
	/// </summary>
	public class Controller_ProcessResult_Unknown_IR : IMsg { }

	/// <summary>
	/// See <see cref="Jeebs.Mvc.Controller.ProcessResultAsync{T}(IR{T}, Func{T, Task{IActionResult}})"/>
	/// </summary>
	public class Controller_ProcessResultAsync_Unknown_IR : IMsg { }
}
