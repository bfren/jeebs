using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Services.Twitter.Models;

namespace Jeebs.Services.Twitter
{
	/// <summary>
	/// Twitter Service
	/// </summary>
	public interface ITwitterService
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
		Task<IR<List<TweetModel>>> GetTweetsAsync(IOkV<string> r, bool excludeReplies = true, int limit = 10);
	}
}
