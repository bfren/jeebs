// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

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