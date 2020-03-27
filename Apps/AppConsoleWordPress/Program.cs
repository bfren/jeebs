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
using Jeebs.Data.Enums;
using Jeebs.Util;
using Jeebs.WordPress;
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

				Console.WriteLine();
				Console.WriteLine("== BCG Terms ==");
				var count0 = w0.ExecuteScalar<int>($"SELECT COUNT(*) FROM {_0.Term} WHERE {_0.Term.Slug} LIKE @a;", new { a = "%a%" });
				Console.WriteLine(count0.Err is ErrorList
					? $"{count0.Err}"
					: $"There are {count0.Val} terms in BCG."
				);

				Console.WriteLine();
				Console.WriteLine("== USA Terms ==");
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

				Console.WriteLine();
				Console.WriteLine("== Option Insert ==");
				var inserted = await w0.InsertAsync(opt);
				Console.WriteLine(inserted.Err is ErrorList
					? $"{inserted.Err}"
					: $"Test option '{inserted.Val.Key}' = '{inserted.Val.Value}'."
				);
			}

			using (var q = bcg.Db.Query)
			{
				var exec = q.QueryPosts<PostModel>(opt => opt.Limit = 3);
				var count = (await exec.Count()).Val;
				var posts = (await exec.Retrieve()).Val;
				Console.WriteLine();
				Console.WriteLine("== Meta ==");
				Console.WriteLine("{0} Posts", count);

				if ((await q.AddMetaToPosts(posts, p => p.Meta)).Err is ErrorList err)
				{
					Console.WriteLine("Error fetching meta");
					Console.WriteLine(err);
				}
				else
				{
					foreach (var post in posts)
					{
						Console.WriteLine("Post {0}", post.PostId);
						foreach (var item in post.Meta)
						{
							Console.WriteLine("{0}: {1}", item.Key, item.Value);
						}
					}
				}
			}

			using (var q = bcg.Db.Query)
			{
				var sermonsExec = q.QueryPosts<SermonModel>(opt =>
				{
					opt.SearchText = "holiness";
					opt.SearchOperator = SearchOperators.Like;
					opt.Type = WpBcg.PostTypes.Sermon;
					opt.Sort = new[] { (bcg.Db.Post.Title, SortOrder.Ascending) };
					opt.Limit = 20;
				});

				Console.WriteLine();
				Console.WriteLine("== Sermons: holiness ==");
				await Output(sermonsExec);

				var taxonomyExec = q.QueryPosts<SermonModel>(opt =>
				{
					opt.Type = WpBcg.PostTypes.Sermon;
					opt.SearchText = "jesus";
					opt.SearchFields = SearchPostFields.Title;
					opt.Taxonomies = new[] { (WpBcg.Taxonomies.BibleBook, 424) };
					opt.Limit = 20;
				});

				Console.WriteLine();
				Console.WriteLine("== Sermons: Luke ==");
				await Output(taxonomyExec);

				var customFieldExec = q.QueryPosts<SermonModel>(opt =>
				{
					opt.Type = WpBcg.PostTypes.Sermon;
					opt.Limit = 20;
				});
			}

			async Task Output(QueryExec<SermonModel> exec)
			{
				var count = await exec.Count();
				Console.WriteLine(count.Err is ErrorList
					? $"{count.Err}"
					: $"There are {count.Val} matching sermons."
				);

				var posts = await exec.Retrieve();
				if (posts.Err is ErrorList)
				{
					Console.WriteLine(posts.Err);
				}
				else
				{
					foreach (var item in posts.Val)
					{
						Console.WriteLine("{0:0000}: {1}", item.PostId, item.Title);
					}
				}
			}

			// End
			Console.WriteLine();
			Console.WriteLine("Complete.");
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

class PostModel : IEntity
{
	public long Id { get => PostId; set => PostId = value; }

	public long PostId { get; set; }

	public MetaDictionary Meta { get; set; }
}

class SermonModel
{
	public long PostId { get; set; }

	public string Title { get; set; }

	public DateTime PublishedOn { get; set; }
}
