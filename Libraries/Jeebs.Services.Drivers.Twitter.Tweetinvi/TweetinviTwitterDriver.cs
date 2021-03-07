// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jeebs.Config;
using Jeebs.Services.Twitter;
using Jeebs.Services.Twitter.Models;
using Jm.Services.Twitter.TweetinviTwitterDriver.GetProfileImageAsync;
using Jm.Services.Twitter.TweetinviTwitterDriver.GetTweetsAsync;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Jeebs.Services.Drivers.Twitter.Tweetinvi
{
	/// <summary>
	/// Tweetinvi Twitter Driver
	/// </summary>
	public abstract class TweetinviTwitterDriver : Driver<TwitterConfig>, ITwitterDriver
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

		private async Task<IR<IUser>> GetUser(IOkV<string> r)
		{
			// Get user
			return await r
				.Link()
					.Handle().With(getUserException)
					.MapAsync(getUser);

			// Get user
			async Task<IR<IUser>> getUser(IOkV<string> r)
			{
				var user = await client.Users.GetUserAsync(r.Value).ConfigureAwait(false);
				return r.OkV(user);
			}

			// Handle get user exceptions
			void getUserException(IR<string> r, System.Exception ex)
			{
				Log.Information("Unable to find user");
				r.AddMsg(new ErrorGettingUserMsg(ex));
			}
		}

		/// <inheritdoc/>
		public async Task<IR<System.IO.Stream>> GetProfileImageStreamAsync(IOkV<string> r)
		{
			return r
				.Link()
					.MapAsync(GetUser).Await()
				.Link()
					.Handle().With(getUrlException)
					.MapAsync(getUrl).Await()
				.Link()
					.Handle().With(getStreamException)
					.MapAsync(getStream).Await();

			// Get profile image URL
			async Task<IR<string>> getUrl(IOkV<IUser> r)
			{
				var url = r.Value.ProfileImageUrlFullSize.Replace("http://", "https://");
				Log.Information("Twitter profile image: '{0}'", url);
				return r.OkV(url);
			}

			// Handle get profile image URL exceptions
			void getUrlException(IR<IUser> r, System.Exception ex)
			{
				Log.Error(ex, "Error getting Twitter Profile image URL");
				r.AddMsg(new ErrorGettingProfileImageUrlMsg(ex));
			}

			// Get profile image stream
			async Task<IR<System.IO.Stream>> getStream(IOkV<string> r)
			{
				using var client = factory.CreateClient();
				var stream = await client.GetStreamAsync(r.Value).ConfigureAwait(false);
				return r.OkV(stream);
			}

			// Handle get profile image stream exceptions
			void getStreamException(IR<string> r, System.Exception ex)
			{
				Log.Error(ex, "Error getting Twitter Profile image stream");
				r.AddMsg(new ErrorGettingProfileImageStreamMsg(ex));
			}
		}

		/// <inheritdoc/>
		public async Task<IR<List<TweetModel>>> GetTweetsAsync(IOkV<string> r, bool excludeReplies = true, int limit = 10)
		{
			return r
				.Link()
					.MapAsync(GetUser).Await()
				.Link()
					.Handle().With(getTimelineException)
					.MapAsync(getTimeline).Await()
				.Link()
					.Handle().With(convertTweetsException)
					.MapAsync(convertTweets).Await();

			// Get timeline
			async Task<IR<List<ITweet>>> getTimeline(IOkV<IUser> r)
			{
				// Set parameters
				var param = new GetUserTimelineParameters(r.Value)
				{
					PageSize = limit,
					ExcludeReplies = excludeReplies
				};

				// Get tweets - return empty list if null or empty
				var timeline = await client.Timelines.GetUserTimelineAsync(param).ConfigureAwait(false);
				return r.OkV(timeline switch
				{
					IEnumerable<ITweet> x =>
						x.ToList(),

					_ =>
						new List<ITweet>()
				});
			}

			// Handle get timeline exceptions
			void getTimelineException(IR<IUser> r, System.Exception ex)
			{
				Log.Error(ex, "Error getting Twitter timeline");
				r.AddMsg(new ErrorGettingTimelineMsg(ex));
			}

			// Convert the tweets to TweetModel
			static async Task<IR<List<TweetModel>>> convertTweets(IOkV<List<ITweet>> r)
			{
				var tweets = from t in r.Value
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

				return r.OkV(tweets.ToList());
			}

			// Handle get timeline exceptions
			void convertTweetsException(IR<List<ITweet>> r, System.Exception ex)
			{
				Log.Error(ex, "Error converting tweets");
				r.AddMsg(new ErrorConvertingTweetsMsg(ex));
			}
		}
	}
}

