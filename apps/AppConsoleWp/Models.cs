// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using AppConsoleWp.Bcg;
using AppConsoleWp.Usa;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;

namespace AppConsoleWp;

internal record class PostModel : WpPostEntityWithId
{
	public string Title { get; init; } = string.Empty;

	public MetaDictionary Meta { get; init; } = new();
}

internal record class PostModelWithContent : PostModel
{
	public string Content { get; init; } = string.Empty;
}

internal record class PostModelWithCustomFields : PostModel
{
	public FeaturedImageId FeaturedImage { get; set; } = new();
}

internal record class SermonModel : WpPostEntityWithId
{
	public string Title { get; init; } = string.Empty;

	public DateTime PublishedOn { get; init; }
}

internal record class SermonModelWithTaxonomies : SermonModel
{
	public TermList BibleBooks { get; init; } = new(WpBcg.Taxonomies.BibleBook);

	public TermList Series { get; init; } = new(WpBcg.Taxonomies.Series);
}

internal record class SermonModelWithCustomFields : SermonModel
{
	public MetaDictionary Meta { get; init; } = new();

	public PassageCustomField Passage { get; init; } = new();

	public PdfCustomField? Pdf { get; init; }

	public AudioRecordingCustomField? Audio { get; init; }

	public FirstPreachedCustomField FirstPreached { get; init; } = new();

	public FeedImageCustomField? Image { get; init; }
}

internal record class TaxonomyModel : WpTermEntityWithId
{
	public string Title { get; init; } = string.Empty;

	public long Count { get; init; }
}

internal record class Attachment : PostAttachment;
