using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Apps;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
	public sealed class Program : Jeebs.Apps.Program
	{
		private static async Task Main(string[] args) => await Main<App>(args, (provider, config) =>
		{
			var log = provider.GetService<ILog>();

			log.Debug("Services loaded");
			log.Debug("Project {ProjectName}", config.GetJeebsConfig().App.Project);

			Console.ReadLine();




			var r1 = Do(true);
			var r2 = Do(false);

			if (r1.Err is ErrorList e1)
			{
				Console.WriteLine($"r1 failed : {e1}");
			}
			else
			{
				Console.WriteLine($"r1 value : {r1.Val}");
			}

			if (r2.Err is ErrorList e2)
			{
				Console.WriteLine($"r2 failed : {e2}");
			}
			else
			{
				Console.WriteLine($"r2 value : {r2.Val}");
			}



			Result<Test> Do(bool succeed)
			{
				if (succeed)
				{
					return Result.Success(new Test { Text = "Success", Num = 2 });
				}
				else
				{
					return Result.Failure<Test>(new[] { "Something went wrong" });
				}
			}
		});
	}

	class Test
	{
		public string Text { get; set; }

		public int Num { get; set; }
	}
}
