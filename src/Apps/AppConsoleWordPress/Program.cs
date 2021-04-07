// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppConsoleWordPress;
using AppConsoleWordPress.Bcg;
using AppConsoleWordPress.Usa;
using Jeebs;
using Jeebs.WordPress;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Mapping;
using Jeebs.WordPress.Enums;
using Microsoft.Extensions.DependencyInjection;
using static F.OptionF;
using Bcg = AppConsoleWordPress.Bcg.Entities;

await Jeebs.Apps.Program.MainAsync<App>(args, async (provider, log) =>
{
	// Begin
	log.Debug("= WordPress Console Test =");

	// Get services
	var bcg = provider.GetRequiredService<WpBcg>();
	var usa = provider.GetRequiredService<WpUsa>();

	// Run test methods
	await TermsAsync("BCG", bcg.Db).AuditAsync((Action<Option<int>>)AuditTerms);
	await TermsAsync("USA", usa.Db).AuditAsync((Action<Option<int>>)AuditTerms);

	await Return(bcg.Db)
		.BindAsync(InsertOptionAsync)
		.AuditAsync(any: AuditOption);

	await Return(bcg.Db)
		.BindAsync(x => GetPagedSermonsAsync(x, "holiness", opt =>
		{
			opt.SearchText = "holiness";
			opt.SearchOperator = SearchOperator.Like;
			opt.Type = WpBcg.PostTypes.Sermon;
			opt.Sort = new[] { (bcg.Db.Post.Title, SortOrder.Ascending) };
			opt.Limit = 4;
		}))
		.AuditAsync(any: AuditPagedSermons);

	await Return(bcg.Db)
		.BindAsync(x => SearchSermonsAsync(x, "holiness", opt =>
		{
			opt.SearchText = "holiness";
			opt.SearchOperator = SearchOperator.Like;
			opt.Type = WpBcg.PostTypes.Sermon;
			opt.Sort = new[] { (bcg.Db.Post.Title, SortOrder.Ascending) };
			opt.Limit = 4;
		}))
		.AuditAsync(any: AuditSermons);

	await Return(bcg.Db)
		.BindAsync(x => SearchSermonsAsync(x, "jesus", opt =>
		{
			opt.Type = WpBcg.PostTypes.Sermon;
			opt.SearchText = "jesus";
			opt.SearchFields = SearchPostFields.Title;
			opt.Taxonomies = new[] { (WpBcg.Taxonomies.BibleBook, 424L) };
			opt.Limit = 5;
		}))
		.AuditAsync(any: AuditSermons);

	await Return(bcg.Db)
		.BindAsync(FetchMeta)
		.AuditAsync(any: AuditMeta)
		.BindAsync(
			_ => FetchCustomFields(bcg.Db)
		)
		.AuditAsync(any: AuditCustomFields);

	Return<int>(
		() => throw new Exception("Test"),
		DefaultHandler
	);

	await Return(bcg.Db)
		.BindAsync(FetchTaxonomies)
		.AuditAsync(any: AuditTaxonomies);

	await Return(bcg.Db)
		.BindAsync(ApplyContentFilters)
		.AuditAsync(any: AuditApplyContentFilters);

	// End
	Console.WriteLine();
	log.Debug("Complete.");
	Console.Read();

	//
	//	FUNCTIONS
	//

	async Task<Option<int>> TermsAsync(string section, IWpDb db)
	{
		Console.WriteLine();
		log.Debug($"== {section} Terms ==");

		using var w = db.UnitOfWork;
		return await w.ExecuteScalarAsync<int>(
			$"SELECT COUNT(*) FROM {db.Term} WHERE {db.Term.Slug} LIKE @a;",
			new { a = "%a%" }
		);
	}

	void AuditTerms(Option<int> opt) =>
		opt.Switch(
			some: x => log.Debug("There are {0} terms", x),
			none: r => log.Error($"No terms found: {r}")
		);

	async Task<Option<Bcg.Option>> InsertOptionAsync(IWpDb db)
	{
		Console.WriteLine();
		log.Debug("== Option Insert ==");

		var opt = new Bcg.Option
		{
			Key = Guid.NewGuid().ToString(),
			Value = DateTime.Now.ToLongTimeString()
		};

		using var w = db.UnitOfWork;
		return await w.CreateAsync(opt);
	}

	void AuditOption(Option<Bcg.Option> opt) =>
		opt.Switch(
			some: x => log.Debug("Test option '{Key}' = '{Value}'", x.Key, x.Value),
			none: r => log.Error($"No option found: {r}")
		);

	async Task<Option<PagedList<SermonModel>>> GetPagedSermonsAsync(IWpDb db, string search, Action<QueryPosts.Options> opt)
	{
		Console.WriteLine();
		log.Debug($"== Sermons: {search} ==");

		using var q = db.GetQueryWrapper();
		return await q.QueryPostsAsync<SermonModel>(1, modify: opt);
	}

	void AuditPagedSermons(Option<PagedList<SermonModel>> opt) =>
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

	async Task<Option<List<SermonModel>>> SearchSermonsAsync(IWpDb db, string search, Action<QueryPosts.Options> opt)
	{
		Console.WriteLine();
		log.Debug($"== Sermons: {search} ==");

		using var q = db.GetQueryWrapper();
		return await q.QueryPostsAsync<SermonModel>(modify: opt);
	}

	void AuditSermons(Option<List<SermonModel>> opt) =>
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

	async Task<Option<List<PostModel>>> FetchMeta(IWpDb db)
	{
		Console.WriteLine();
		log.Debug("== Meta ==");

		using var w = db.GetQueryWrapper();
		return await w.QueryPostsAsync<PostModel>(modify: opt => opt.Limit = 3);
	}

	void AuditMeta(Option<List<PostModel>> opt) =>
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

	async Task<Option<List<SermonModelWithCustomFields>>> FetchCustomFields(IWpDb db)
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

	void AuditCustomFields(Option<List<SermonModelWithCustomFields>> opt) =>
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

	async Task<Option<List<SermonModelWithTaxonomies>>> FetchTaxonomies(IWpDb db)
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

	void AuditTaxonomies(Option<List<SermonModelWithTaxonomies>> opt) =>
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

	async Task<Option<List<PostModelWithContent>>> ApplyContentFilters(IWpDb db)
	{
		Console.WriteLine();
		log.Debug("== Apply Content Filters ==");

		using var w = db.GetQueryWrapper();
		return await w.QueryPostsAsync<PostModelWithContent>(
			modify: opt => opt.Limit = 3,
			filters: GenerateExcerpt.Create()
		);
	}

	void AuditApplyContentFilters(Option<List<PostModelWithContent>> opt) =>
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
}).ConfigureAwait(false);
