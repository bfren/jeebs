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
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AppConsoleWordPress
{
	internal sealed class Program : Jeebs.Apps.Program
	{
		internal static async Task Main(string[] args) => await Main<App>(args, async (provider, _) =>
		{
			// Begin
			Console.WriteLine("= WordPress Console Test =");

			// Get services
			var bcg = provider.GetService<WpBcg>();
			var usa = provider.GetService<WpUsa>();

			// Perform tests
			await TermsAsync("BCG", bcg.Db);
			await TermsAsync("USA", usa.Db);
			await InsertOptionAsync(bcg.Db);
			await SearchSermonsAsync("holiness", bcg.Db, opt =>
			{
				opt.SearchText = "holiness";
				opt.SearchOperator = SearchOperators.Like;
				opt.Type = WpBcg.PostTypes.Sermon;
				opt.Sort = new[] { (bcg.Db.Post.Title, SortOrder.Ascending) };
				opt.Limit = 4;
			});
			await SearchSermonsAsync("jesus", bcg.Db, opt =>
			{
				opt.Type = WpBcg.PostTypes.Sermon;
				opt.SearchText = "jesus";
				opt.SearchFields = SearchPostFields.Title;
				opt.Taxonomies = new[] { (WpBcg.Taxonomies.BibleBook, 424) };
				opt.Limit = 5;
			});
			await FetchMeta(bcg.Db);
			await FetchCustomFields(bcg.Db);
			await FetchTaxonomies(bcg.Db);
			await ApplyContentFilters(bcg.Db);

			// End
			Console.WriteLine();
			Console.WriteLine("Complete.");
			Console.Read();
		});

		/// <summary>
		/// Select Terms
		/// </summary>
		/// <param name="section"></param>
		/// <param name="db"></param>
		internal static async Task TermsAsync(string section, IWpDb db)
		{
			Console.WriteLine();
			Console.WriteLine($"== {section} Terms ==");

			using var w = db.UnitOfWork;

			var count = await w.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {db.Term} WHERE {db.Term.Slug} LIKE @a;", new { a = "%a%" });
			Console.WriteLine(count.Err is IErrorList
				? $"{count.Err}"
				: $"There are {count.Val} terms in {section}."
			);
		}

		/// <summary>
		/// Insert an Option
		/// </summary>
		/// <param name="bcg"></param>
		internal static async Task InsertOptionAsync(IWpDb bcg)
		{
			Console.WriteLine();
			Console.WriteLine("== Option Insert ==");

			var opt = new Bcg.Entities.Option
			{
				Key = Guid.NewGuid().ToString(),
				Value = DateTime.Now.ToLongTimeString()
			};

			using var w = bcg.UnitOfWork;

			var inserted = await w.InsertAsync(opt);
			Console.WriteLine(inserted.Err is IErrorList
				? $"{inserted.Err}"
				: $"Test option '{inserted.Val.Key}' = '{inserted.Val.Value}'."
			);
		}

		/// <summary>
		/// Search sermons
		/// </summary>
		/// <param name="search"></param>
		/// <param name="bcg"></param>
		/// <param name="opt"></param>
		internal static async Task SearchSermonsAsync(string search, IWpDb bcg, Action<QueryPosts.Options> opt)
		{
			Console.WriteLine();
			Console.WriteLine($"== Sermons: {search} ==");

			using var q = bcg.GetQueryWrapper();

			var query = await q.QueryPostsAsync<SermonModel>(modify: opt);

			Console.WriteLine(query.Err is IErrorList
				? $"{query.Err}"
				: $"There are {query.Val.Count} matching sermons."
			);

			foreach (var item in query.Val)
			{
				Console.WriteLine("{0:0000}: {1}", item.PostId, item.Title);
			}
		}

		/// <summary>
		/// Fetch post meta
		/// </summary>
		/// <param name="db"></param>
		internal static async Task FetchMeta(IWpDb db)
		{
			Console.WriteLine();
			Console.WriteLine("== Meta ==");

			using var w = db.GetQueryWrapper();

			var result = await w.QueryPostsAsync<PostModel>(modify: opt => opt.Limit = 3);
			if (result.Err is IErrorList postsErr)
			{
				Console.WriteLine("Error fetching posts");
				Console.WriteLine(postsErr);
				return;
			}

			var posts = result.Val;
			foreach (var post in posts)
			{
				Console.WriteLine("Post {0}", post.PostId);
				foreach (var item in post.Meta)
				{
					Console.WriteLine("{0}: {1}", item.Key, item.Value);
				}
			}
		}

		/// <summary>
		/// Fetch post custom fields
		/// </summary>
		/// <param name="db"></param>
		internal static async Task FetchCustomFields(IWpDb db)
		{
			try
			{
				Console.WriteLine();
				Console.WriteLine("== Custom Fields ==");

				using var q = db.GetQueryWrapper();

				var query = await q.QueryPostsAsync<SermonModelWithCustomFields>(modify: opt =>
				{
					opt.Type = WpBcg.PostTypes.Sermon;
					opt.SortRandom = true;
					opt.Limit = 10;
				});

				if (query.Err is IErrorList sermonsErr)
				{
					Console.WriteLine("Error fetching sermons");
					Console.WriteLine(sermonsErr);
					return;
				}

				var sermons = query.Val;
				Console.WriteLine($"{sermons.Count()} sermons found");

				foreach (var sermon in sermons.ToList())
				{
					Console.WriteLine("{0:0000} '{1}'", sermon.PostId, sermon.Title);
					Console.WriteLine("  - Passage: {0}", sermon.Passage);
					Console.WriteLine("  - PDF: {0}", sermon.Pdf);
					Console.WriteLine("  - Audio: {0}", sermon.Audio);
					Console.WriteLine("  - First Preached: {0}", sermon.FirstPreached);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		/// <summary>
		/// Fetch taxonomies
		/// </summary>
		/// <param name="db"></param>
		internal static async Task FetchTaxonomies(IWpDb db)
		{
			try
			{
				Console.WriteLine();
				Console.WriteLine("== Taxonomies ==");

				using var q = db.GetQueryWrapper();

				var query = await q.QueryPostsAsync<SermonModelWithTaxonomies>(modify: opt =>
				{
					opt.Type = WpBcg.PostTypes.Sermon;
					opt.SortRandom = true;
					opt.Limit = 10;
				});

				if (query.Err is IErrorList sermonsErr)
				{
					Console.WriteLine("Error fetching sermons");
					Console.WriteLine(sermonsErr);
					return;
				}

				var sermons = query.Val;
				Console.WriteLine($"{sermons.Count} sermons found");

				foreach (var sermon in sermons)
				{
					Console.WriteLine("{0:0000} '{1}'", sermon.PostId, sermon.Title);

					foreach (var book in sermon.BibleBooks)
					{
						Console.WriteLine("  - Bible Book: {0}", book);
					}

					foreach (var series in sermon.Series)
					{
						Console.WriteLine("  - Series: {0}", series);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		/// <summary>
		/// Apply content filters
		/// </summary>
		/// <param name="db"></param>
		internal static async Task ApplyContentFilters(IWpDb db)
		{
			Console.WriteLine();
			Console.WriteLine("== Content Filters ==");

			using var w = db.GetQueryWrapper();

			var result = await w.QueryPostsAsync<PostModelWithContent>(modify: opt => opt.Limit = 3, filters: GenerateExcerpt.Create());
			if (result.Err is IErrorList postsErr)
			{
				Console.WriteLine("Error fetching posts with content filter");
				Console.WriteLine(postsErr);
				return;
			}

			foreach (var post in result.Val)
			{
				Console.WriteLine("Post {0}", post.PostId);
				Console.WriteLine("  - {0}", post.Content);
			}
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

	class PostModelWithContent : PostModel
	{
		public string Content { get; set; }
	}

	class SermonModel : IEntity
	{
		public long Id { get => PostId; set => PostId = value; }

		public long PostId { get; set; }

		public string Title { get; set; } = string.Empty;

		public DateTime PublishedOn { get; set; }
	}

	class SermonModelWithCustomFields : SermonModel
	{
		public MetaDictionary Meta { get; set; } = new MetaDictionary();

		public PassageCustomField Passage { get; set; } = new PassageCustomField();

		public PdfCustomField Pdf { get; set; } = new PdfCustomField();

		public AudioRecordingCustomField Audio { get; set; } = new AudioRecordingCustomField();

		public FirstPreachedCustomField FirstPreached { get; set; } = new FirstPreachedCustomField();
	}

	class SermonModelWithTaxonomies : SermonModel
	{
		public TermList BibleBooks { get; set; } = new TermList(WpBcg.Taxonomies.BibleBook);

		public TermList Series { get; set; } = new TermList(WpBcg.Taxonomies.Series);
	}
}
