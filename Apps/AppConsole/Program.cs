using System;
using System.Threading.Tasks;
using Jeebs;
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
		});
	}
}
