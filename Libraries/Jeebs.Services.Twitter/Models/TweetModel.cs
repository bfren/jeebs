using System;
using System.Collections.Generic;
using System.Text;

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
