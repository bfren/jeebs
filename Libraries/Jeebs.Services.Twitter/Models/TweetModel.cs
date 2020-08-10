using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Services.Twitter.Models
{
	/// <summary>
	/// Twitter Tweet
	/// </summary>
	public sealed class TweetModel
	{
		/// <summary>
		/// Author
		/// </summary>
		public AuthorModel Author { get; set; } = new AuthorModel();

		/// <summary>
		/// TweetedOn
		/// </summary>
		public DateTime TweetedOn { get; set; }

		/// <summary>
		/// Length
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		/// Text
		/// </summary>
		public string Text { get; set; } = string.Empty;
	}
}
