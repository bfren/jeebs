// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using AppConsoleWp;
using AppConsoleWp.Bcg;
using AppConsoleWp.Usa;
using Jeebs;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.ContentFilters;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Microsoft.Extensions.DependencyInjection;

await Jeebs.Apps.Program.MainAsync<App>(args, async (provider, log) =>
{
	// Begin
	log.Debug("= WordPress Console Test =");

	// Get services
	var bcg = provider.GetRequiredService<WpBcg>();
	var usa = provider.GetRequiredService<WpUsa>();

	//
	// Get random posts
	//

	Console.WriteLine();
	log.Debug("== Three Random Posts ==");
	await bcg.Db.QueryPostsAsync<PostModel>(opt => opt with
	{
		SortRandom = true,
		Maximum = 3
	})
	.AuditAsync(
		some: x =>
		{
			if (!x.Any())
			{
				log.Error("No posts found.");
			}

			foreach (var item in x)
			{
				log.Debug("Post {Id:0000}: {Title}", item.PostId, item.Title);
			}
		},
		none: r => log.Message(r)
	);

	//
	// Search for sermons with a string term
	//

	const string term = "holiness";
	Console.WriteLine();
	log.Debug("== Search for Sermons with '{Term}' ==", term);
	await bcg.Db.QueryPostsAsync<SermonModel>(2, opt => opt with
	{
		Type = WpBcg.PostTypes.Sermon,
		SearchText = term,
		SearchComparison = Jeebs.Data.Enums.Compare.Like,
		SortRandom = true
	})
	.AuditAsync(
		some: x =>
		{
			if (!x.Any())
			{
				log.Error("No sermons found.");
			}

			foreach (var item in x)
			{
				log.Debug("Sermon {Id:0000}", item.PostId);
				log.Debug("  - Title: {Title}", item.Title);
				log.Debug("  - Published: {Published:dd/MM/yyyy}", item.PublishedOn);
			}
		},
		none: r => log.Message(r)
	);

	//
	// Get sermons with taxonomies
	//

	Console.WriteLine();
	log.Debug("== Get Sermons with Taxonomy properties ==");
	await bcg.Db.QueryPostsAsync<SermonModelWithTaxonomies>(opt => opt with
	{
		Type = WpBcg.PostTypes.Sermon,
		SortRandom = true,
		Maximum = 5
	})
	.AuditAsync(
		some: x =>
		{
			if (!x.Any())
			{
				log.Error("No sermons found.");
			}

			foreach (var item in x)
			{
				log.Debug("Sermon {Id:0000}: {Title}", item.PostId, item.Title);
				log.Debug("  - Bible Books: {Books}", string.Join(", ", item.BibleBooks.Select(b => b.Title)));
				log.Debug("  - Series: {Series}", string.Join(", ", item.Series.Select(b => b.Title)));
			}
		},
		none: r => log.Message(r)
	);

	//
	// Search for sermons with a taxonomy
	//

	var taxonomy = WpBcg.Taxonomies.BibleBook;
	const long book0 = 423L;
	const long book1 = 628L;
	Console.WriteLine();
	log.Debug("== Search for Sermons with Bible Books {Book0} and {Book1} ==", book0, book1);
	await bcg.Db.QueryPostsAsync<SermonModelWithTaxonomies>(opt => opt with
	{
		Type = WpBcg.PostTypes.Sermon,
		Taxonomies = new[] { (taxonomy, book0), (taxonomy, book1) }.ToImmutableList(),
		Maximum = 5
	})
	.AuditAsync(
		some: x =>
		{
			if (!x.Any())
			{
				log.Error("No sermons found.");
			}

			if (x.Count() > 1)
			{
				log.Error("Too many sermons found.");
				return;
			}

			foreach (var item in x)
			{
				log.Debug("Sermon {Id:0000}: {Title}", item.PostId, item.Title);
				log.Debug("  - Bible Books: {Books}", string.Join(", ", item.BibleBooks.Select(b => b.Title)));
			}
		},
		none: r => log.Message(r)
	);

	//
	// Get taxonomies
	//

	Console.WriteLine();
	const long countAtLeast = 3;
	log.Debug("== Get Category taxonomy with at least {CountAtLeast} posts ==", countAtLeast);
	await usa.Db.QueryTermsAsync<TaxonomyModel>(opt => opt with
	{
		Taxonomy = Taxonomy.PostCategory,
		CountAtLeast = countAtLeast
	})
	.AuditAsync(
		some: x =>
		{
			if (!x.Any())
			{
				log.Error("No terms found.");
			}

			foreach (var item in x)
			{
				log.Debug("Term {Id:00}: {Title} ({Count})", item.TermId, item.Title, item.Count);
			}
		},
		none: r => log.Message(r)
	);

	//
	// Get sermons with custom fields
	//

	Console.WriteLine();
	log.Debug("== Get Sermons with Custom Fields ==");
	await bcg.Db.QueryPostsAsync<SermonModelWithCustomFields>(opt => opt with
	{
		Type = WpBcg.PostTypes.Sermon,
		Ids = ImmutableList.Create<WpPostId>(new(924L), new(1867L), new(2020L))
	})
	.AuditAsync(
		some: x =>
		{
			if (!x.Any())
			{
				log.Error("No sermons found.");
			}

			foreach (var item in x)
			{
				log.Debug("Sermon {Id:0000}: {Title}", item.PostId, item.Title);
				log.Debug("  - Passage: {Passage}", item.Passage.ValueObj);
				log.Debug("  - PDF: {Pdf}", item.Pdf?.ValueObj.UrlPath ?? "none");
				log.Debug("  - Audio: {Audio}", item.Audio?.ValueObj.UrlPath ?? "none");
				log.Debug("  - First Preached: {First}", item.FirstPreached.ValueObj.Title);
				log.Debug("  - Image: {Image}", item.Image?.ValueObj.UrlPath ?? "none");
			}
		},
		none: r => log.Message(r)
	);

	//
	// Search for sermons with custom fields
	//

	Console.WriteLine();
	ICustomField field = WpBcg.CustomFields.FirstPreached;
	object first = 422L;
	log.Debug("== Get Sermons where First Preached is {First} ==", first);
	await bcg.Db.QueryPostsAsync<SermonModelWithCustomFields>(opt => opt with
	{
		Type = WpBcg.PostTypes.Sermon,
		CustomFields = ImmutableList.Create(new[] { (field, Compare.Equal, first) })
	})
	.AuditAsync(
		some: x =>
		{
			if (!x.Any())
			{
				log.Error("No sermons found.");
			}

			if (x.Count() > 2)
			{
				log.Error("Too many sermons found.");
				return;
			}

			foreach (var item in x)
			{
				var obj = item.FirstPreached.ValueObj;
				log.Debug("Sermon {Id:0000}: {Title}", item.PostId, item.Title);
				log.Debug("  - {FirstId:0000}: {FirstTitle}", obj.TermId, obj.Title);
			}
		},
		none: r => log.Message(r)
	);

	//
	// Generate Excerpts
	//

	Console.WriteLine();
	log.Debug("== Get Posts with generated excerpt ==");
	await bcg.Db.QueryPostsAsync<PostModelWithContent>(opt => opt with
	{
		SortRandom=true
	}, GenerateExcerpt.Create())
	.AuditAsync(
		some: x =>
		{
			if (!x.Any())
			{
				log.Error("No posts found.");
			}

			foreach (var item in x)
			{
				log.Debug("Post {Id:0000}: {@Content}", item.PostId, item.Content);
			}
		},
		none: r => log.Message(r)
	);

	// End
	Console.WriteLine();
	log.Debug("Complete.");
});
