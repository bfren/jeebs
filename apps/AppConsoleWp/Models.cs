// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using AppConsoleWp.Bcg;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;

namespace AppConsoleWp
{
	internal record PostModel : IWithId<WpPostId>
	{
		public WpPostId Id { get; init; } = new();

		public long PostId { get => Id.Value; init => Id = new(value); }

		public string Title { get; set; } = string.Empty;

		public MetaDictionary Meta { get; set; } = new();
	}

	internal record PostModelWithContent : PostModel
	{
		public string Content { get; set; } = string.Empty;
	}

	internal class SermonModel : IWithId<WpPostId>
	{
		public WpPostId Id { get; init; } = new();

		public long PostId { get => Id.Value; init => Id = new(value); }

		public string Title { get; set; } = string.Empty;

		public DateTime PublishedOn { get; set; }
	}

	internal class SermonModelWithTaxonomies : SermonModel
	{
		public TermList BibleBooks { get; set; } = new(WpBcg.Taxonomies.BibleBook);

		public TermList Series { get; set; } = new(WpBcg.Taxonomies.Series);
	}

	internal class SermonModelWithCustomFields : SermonModel
	{
		public MetaDictionary Meta { get; set; } = new();

		public PassageCustomField Passage { get; set; } = new();

		public PdfCustomField? Pdf { get; set; }

		public AudioRecordingCustomField? Audio { get; set; }

		public FirstPreachedCustomField FirstPreached { get; set; } = new();

		public FeedImageCustomField? Image { get; set; }
	}

	internal class TaxonomyModel : IWithId<WpTermId>
	{
		public WpTermId Id { get; init; } = new();

		public long TermId { get => Id.Value; init => Id = new(value); }

		public string Title { get; set; } = string.Empty;

		public long Count { get; set; }
	}

	internal record Attachment : WpAttachmentEntity;
}
