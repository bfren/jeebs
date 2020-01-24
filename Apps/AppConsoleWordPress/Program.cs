using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppConsoleWordPress.Bcg;
using AppConsoleWordPress.Usa;
using Jeebs;
using Jeebs.Apps;
using Jeebs.Config;
using Jeebs.Data;
using Jeebs.Util;
using Jeebs.WordPress.Enums;
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

			using (IUnitOfWork w0 = bcg.Db.UnitOfWork, w1 = usa.Db.UnitOfWork)
			{
				var _0 = bcg.Db;
				var _1 = usa.Db;

				var count0 = w0.ExecuteScalar<int>($"SELECT COUNT(*) FROM {_0.Term} WHERE {_0.Term.Slug} LIKE @a;", new { a = "%a%" });
				Console.WriteLine(count0.Err is ErrorList
					? $"{count0.Err}"
					: $"There are {count0.Val} terms in BCG."
				);

				var count1 = w0.ExecuteScalar<int>($"SELECT COUNT(*) FROM {_1.Term} WHERE {_1.Term.Slug} LIKE @a;", new { a = "%a%" });
				Console.WriteLine(count1.Err is ErrorList
					? $"{count1.Err}"
					: $"There are {count1.Val} terms in USA."
				);

				var opt = new Bcg.Entities.Option
				{
					Key = Guid.NewGuid().ToString(),
					Value = DateTime.Now.ToLongTimeString()
				};

				var inserted = await w0.InsertAsync(opt);
				Console.WriteLine(inserted.Err is ErrorList
					? $"{inserted.Err}"
					: $"Test option '{inserted.Val.Key}' = '{inserted.Val.Value}'."
				);
			}

			// End
			Console.Read();
		});
	}
}

class TermModel
{
	public string Title { get; set; }

	public Taxonomy Taxonomy { get; set; }

	public int Count { get; set; }
}
