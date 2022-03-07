// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jeebs.Services.Twitter;

/// <summary>
/// Twitter Driver
/// </summary>
public interface ITwitterDriver<T>
	where T : ITwitterTweet
{
	/// <summary>
	/// Get the full-size profile image URL for a user
	/// </summary>
	/// <param name="screenName">Screen name</param>
	Task<Maybe<Stream>> GetProfileImageStreamAsync(string screenName);

	/// <inheritdoc cref="GetTweetsAsync(string, bool, int)"/>
	Task<Maybe<List<T>>> GetTweetsAsync(string screenName);

	/// <inheritdoc cref="GetTweetsAsync(string, bool, int)"/>
	Task<Maybe<List<T>>> GetTweetsAsync(string screenName, bool excludeReplies);

	/// <summary>
	/// Retrieve a list of Tweets
	/// </summary>
	/// <param name="screenName">Screen name</param>
	/// <param name="excludeReplies">Whether or not to exclude replies</param>
	/// <param name="limit">The maximum number of tweets to return</param>
	Task<Maybe<List<T>>> GetTweetsAsync(string screenName, bool excludeReplies, int limit);
}
