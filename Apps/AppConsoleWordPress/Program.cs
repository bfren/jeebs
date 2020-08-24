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
		internal static async Task Main(string[] args)
			=> await Main<App>(args, async (provider, _) =>
			{
				// Begin
				Console.WriteLine("= WordPress Console Test =");

				// Get services
				var log = provider.GetService<ILog<Program>>();
				var bcg = provider.GetService<WpBcg>();
				var usa = provider.GetService<WpUsa>();

				// Run test methods
				TermsAsync(Result.Ok(), "BCG", bcg.Db).Await().Audit(AuditTerms);
				(await TermsAsync(Result.Ok(), "USA", usa.Db)).Audit(AuditTerms);

				Chain.Create()
					.AddLogger(log)
					.Link().MapAsync(r => InsertOptionAsync(r, bcg.Db)).Await()
					.Audit(AuditOption);

				Chain.Create(bcg.Db)
					.Link().MapAsync(r => SearchSermonsAsync(r, "holiness", opt =>
					{
						opt.SearchText = "holiness";
						opt.SearchOperator = SearchOperators.Like;
						opt.Type = WpBcg.PostTypes.Sermon;
						opt.Sort = new[] { (bcg.Db.Post.Title, SortOrder.Ascending) };
						opt.Limit = 4;
					})).Await()
					.Audit(AuditSermons);

				Chain.Create(bcg.Db)
					.Link().MapAsync(r => SearchSermonsAsync(r, "jesus", opt =>
					{
						opt.Type = WpBcg.PostTypes.Sermon;
						opt.SearchText = "jesus";
						opt.SearchFields = SearchPostFields.Title;
						opt.Taxonomies = new[] { (WpBcg.Taxonomies.BibleBook, 424) };
						opt.Limit = 5;
					})).Await()
					.Audit(AuditSermons);

				Chain.Create(bcg.Db)
					.AddLogger(log)
					.Link().MapAsync(FetchMeta).Await()
					.Audit(AuditMeta)
					.Link().MapAsync(FetchCustomFields).Await()
					.Audit(AuditCustomFields);

				// Perform tests
				//await FetchMeta(bcg.Db).ConfigureAwait(false);
				//await FetchCustomFields(bcg.Db).ConfigureAwait(false);
				//await FetchTaxonomies(bcg.Db).ConfigureAwait(false);
				//await ApplyContentFilters(bcg.Db).ConfigureAwait(false);

				// End
				Console.WriteLine();
				Console.WriteLine("Complete.");
				Console.Read();
			}).ConfigureAwait(false);

		/// <summary>
		/// Select Terms
		/// </summary>
		/// <param name="section"></param>
		/// <param name="db"></param>
		internal static async Task<IR<int>> TermsAsync(IOk r, string section, IWpDb db)
		{
			Console.WriteLine();
			Console.WriteLine($"== {section} Terms ==");

			using var w = db.UnitOfWork;
			return await w.ExecuteScalarAsync<int>(r,
				$"SELECT COUNT(*) FROM {db.Term} WHERE {db.Term.Slug} LIKE @a;",
				new { a = "%a%" }
			);
		}

		internal static void AuditTerms(IR<int> r)
			=> Console.Write(r switch
			{
				IOkV<int> x => $"There are {x.Value} terms",
				{ } e => $"{e.Messages}"
			});

		/// <summary>
		/// Insert an Option
		/// </summary>
		/// <param name="bcg"></param>
		internal static async Task<IR<Bcg.Entities.Option>> InsertOptionAsync(IOk r, IWpDb bcg)
		{
			Console.WriteLine();
			Console.WriteLine("== Option Insert ==");

			var opt = new Bcg.Entities.Option
			{
				Key = Guid.NewGuid().ToString(),
				Value = DateTime.Now.ToLongTimeString()
			};

			using var w = bcg.UnitOfWork;
			return await w.InsertAsync(r.OkV(opt));
		}

		internal static void AuditOption(IR<Bcg.Entities.Option> r)
			=> Console.Write(r switch
			{
				IOkV<Bcg.Entities.Option> x => $"Test option '{x.Value.Key}' = '{x.Value.Value}'.",
				{ } e => $"{e.Messages}"
			});

		/// <summary>
		/// Search sermons
		/// </summary>
		/// <param name="search"></param>
		/// <param name="bcg"></param>
		/// <param name="opt"></param>
		internal static async Task<IR<List<SermonModel>, IWpDb>> SearchSermonsAsync(IOk<bool, IWpDb> r, string search, Action<QueryPosts.Options> opt)
		{
			Console.WriteLine();
			Console.WriteLine($"== Sermons: {search} ==");

			using var q = r.State.GetQueryWrapper();
			return await r.Link().MapAsync(ok => q.QueryPostsAsync<SermonModel>(ok, modify: opt));
		}

		internal static void AuditSermons(IR<List<SermonModel>, IWpDb> r)
		{
			if (r is IError)
			{
				Console.WriteLine($"{r.Messages}");
			}

			if (r is IOkV<List<SermonModel>> ok)
			{
				Console.WriteLine($"There are {ok.Value.Count} matching sermons.");
				foreach (var item in ok.Value)
				{
					Console.WriteLine("{0:0000}: {1}", item.PostId, item.Title);
				}
			}
		}

		/// <summary>
		/// Fetch post meta
		/// </summary>
		/// <param name="db"></param>
		internal static async Task<IR<List<PostModel>, IWpDb>> FetchMeta<TIgnore>(IOk<TIgnore, IWpDb> r)
		{
			Console.WriteLine();
			Console.WriteLine("== Meta ==");

			using var w = r.State.GetQueryWrapper();
			return await r.Link().MapAsync(ok => w.QueryPostsAsync<PostModel>(ok, modify: opt => opt.Limit = 3));
		}

		internal static void AuditMeta(IR<List<PostModel>> r)
		{
			if (r is IError)
			{
				Console.WriteLine($"{r.Messages}");
			}

			if (r is IOkV<List<PostModel>> ok)
			{
				foreach (var post in ok.Value)
				{
					Console.WriteLine("Post {0}", post.PostId);
					foreach (var item in post.Meta)
					{
						Console.WriteLine("{0}: {1}", item.Key, item.Value);
					}
				}
			}
		}

		/// <summary>
		/// Fetch post custom fields
		/// </summary>
		/// <param name="db"></param>
		internal static async Task<IR<List<SermonModelWithCustomFields>, IWpDb>> FetchCustomFields<TIgnore>(IOk<TIgnore, IWpDb> r)
		{
			Console.WriteLine();
			Console.WriteLine("== Custom Fields ==");

			using var q = r.State.GetQueryWrapper();
			return await r.Link().MapAsync(ok => q.QueryPostsAsync<SermonModelWithCustomFields>(ok, modify: opt =>
			{
				opt.Type = WpBcg.PostTypes.Sermon;
				opt.SortRandom = true;
				opt.Limit = 10;
			}));
		}

		internal static void AuditCustomFields(IR<List<SermonModelWithCustomFields>> r)
		{
			if (r is IError)
			{
				Console.WriteLine($"{r.Messages}");
			}

			if (r is IOkV<List<SermonModelWithCustomFields>> ok)
			{
				Console.WriteLine($"{ok.Value.Count} sermons found");

				foreach (var sermon in ok.Value.ToList())
				{
					Console.WriteLine("{0:0000} '{1}'", sermon.PostId, sermon.Title);
					Console.WriteLine("  - Passage: {0}", sermon.Passage);
					Console.WriteLine("  - PDF: {0}", sermon.Pdf);
					Console.WriteLine("  - Audio: {0}", sermon.Audio);
					Console.WriteLine("  - First Preached: {0}", sermon.FirstPreached);
				}
			}
		}

		///// <summary>
		///// Fetch taxonomies
		///// </summary>
		///// <param name="db"></param>
		//internal static async Task FetchTaxonomies(IWpDb db)
		//{
		//	try
		//	{
		//		Console.WriteLine();
		//		Console.WriteLine("== Taxonomies ==");

		//		using var q = db.GetQueryWrapper();

		//		var query = await q.QueryPostsAsync<SermonModelWithTaxonomies>(modify: opt =>
		//		{
		//			opt.Type = WpBcg.PostTypes.Sermon;
		//			opt.SortRandom = true;
		//			opt.Limit = 10;
		//		}).ConfigureAwait(false);

		//		if (query.Err is IErrorList sermonsErr)
		//		{
		//			Console.WriteLine("Error fetching sermons");
		//			Console.WriteLine(sermonsErr);
		//			return;
		//		}

		//		var sermons = query.Val;
		//		Console.WriteLine($"{sermons.Count} sermons found");

		//		foreach (var sermon in sermons)
		//		{
		//			Console.WriteLine("{0:0000} '{1}'", sermon.PostId, sermon.Title);

		//			foreach (var book in sermon.BibleBooks)
		//			{
		//				Console.WriteLine("  - Bible Book: {0}", book);
		//			}

		//			foreach (var series in sermon.Series)
		//			{
		//				Console.WriteLine("  - Series: {0}", series);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine(ex);
		//	}
		//}

		///// <summary>
		///// Apply content filters
		///// </summary>
		///// <param name="db"></param>
		//internal static async Task ApplyContentFilters(IWpDb db)
		//{
		//	Console.WriteLine();
		//	Console.WriteLine("== Content Filters ==");

		//	using var w = db.GetQueryWrapper();

		//	var result = await w.QueryPostsAsync<PostModelWithContent>(modify: opt => opt.Limit = 3, filters: GenerateExcerpt.Create()).ConfigureAwait(false);
		//	if (result.Err is IErrorList postsErr)
		//	{
		//		Console.WriteLine("Error fetching posts with content filter");
		//		Console.WriteLine(postsErr);
		//		return;
		//	}

		//	foreach (var post in result.Val)
		//	{
		//		Console.WriteLine("Post {0}", post.PostId);
		//		Console.WriteLine("  - {0}", post.Content);
		//	}
		//}
	}

	class TermModel
	{
		public string Title { get; set; } = string.Empty;

		public Taxonomy Taxonomy { get; set; } = Taxonomy.Blank;

		public int Count { get; set; }
	}

	class PostModel : IEntity
	{
		public long Id { get => PostId; set => PostId = value; }

		public long PostId { get; set; }

		public MetaDictionary Meta { get; set; } = new MetaDictionary();
	}

	class PostModelWithContent : PostModel
	{
		public string Content { get; set; } = string.Empty;
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
