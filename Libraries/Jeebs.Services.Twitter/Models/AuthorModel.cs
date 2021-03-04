using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Services.Twitter.Models
{
	/// <summary>
	/// Twitter Author
	/// </summary>
	public sealed record AuthorModel
	{
		/// <summary>
		/// ScreenName
		/// </summary>
		public string ScreenName { get; init; } = string.Empty;

		/// <summary>
		/// FullName
		/// </summary>
		public string FullName { get; init; } = string.Empty;

		/// <summary>
		/// ProfileUrl
		/// </summary>
		public string ProfileUrl { get; init; } = string.Empty;

		/// <summary>
		/// ProfileImageUrl
		/// </summary>
		public string ProfileImageUrl { get; init; } = string.Empty;
	}
}
