// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Services.Twitter.Models
{
	/// <inheritdoc cref="ITwitterTweet"/>
	public sealed record class TweetModel : ITwitterTweet
	{
		/// <inheritdoc/>
		public ITwitterAuthor Author { get; init; } = new AuthorModel();

		/// <inheritdoc/>
		public DateTime TweetedOn { get; init; }

		/// <inheritdoc/>
		public int Length { get; init; }

		/// <inheritdoc/>
		public string Text { get; init; } = string.Empty;
	}
}
