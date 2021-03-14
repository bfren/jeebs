// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Services.Twitter.Models
{
	/// <inheritdoc cref="ITwitterAuthor"/>
	public sealed record AuthorModel : ITwitterAuthor
	{
		/// <inheritdoc/>
		public string ScreenName { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string FullName { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string ProfileUrl { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string ProfileImageUrl { get; init; } = string.Empty;
	}
}
