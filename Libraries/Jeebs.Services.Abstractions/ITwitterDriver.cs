// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jeebs.Services.Twitter
{
	/// <summary>
	/// Twitter Driver
	/// </summary>
	public interface ITwitterDriver<T>
		where T : ITwitterTweet
	{
		/// <summary>
		/// Get the full-size profile image URL for a user
		/// </summary>
		/// <param name="r">Result: value is screen name</param>
		Task<IR<Stream>> GetProfileImageStreamAsync(IOkV<string> r);

		/// <summary>
		/// Retrieve a list of Tweets
		/// </summary>
		/// <param name="r">Result: value is screen name</param>
		/// <param name="excludeReplies">[Optional] Whether or not to exclude replies</param>
		/// <param name="limit">[Optional] The maximum number of tweets to return</param>
		Task<IR<List<T>>> GetTweetsAsync(IOkV<string> r, bool excludeReplies = true, int limit = 10);
	}
}
