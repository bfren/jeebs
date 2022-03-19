// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppConsoleWp;
using AppConsoleWp.Bcg;
using AppConsoleWp.Usa;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Logging;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.CustomFields;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using MaybeF;
using Microsoft.Extensions.DependencyInjection;

var builder = Jeebs.Apps.Host.CreateBuilder<App>(args);
var app = builder.Build();

var log = app.Services.GetRequiredService<ILog<App>>();

// Begin
log.Dbg("= WordPress Console Test =");

// Get services
var bcg = app.Services.GetRequiredService<WpBcg>();
var usa = app.Services.GetRequiredService<WpUsa>();

//
// Get random posts
//

Console.WriteLine();
log.Dbg("== Three Random Posts ==");
_ = await bcg.Db.Query.PostsAsync<PostModel>(opt => opt with
{
	SortRandom = true,
	Maximum = 3
})
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No posts found.");
		}

		foreach (var item in x)
		{
			log.Dbg("Post {Id:0000}: {Title}", item.Id.Value, item.Title);
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Search for sermons with a string term
//

const string term = "holiness";
Console.WriteLine();
log.Dbg("== Search for Sermons with '{Term}' ==", term);
_ = await bcg.Db.Query.PostsAsync<SermonModel>(2, opt => opt with
{
	Type = WpBcg.PostTypes.Sermon,
	SearchText = term,
	SearchComparison = Compare.Like,
	SortRandom = true
})
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No sermons found.");
		}

		foreach (var item in x)
		{
			log.Dbg("Sermon {Id:0000}", item.Id.Value);
			log.Dbg("  - Title: {Title}", item.Title);
			log.Dbg("  - Published: {Published:dd/MM/yyyy}", item.PublishedOn);
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Get sermons with taxonomies
//

Console.WriteLine();
log.Dbg("== Get Sermons with Taxonomy properties ==");
_ = await bcg.Db.Query.PostsAsync<SermonModelWithTaxonomies>(opt => opt with
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
			log.Err("No sermons found.");
		}

		foreach (var item in x)
		{
			log.Dbg("Sermon {Id:0000}: {Title}", item.Id.Value, item.Title);
			log.Dbg("  - Bible Books: {Books}", string.Join(", ", item.BibleBooks.Select(b => b.Title)));
			log.Dbg("  - Series: {Series}", string.Join(", ", item.Series.Select(b => b.Title)));
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Search for sermons with a taxonomy
//

var taxonomy = WpBcg.Taxonomies.BibleBook;
var book0 = new WpTermId(423U);
var book1 = new WpTermId(628U);
Console.WriteLine();
log.Dbg("== Search for Sermons with Bible Books {Book0} and {Book1} ==", book0.Value, book1.Value);
_ = await bcg.Db.Query.PostsAsync<SermonModelWithTaxonomies>(opt => opt with
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
			log.Err("No sermons found.");
		}

		if (x.Count() > 1)
		{
			log.Err("Too many sermons found.");
			return;
		}

		foreach (var item in x)
		{
			log.Dbg("Sermon {Id:0000}: {Title}", item.Id.Value, item.Title);
			log.Dbg("  - Bible Books: {Books}", string.Join(", ", item.BibleBooks.Select(b => b.Title)));
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Get taxonomies
//

Console.WriteLine();
const long countAtLeast = 3;
log.Dbg("== Get Category taxonomy with at least {CountAtLeast} posts ==", countAtLeast);
_ = await usa.Db.Query.TermsAsync<TaxonomyModel>(opt => opt with
{
	Taxonomy = Taxonomy.PostCategory,
	CountAtLeast = countAtLeast
})
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No terms found.");
		}

		foreach (var item in x)
		{
			log.Dbg("Term {Id:00}: {Title} ({Count})", item.Id.Value, item.Title, item.Count);
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Get posts with custom fields
//

Console.WriteLine();
log.Dbg("== Get Posts with Custom Fields ==");
_ = await usa.Db.Query.PostsAsync<PostModelWithCustomFields>(opt => opt)
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No posts found.");
		}

		foreach (var item in x)
		{
			log.Dbg("Post {Id:0000}: {Title}", item.Id.Value, item.Title);
			log.Dbg("  - Image: {Image}", item.FeaturedImage.ValueObj);
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Get sermons with custom fields
//

Console.WriteLine();
log.Dbg("== Get Sermons with Custom Fields ==");
_ = await bcg.Db.Query.PostsAsync<SermonModelWithCustomFields>(opt => opt with
{
	Type = WpBcg.PostTypes.Sermon,
	Ids = ImmutableList.Create<WpPostId>(new(924L), new(1867L), new(2020L))
})
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No sermons found.");
		}

		foreach (var item in x)
		{
			log.Dbg("Sermon {Id:0000}: {Title}", item.Id.Value, item.Title);
			log.Dbg("  - Passage: {Passage}", item.Passage.ValueObj);
			log.Dbg("  - PDF: {Pdf}", item.Pdf?.ValueObj.UrlPath ?? "none");
			log.Dbg("  - Audio: {Audio}", item.Audio?.ValueObj.UrlPath ?? "none");
			log.Dbg("  - First Preached: {First}", item.FirstPreached.ValueObj.Title);
			log.Dbg("  - Image: {Image}", item.Image?.ValueObj.UrlPath ?? "none");
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Search for sermons with custom fields
//

Console.WriteLine();
ICustomField field = WpBcg.CustomFields.FirstPreached;
object first = 422L;
log.Dbg("== Get Sermons where First Preached is {First} ==", first);
_ = await bcg.Db.Query.PostsAsync<SermonModelWithCustomFields>(opt => opt with
{
	Type = WpBcg.PostTypes.Sermon,
	CustomFields = ImmutableList.Create(new[] { (field, Compare.Equal, first) })
})
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No sermons found.");
		}

		if (x.Count() > 2)
		{
			log.Err("Too many sermons found.");
			return;
		}

		foreach (var item in x)
		{
			var obj = item.FirstPreached.ValueObj;
			log.Dbg("Sermon {Id:0000}: {Title}", item.Id.Value, item.Title);
			log.Dbg("  - {FirstId:0000}: {FirstTitle}", obj.Id.Value, obj.Title);
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Generate Excerpts
//

Console.WriteLine();
log.Dbg("== Get Posts with generated excerpt ==");
_ = await bcg.Db.Query.PostsAsync<PostModelWithContent>(opt => opt with
{
	SortRandom = true
}, GenerateExcerpt.Create())
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No posts found.");
		}

		foreach (var item in x)
		{
			log.Dbg("Post {Id:0000}: {@Content}", item.Id.Value, item.Content);
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Get attachments
//

Console.WriteLine();
log.Dbg("== Get Attachments ==");
_ = await bcg.Db.Query.AttachmentsAsync<Attachment>(opt => opt with
{
	Ids = ImmutableList.Create<WpPostId>(new(802L), new(862L), new(2377L))
})
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No attachments found.");
		}

		foreach (var item in x)
		{
			log.Dbg("Attachment {Id:0000}: {Description}", item.Id.Value, item.Title);
			log.Dbg("  - Description: {Description}", item.Description);
			log.Dbg("  - Url: {Url}", item.Url);
			log.Dbg("  - UrlPath: {UrlPath}", item.UrlPath);
			log.Dbg("  - FilePath: {FilePath}", item.GetFilePath(bcg.Db.WpConfig.UploadsPath));
		}
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Get attachment file path
//

Console.WriteLine();
log.Dbg("== Get Attachment file path ==");
_ = await bcg.Db.Query.AttachmentFilePathAsync(new(802L))
.AuditAsync(
	some: x => log.Dbg("Path: {FilePath}", x),
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

//
// Test paging
//

Console.WriteLine();
log.Dbg("== Test Paging Values ==");
_ = await bcg.Db.Query.PostsAsync<PostModel>(2, opt => opt with
{
	SortRandom = true,
	Maximum = 15
})
.AuditAsync(
	some: x =>
	{
		if (!x.Any())
		{
			log.Err("No posts found.");
		}

		log.Dbg("Pages: {NumberOfPages}", x.Values.Pages);
		log.Dbg("  - Items: {Items}", x.Values.Items);
		log.Dbg("  - First Page: {FirstPage}", x.Values.LowerPage);
		log.Dbg("  - Last Page: {LastPage}", x.Values.UpperPage);
		log.Dbg("  - First Item: {FirstItem}", x.Values.FirstItem);
		log.Dbg("  - Last Item: {LastItem}", x.Values.LastItem);
	},
	none: r => log.Msg(r)
)
.ConfigureAwait(false);

// End
Console.WriteLine();
log.Dbg("Complete.");
