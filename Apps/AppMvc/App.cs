using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MvcApp
{
	public sealed class App : Jeebs.Apps.MvcApp
	{
		public App() : base(false) { }
	}
}
