// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;
using Jeebs.Services.Twitter;
using Jeebs.Services.Twitter.Models;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using static F.OptionF;

namespace Jeebs.Services.Drivers.Twitter.Tweetinvi
{
	/// <summary>
	/// Tweetinvi Twitter Driver
	/// </summary>
	public abstract class TweetinviTwitterDriver : Driver<TwitterConfig>, ITwitterDriver<TweetModel>
	{
		/// <summary>
		/// Add required services - called by <see cref="ServiceCollectionExtensions"/>
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		public static void AddRequiredServices(IServiceCollection services) =>
			services.AddHttpClient();

		private readonly IHttpClientFactory factory;

		private readonly TwitterClient client;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Service name</param>
		/// <param name="args">TweetinviTwitterDriverArgs</param>
		protected TweetinviTwitterDriver(string name, TweetinviTwitterDriverArgs args) : base(name, args)
		{
			factory = args.Factory;
			client = new TwitterClient(
				ServiceConfig.ConsumerKey,
				ServiceConfig.ConsumerSecret,
				ServiceConfig.UserAccessToken,
				ServiceConfig.UserAccessSecret
			);
		}

		private Task<Option<IUser>> GetUser(string screenName) =>
			ReturnAsync(
				() => client.Users.GetUserAsync(screenName),
				e => new Msg.UserNotFoundExceptionMsg(screenName, e)
			);

		/// <inheritdoc/>
		public Task<Option<System.IO.Stream>> GetProfileImageStreamAsync(string screenName)
		{
			return Return(screenName)
				.BindAsync(
					GetUser
				)
				.MapAsync(
					getUrl,
					e => new Msg.GettingProfileImageUrlExceptionMsg(screenName, e)
				)
				.MapAsync(
					getStream,
					e => new Msg.GettingProfileImageStreamExceptionMsg(screenName, e)
				);

			// Get profile image URL
			string getUrl(IUser user)
			{
				var url = user.ProfileImageUrlFullSize.Replace("http://", "https://");
				Log.Debug("Twitter profile image: '{0}'", url);
				return url;
			}

			// Get profile image stream
			async Task<System.IO.Stream> getStream(string uri)
			{
				using var client = factory.CreateClient();
				return await client.GetStreamAsync(uri).ConfigureAwait(false);
			}
		}

		/// <inheritdoc/>
		public Task<Option<List<TweetModel>>> GetTweetsAsync(string screenName, bool excludeReplies = true, int limit = 10)
		{
			return Return(screenName)
				.BindAsync(
					GetUser
				)
				.MapAsync(
					getTimeline,
					e => new Msg.GettingTimelineExceptionMsg(screenName, e)
				)
				.MapAsync(
					convertTweets,
					e => new Msg.ConvertingTweetsExceptionMsg(e)
				);

			// Get timeline
			Task<ITweet[]> getTimeline(IUser user)
			{
				// Set parameters
				var param = new GetUserTimelineParameters(user)
				{
					PageSize = limit,
					ExcludeReplies = excludeReplies
				};

				// Get tweets - return empty list if null or empty
				return client.Timelines.GetUserTimelineAsync(param);
			}

			// Convert the tweets to TweetModel
			static List<TweetModel> convertTweets(ITweet[] tweets)
			{
				var models = from t in tweets
							 select new TweetModel
							 {
								 Author = new AuthorModel
								 {
									 ScreenName = t.CreatedBy.ScreenName,
									 FullName = t.CreatedBy.Name,
									 ProfileUrl = t.CreatedBy.Url,
									 ProfileImageUrl = t.CreatedBy.ProfileImageUrl400x400
								 },
								 TweetedOn = t.CreatedAt.Date,
								 Text = t.Text
							 };

				return models.ToList();
			}
		}

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Exception converting tweets</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record class ConvertingTweetsExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Exception getting profile image stream</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record class GettingProfileImageStreamExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Exception getting profile image URL</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record class GettingProfileImageUrlExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Exception getting user timeline</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record class GettingTimelineExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Exception getting user</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record class GettingUserExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>User not found</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record class UserNotFoundExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}

