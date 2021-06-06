// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using AppConsoleWp.Bcg;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;

namespace AppConsoleWp
{
	internal class PostModel : IWithId<WpPostId>
	{
		public WpPostId Id { get; init; } = new();

		public long PostId { get => Id.Value; init => Id = new(value); }

		public string Title { get; init; } = string.Empty;

		public MetaDictionary Meta { get; init; } = new MetaDictionary();
	}

	internal class PostModelWithContent : PostModel
	{
		public string Content { get; init; } = string.Empty;
	}

	internal class SermonModel : IWithId<WpPostId>
	{
		public WpPostId Id { get; init; } = new();

		public long PostId { get => Id.Value; init => Id = new(value); }

		public string Title { get; init; } = string.Empty;

		public DateTime PublishedOn { get; init; }
	}

	internal class SermonModelWithTaxonomies : SermonModel
	{
		public TermList BibleBooks { get; set; } = new TermList(WpBcg.Taxonomies.BibleBook);

		public TermList Series { get; set; } = new TermList(WpBcg.Taxonomies.Series);
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

	internal class TaxonomyModel : IWithId<WpTermId>
	{
		public WpTermId Id { get; init; } = new();

		public long TermId { get => Id.Value; init => Id = new(value); }

		public string Title { get; set; } = string.Empty;

		public long Count { get; set; }
	}
}
