// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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

namespace AppConsoleWordPress
{
	internal sealed class Program : Jeebs.Apps.Program
	{
		private static ILog log = new Jeebs.Logging.SerilogLogger();

		internal static async Task Main(string[] args) =>
			await Main<App>(
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
					await TermsAsync("BCG", bcg.Db).AuditAsync(AuditTerms);
					await TermsAsync("USA", usa.Db).AuditAsync(AuditTerms);

					await Option
						.Wrap(bcg.Db)
						.BindAsync(InsertOptionAsync)
						.AuditAsync(AuditOption);

					await Option
						.Wrap(bcg.Db)
						.BindAsync(x => SearchSermonsAsync(x, "holiness", opt =>
						{
							opt.SearchText = "holiness";
							opt.SearchOperator = SearchOperators.Like;
							opt.Type = WpBcg.PostTypes.Sermon;
							opt.Sort = new[] { (bcg.Db.Post.Title, SortOrder.Ascending) };
							opt.Limit = 4;
						}))
						.AuditAsync(AuditSermons);

					await Option
						.Wrap(bcg.Db)
						.BindAsync(x => SearchSermonsAsync(x, "jesus", opt =>
						{
							opt.Type = WpBcg.PostTypes.Sermon;
							opt.SearchText = "jesus";
							opt.SearchFields = SearchPostFields.Title;
							opt.Taxonomies = new[] { (WpBcg.Taxonomies.BibleBook, 424L) };
							opt.Limit = 5;
						}))
						.AuditAsync(AuditSermons);

					await Option
						.Wrap(bcg.Db)
						.BindAsync(FetchMeta)
						.AuditAsync(AuditMeta)
						.BindAsync(
							_ => FetchCustomFields(bcg.Db)
						)
						.AuditAsync(AuditCustomFields);

					Option.True
						.Map<int>(_ => throw new Exception("Test"));

					await Option
						.Wrap(bcg.Db)
						.BindAsync(FetchTaxonomies)
						.AuditAsync(AuditTaxonomies);

					await Option
						.Wrap(bcg.Db)
						.BindAsync(ApplyContentFilters)
						.AuditAsync(AuditApplyContentFilters);

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

		internal static async void AuditTaxonomies(Option<List<SermonModelWithTaxonomies>> opt) =>
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

		internal static async void AuditApplyContentFilters(Option<List<PostModelWithContent>> opt) =>
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
