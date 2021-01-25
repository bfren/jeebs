using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppWeb
{
	public sealed class Program : Jeebs.Apps.Program
	{
		private static async Task Main(string[] args) =>
			await Main<App>(args).ConfigureAwait(false);
	}
}
