// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Services.Twitter
{
	/// <summary>
	/// Twitter Tweet
	/// </summary>
	public interface ITwitterTweet
	{
		/// <summary>
		/// Author
		/// </summary>
		ITwitterAuthor Author { get; init; }

		/// <summary>
		/// TweetedOn
		/// </summary>
		DateTime TweetedOn { get; init; }

		/// <summary>
		/// Length
		/// </summary>
		int Length { get; init; }

		/// <summary>
		/// Text
		/// </summary>
		string Text { get; init; }
	}
}