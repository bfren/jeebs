// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppConsoleWordPress.Bcg;
using AppConsoleWordPress.Usa;
using Jeebs;
using Jeebs.WordPress;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Mapping;
using Jeebs.WordPress.Enums;
using Microsoft.Extensions.DependencyInjection;
using static F.OptionF;

namespace AppConsoleWordPress
{
	internal sealed class Program : Jeebs.Apps.Program
	{
		private static ILog log = new Jeebs.Logging.SerilogLogger();

		internal static async Task Main(string[] args) =>
			await MainAsync<App>(
				args,
				async (provider, _) =>
				{
					// Begin
					log.Debug("= WordPress Console Test =");

					// Get services
					log = provider.GetRequiredService<ILog<Program>>();
					var bcg = provider.GetRequiredService<WpBcg>();
					var usa = provider.GetRequiredService<WpUsa>();

					// Run test methods
					await TermsAsync((string)"BCG", (IWpDb)bcg.Db).AuditAsync((Action<Option<int>>)AuditTerms);
					await TermsAsync((string)"USA", (IWpDb)usa.Db).AuditAsync((Action<Option<int>>)AuditTerms);

					await Return((IWpDb)bcg.Db)
						.BindAsync((Func<IWpDb, Task<Option<Bcg.Entities.Option>>>)InsertOptionAsync)
						.AuditAsync((Action<Option<Bcg.Entities.Option>>)AuditOption);

					await Return((IWpDb)bcg.Db)
						.BindAsync((Func<IWpDb, Task<Option<PagedList<SermonModel>>>>)(x => (Task<Option<PagedList<SermonModel>>>)GetPagedSermonsAsync((IWpDb)x, (string)"holiness", (Action<QueryPosts.Options>)(opt =>
						{
							opt.SearchText = "holiness";
							opt.SearchOperator = SearchOperator.Like;
							opt.Type = WpBcg.PostTypes.Sermon;
							opt.Sort = new[] { (Title:(string)bcg.Db.Post.Title, Ascending:(SortOrder)SortOrder.Ascending) };
							opt.Limit = 4;
						}))))
						.AuditAsync((Action<Option<PagedList<SermonModel>>>)AuditPagedSermons);

					await Return((IWpDb)bcg.Db)
						.BindAsync((Func<IWpDb, Task<Option<List<SermonModel>>>>)(x => (Task<Option<List<SermonModel>>>)SearchSermonsAsync((IWpDb)x, (string)"holiness", (Action<QueryPosts.Options>)(opt =>
						{
							opt.SearchText = "holiness";
							opt.SearchOperator = SearchOperator.Like;
							opt.Type = WpBcg.PostTypes.Sermon;
							opt.Sort = new[] { (Title:(string)bcg.Db.Post.Title, Ascending:(SortOrder)SortOrder.Ascending) };
							opt.Limit = 4;
						}))))
						.AuditAsync((Action<Option<List<SermonModel>>>)AuditSermons);

					await Return((IWpDb)bcg.Db)
						.BindAsync((Func<IWpDb, Task<Option<List<SermonModel>>>>)(x => (Task<Option<List<SermonModel>>>)SearchSermonsAsync((IWpDb)x, (string)"jesus", (Action<QueryPosts.Options>)(opt =>
						{
							opt.Type = WpBcg.PostTypes.Sermon;
							opt.SearchText = "jesus";
							opt.SearchFields = SearchPostFields.Title;
							opt.Taxonomies = new[] { (BibleBook:(Taxonomy)WpBcg.Taxonomies.BibleBook, (long)424L) };
							opt.Limit = 5;
						}))))
						.AuditAsync((Action<Option<List<SermonModel>>>)AuditSermons);

					await Return(bcg.Db)
						.BindAsync(FetchMeta)
						.AuditAsync(AuditMeta)
						.BindAsync(
							_ => FetchCustomFields(bcg.Db)
						)
						.AuditAsync(AuditCustomFields);

					Return<int>(
						() => throw new Exception("Test"),
						DefaultHandler
					);

					await Return((IWpDb)bcg.Db)
						.BindAsync((Func<IWpDb, Task<Option<List<SermonModelWithTaxonomies>>>>)FetchTaxonomies)
						.AuditAsync((Action<Option<List<SermonModelWithTaxonomies>>>)AuditTaxonomies);

					await Return((IWpDb)bcg.Db)
						.BindAsync((Func<IWpDb, Task<Option<List<PostModelWithContent>>>>)ApplyContentFilters)
						.AuditAsync((Action<Option<List<PostModelWithContent>>>)AuditApplyContentFilters);

					// End
					Console.WriteLine();
					log.Debug("Complete.");
					Console.Read();
				}
			).ConfigureAwait(false);

		internal static async Task<Option<int>> TermsAsync(string section, IWpDb db)
		{
			Console.WriteLine();
			log.Debug($"== {section} Terms ==");

			using var w = db.UnitOfWork;
			return await w.ExecuteScalarAsync<int>(
				$"SELECT COUNT(*) FROM {db.Term} WHERE {db.Term.Slug} LIKE @a;",
				new { a = "%a%" }
			);
		}

		internal static void AuditTerms(Option<int> opt) =>
			opt.Switch(
				some: x => log.Debug("There are {0} terms", x),
				none: r => log.Error($"No terms found: {r}")
			);

		internal static async Task<Option<Bcg.Entities.Option>> InsertOptionAsync(IWpDb db)
		{
			Console.WriteLine();
			log.Debug("== Option Insert ==");

			var opt = new Bcg.Entities.Option
			{
				Key = Guid.NewGuid().ToString(),
				Value = DateTime.Now.ToLongTimeString()
			};

			using var w = db.UnitOfWork;
			return await w.CreateAsync(opt);
		}

		internal static void AuditOption(Option<Bcg.Entities.Option> opt) =>
			opt.Switch(
				some: x => log.Debug("Test option '{Key}' = '{Value}'", x.Key, x.Value),
				none: r => log.Error($"No option found: {r}")
			);

		internal static async Task<Option<PagedList<SermonModel>>> GetPagedSermonsAsync(IWpDb db, string search, Action<QueryPosts.Options> opt)
		{
			Console.WriteLine();
			log.Debug($"== Sermons: {search} ==");

			using var q = db.GetQueryWrapper();
			return await q.QueryPostsAsync<SermonModel>(1, modify: opt);
		}

		internal static void AuditPagedSermons(Option<PagedList<SermonModel>> opt) =>
			opt.Switch(
				some: x =>
				{
					log.Debug("There are {Count} matching sermons", x.Count);
					foreach (var item in x)
					{
						log.Debug("{PostId:0000}: {Title}", item.PostId, item.Title);
					}
				},
				none: r => log.Error($"No sermons found: {r}")
			);

		internal static async Task<Option<List<SermonModel>>> SearchSermonsAsync(IWpDb db, string search, Action<QueryPosts.Options> opt)
		{
			Console.WriteLine();
			log.Debug($"== Sermons: {search} ==");

			using var q = db.GetQueryWrapper();
			return await q.QueryPostsAsync<SermonModel>(modify: opt);
		}

		internal static void AuditSermons(Option<List<SermonModel>> opt) =>
			opt.Switch(
				some: x =>
				{
					log.Debug("There are {Count} matching sermons", x.Count);
					foreach (var item in x)
					{
						log.Debug("{PostId:0000}: {Title}", item.PostId, item.Title);
					}
				},
				none: r => log.Error($"No sermons found: {r}")
			);

		internal static async Task<Option<List<PostModel>>> FetchMeta(IWpDb db)
		{
			Console.WriteLine();
			log.Debug("== Meta ==");

			using var w = db.GetQueryWrapper();
			return await w.QueryPostsAsync<PostModel>(modify: opt => opt.Limit = 3);
		}

		internal static void AuditMeta(Option<List<PostModel>> opt) =>
			opt.Switch(
				some: x =>
				{
					foreach (var post in x)
					{
						log.Debug("Post {PostId}", post.PostId);
						foreach (var item in post.Meta)
						{
							log.Debug(" - {Key}: {Value}", item.Key, item.Value);
						}
					}
				},
				none: r => log.Error($"No posts found: {r}")
			);

		internal static async Task<Option<List<SermonModelWithCustomFields>>> FetchCustomFields(IWpDb db)
		{
			Console.WriteLine();
			log.Debug("== Custom Fields ==");

			using var q = db.GetQueryWrapper();
			return await q.QueryPostsAsync<SermonModelWithCustomFields>(modify: opt =>
			{
				opt.Type = WpBcg.PostTypes.Sermon;
				//opt.SortRandom = true;
				//opt.Limit = 10;
				opt.Ids = new[] { 924L, 2336L };
			});
		}

		internal static void AuditCustomFields(Option<List<SermonModelWithCustomFields>> opt) =>
			opt.Switch(
				some: x =>
				{
					log.Debug("{Count} sermons found", x.Count);

					foreach (var sermon in x)
					{
						log.Debug("{SermonId:0000} '{Title}'", sermon.PostId, sermon.Title);
						log.Debug("  - Passage: {Passage}", sermon.Passage);
						log.Debug("  - PDF: {Pdf}", sermon.Pdf?.ToString() ?? "null");
						log.Debug("  - Audio: {Audio}", sermon.Audio?.ToString() ?? "null");
						log.Debug("  - First Preached: {FirstPreached}", sermon.FirstPreached);
						log.Debug("  - Feed Image: {FeedImageUrl}", sermon.Image?.ValueObj.UrlPath ?? "null");
					}
				},
				none: r => log.Error($"No sermons found: {r}")
			);

		internal static async Task<Option<List<SermonModelWithTaxonomies>>> FetchTaxonomies(IWpDb db)
		{
			Console.WriteLine();
			log.Debug("== Taxonomies ==");

			using var w = db.GetQueryWrapper();
			return await w.QueryPostsAsync<SermonModelWithTaxonomies>(modify: opt =>
			{
				opt.Type = WpBcg.PostTypes.Sermon;
				opt.SortRandom = true;
				opt.Limit = 10;
			});
		}

		internal static void AuditTaxonomies(Option<List<SermonModelWithTaxonomies>> opt) =>
			opt.Switch(
				some: x =>
				{
					log.Debug("{Count} sermons found", x.Count);

					foreach (var sermon in x)
					{
						log.Debug("{PostId:0000} '{Title}'", sermon.PostId, sermon.Title);

						foreach (var book in sermon.BibleBooks)
						{
							log.Debug("  - Bible Book: {Book}", book);
						}

						foreach (var series in sermon.Series)
						{
							log.Debug("  - Series: {Series}", series);
						}
					}
				},
				none: r => log.Error($"No sermons found: {r}")
			);

		internal static async Task<Option<List<PostModelWithContent>>> ApplyContentFilters(IWpDb db)
		{
			Console.WriteLine();
			log.Debug("== Apply Content Filters ==");

			using var w = db.GetQueryWrapper();
			return await w.QueryPostsAsync<PostModelWithContent>(
				modify: opt => opt.Limit = 3,
				filters: GenerateExcerpt.Create()
			);
		}

		internal static void AuditApplyContentFilters(Option<List<PostModelWithContent>> opt) =>
			opt.Switch(
				some: x =>
				{
					log.Debug("{Count} posts found", x.Count);

					foreach (var post in x)
					{
						log.Debug("{PostId:0000} {Content}", post.PostId, post.Content);
					}
				},
				none: r => log.Error($"No posts found: {r}")
			);
	}

	internal class TermModel
	{
		public string Title { get; set; } = string.Empty;

		public Taxonomy Taxonomy { get; set; } = Taxonomy.Blank;

		public int Count { get; set; }
	}

	internal class PostModel : IEntity
	{
		public long Id { get => PostId; set => PostId = value; }

		public long PostId { get; set; }

		public MetaDictionary Meta { get; set; } = new MetaDictionary();
	}

	internal class PostModelWithContent : PostModel
	{
		public string Content { get; set; } = string.Empty;
	}

	internal class SermonModel : IEntity
	{
		public long Id { get => PostId; set => PostId = value; }

		public long PostId { get; set; }

		public string Title { get; set; } = string.Empty;

		public DateTime PublishedOn { get; set; }
	}

	internal class SermonModelWithCustomFields : SermonModel
	{
		public MetaDictionary Meta { get; set; } = new MetaDictionary();

		public PassageCustomField Passage { get; set; } = new PassageCustomField();

		public PdfCustomField? Pdf { get; set; }

		public AudioRecordingCustomField? Audio { get; set; }

		public FirstPreachedCustomField FirstPreached { get; set; } = new FirstPreachedCustomField();

		public FeedImageCustomField? Image { get; set; }
	}

	internal class SermonModelWithTaxonomies : SermonModel
	{
		public TermList BibleBooks { get; set; } = new TermList(WpBcg.Taxonomies.BibleBook);

		public TermList Series { get; set; } = new TermList(WpBcg.Taxonomies.Series);
	}
}
