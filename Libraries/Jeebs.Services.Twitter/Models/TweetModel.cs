// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs.Services.Twitter.Models
{
	/// <summary>
	/// Twitter Tweet
	/// </summary>
	public sealed record TweetModel
	{
		/// <summary>
		/// Author
		/// </summary>
		public AuthorModel Author { get; init; } = new();

		/// <summary>
		/// TweetedOn
		/// </summary>
		public DateTime TweetedOn { get; init; }

		/// <summary>
		/// Length
		/// </summary>
		public int Length { get; init; }

		/// <summary>
		/// Text
		/// </summary>
		public string Text { get; init; } = string.Empty;
	}
}
