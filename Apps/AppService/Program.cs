using System;
using System.Threading.Tasks;

namespace ServiceApp
{
	public sealed class Program : Jeebs.Apps.Program
	{
		private static async Task Main(string[] args) => await Main<App>(args);
	}
}
