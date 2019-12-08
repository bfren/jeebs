using System;
using System.Threading.Tasks;
using AppConsoleWordPress.Bcg;
using Microsoft.Extensions.DependencyInjection;

namespace AppConsoleWordPress
{
	internal sealed class Program : Jeebs.Apps.Program
	{
		internal static async Task Main(string[] args) => await Main<App>(args, (provider, config) =>
		{
			// Begin
			Console.WriteLine("= WordPress Console Test =");

			// Get BCG
			var bcg = provider.GetService<WpBcg>();
			using (var w = bcg.Db.UnitOfWork)
			{
				var count = w.ExecuteScalar<int>($"SELECT COUNT(*) FROM {bcg.Db.Term}");
				Console.WriteLine($"There are {count} terms.");
			}


			// End
			Console.Read();
		});
	}
}
