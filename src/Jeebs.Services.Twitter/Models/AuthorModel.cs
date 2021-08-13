// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Services.Twitter.Models
{
	/// <inheritdoc cref="ITwitterAuthor"/>
	public sealed record class AuthorModel : ITwitterAuthor
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
