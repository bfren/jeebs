// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using AppConsoleWordPress.Bcg;
using Jeebs.WordPress;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Enums;

namespace AppConsoleWordPress
{
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
