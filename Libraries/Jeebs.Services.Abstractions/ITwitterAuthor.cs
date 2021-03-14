// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Services.Twitter
{
	/// <summary>
	/// Twitter Author
	/// </summary>
	public interface ITwitterAuthor
	{
		/// <summary>
		/// ScreenName
		/// </summary>
		string ScreenName { get; init; }

		/// <summary>
		/// FullName
		/// </summary>
		string FullName { get; init; }

		/// <summary>
		/// ProfileUrl
		/// </summary>
		string ProfileUrl { get; init; }

		/// <summary>
		/// ProfileImageUrl
		/// </summary>
		string ProfileImageUrl { get; init; }
	}
}