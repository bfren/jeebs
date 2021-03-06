// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
