using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppConsoleWordPress.Bcg;
using AppConsoleWordPress.Usa;
using Jeebs;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.WordPress;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Enums;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Crypto.Digests;

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
					.UseLog(log)
					.Link().MapAsync(r => InsertOptionAsync(r, bcg.Db)).Await()
					.Audit(AuditOption);

				Chain.Create(bcg.Db)
					.UseLog(log)
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
					.UseLog(log)
					.Link().MapAsync(r => SearchSermonsAsync(r, "jesus", opt =>
					{
						opt.Type = WpBcg.PostTypes.Sermon;
						opt.SearchText = "jesus";
						opt.SearchFields = SearchPostFields.Title;
						opt.Taxonomies = new[] { (WpBcg.Taxonomies.BibleBook, 424L) };
						opt.Limit = 5;
					})).Await()
					.Audit(AuditSermons);

				Chain.Create(bcg.Db)
					.UseLog(log)
					.Link().MapAsync(FetchMeta).Await()
					.Audit(AuditMeta)
					.Link().MapAsync(FetchCustomFields).Await()
					.Audit(AuditCustomFields);

				Chain.Create()
					.UseLog(log)
					.Link().Map<int>(_ => throw new Exception("Test"));

				Chain.Create(bcg.Db)
					.UseLog(log)
					.Link().MapAsync(FetchTaxonomies).Await()
					.Audit(AuditTaxonomies);

				Chain.Create(bcg.Db)
					.UseLog(log)
					.Link().MapAsync(ApplyContentFilters).Await()
					.Audit(AuditApplyContentFilters);

				// End
				Console.WriteLine();
				Console.WriteLine("Complete.");
				Console.Read();
			}).ConfigureAwait(false);

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
			return await w.CreateAsync(r.OkV(opt));
		}

		internal static void AuditOption(IR<Bcg.Entities.Option> r)
			=> Console.Write(r switch
			{
				IOkV<Bcg.Entities.Option> x => $"Test option '{x.Value.Key}' = '{x.Value.Value}'.",
				{ } e => $"{e.Messages}"
			});

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

		internal static async Task<IR<List<SermonModelWithTaxonomies>, IWpDb>> FetchTaxonomies<TIgnore>(IOk<TIgnore, IWpDb> r)
		{
			Console.WriteLine();
			Console.WriteLine("== Taxonomies ==");

			using var w = r.State.GetQueryWrapper();
			return await r.Link().MapAsync(ok => w.QueryPostsAsync<SermonModelWithTaxonomies>(ok, modify: opt =>
			{
				opt.Type = WpBcg.PostTypes.Sermon;
				opt.SortRandom = true;
				opt.Limit = 10;
			}));
		}

		internal static async void AuditTaxonomies(IR<List<SermonModelWithTaxonomies>> r)
		{
			if (r is IError)
			{
				r.Logger.Messages(r.Messages);
			}

			if (r is IOkV<List<SermonModelWithTaxonomies>> ok)
			{
				Console.WriteLine($"{ok.Value.Count} sermons found");

				foreach (var sermon in ok.Value)
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
		}

		internal static async Task<IR<List<PostModelWithContent>, IWpDb>> ApplyContentFilters<TIgnore>(IOk<TIgnore, IWpDb> r)
		{
			Console.WriteLine();
			Console.WriteLine("== Apply Content Filters ==");

			using var w = r.State.GetQueryWrapper();
			return await r.Link().MapAsync(ok => w.QueryPostsAsync<PostModelWithContent>(
				ok,
				modify: opt => opt.Limit = 3,
				filters: GenerateExcerpt.Create()
			));
		}

		internal static async void AuditApplyContentFilters(IR<List<PostModelWithContent>> r)
		{
			if (r is IError)
			{
				r.Logger.Messages(r.Messages);
			}

			if (r is IOkV<List<PostModelWithContent>> ok)
			{
				Console.WriteLine($"{ok.Value.Count} posts found");

				foreach (var post in ok.Value)
				{
					Console.WriteLine("{0:0000} {1}", post.PostId, post.Content);
				}
			}
		}
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
