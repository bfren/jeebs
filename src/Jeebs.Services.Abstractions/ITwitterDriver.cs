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
	Task<Option<Stream>> GetProfileImageStreamAsync(string screenName);

	/// <summary>
	/// Retrieve a list of Tweets
	/// </summary>
	/// <param name="screenName">Screen name</param>
	/// <param name="excludeReplies">[Optional] Whether or not to exclude replies</param>
	/// <param name="limit">[Optional] The maximum number of tweets to return</param>
	Task<Option<List<T>>> GetTweetsAsync(string screenName, bool excludeReplies = true, int limit = 10);
}
