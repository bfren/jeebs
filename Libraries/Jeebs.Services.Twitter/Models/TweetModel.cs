// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Services.Twitter.Models
{
	/// <inheritdoc cref="ITwitterTweet"/>
	public sealed record TweetModel : ITwitterTweet
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
