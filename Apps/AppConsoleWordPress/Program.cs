using System;
using System.Threading.Tasks;
using AppConsoleWordPress.Bcg;
using AppConsoleWordPress.Usa;
using Jeebs;
using Jeebs.Apps;
using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AppConsoleWordPress
{
	internal sealed class Program : Jeebs.Apps.Program
	{
		internal static async Task Main(string[] args) => await Main<App>(args, async (provider, config) =>
		{
			// Begin
			Console.WriteLine("= WordPress Console Test =");

			// Get BCG
			var bcg = provider.GetService<WpBcg>();
			var usa = provider.GetService<WpUsa>();

			using (UnitOfWork w0 = bcg.Db.UnitOfWork, w1 = usa.Db.UnitOfWork)
			{
				var _0 = bcg.Db;
				var _1 = usa.Db;

				var count0 = w0.ExecuteScalar<int>($"SELECT COUNT(*) FROM {_0.Term} WHERE {_0.Term.Slug} LIKE @a;", new { a = "%a%" });
				if (count0 is Success<int> success0)
				{
					Console.WriteLine($"There are {success0.Value} terms in BCG.");
				}

				var count1 = w0.ExecuteScalar<int>($"SELECT COUNT(*) FROM {_1.Term} WHERE {_1.Term.Slug} LIKE @a;", new { a = "%a%" });
				if (count1 is Success<int> success1)
				{
					Console.WriteLine($"There are {success1.Value} terms in USA.");
				}

				var opt = new Bcg.Entities.Option
				{
					Key = Guid.NewGuid().ToString(),
					Value = DateTime.Now.ToLongTimeString()
				};

				var inserted = await w0.InsertAsync(opt);
				if (inserted is Success<Bcg.Entities.Option> success)
				{
					Console.WriteLine($"Test option '{success.Value.Key}' = '{success.Value.Value}'.");
				}
				else
				{
					Console.WriteLine($"{inserted}");
				}
			}

			// End
			Console.Read();
		});
	}
}
