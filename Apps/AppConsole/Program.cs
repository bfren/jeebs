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

			var r1 = Do(true);
			var r2 = Do(false);

			Write(r1, 1);
			Write(r2, 2);

			Console.ReadLine();

			static IResult<Test> Do(bool succeed)
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

			static void Write(IResult<Test> r, int num)
			{
				Console.WriteLine(r.Err is ErrorList e ? $"r{num} failed : {e}" : $"r{num} value : Text = '{r.Val.Text}', Num = {r.Val.Num}");
			}
		});
	}

	class Test
	{
		public string Text { get; set; } = string.Empty;

		public int Num { get; set; }
	}
}
