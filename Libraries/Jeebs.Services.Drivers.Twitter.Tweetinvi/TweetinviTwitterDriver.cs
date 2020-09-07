using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jeebs.Config;
using Jeebs.Services.Twitter;
using Jeebs.Services.Twitter.Models;
using Jm.Services.Twitter.TweetinviTwitterSvc.GetProfileImageAsync;
using Jm.Services.Twitter.TweetinviTwitterSvc.GetTweetsAsync;
using Microsoft.Extensions.Options;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Jeebs.Services.Drivers.Twitter.Tweetinvi
{
	/// <summary>
	/// Tweetinvi Service
	/// </summary>
	public sealed class TweetinviTwitterDriver : ITwitterDriver
	{
		private readonly TwitterConfig settings;

		private readonly HttpClient client;

		private readonly ILog log;

		/// <summary>
		/// Setup dependencies
		/// </summary>
		/// <param name="settings">TwitterSettings</param>
		/// <param name="client">HttpClient</param>
		/// <param name="log">ILog</param>
		public TweetinviTwitterDriver(IOptions<TwitterConfig> settings, HttpClient client, ILog log)
			: this(settings.Value, client, log) { }

		/// <summary>
		/// Setup dependencies
		/// </summary>
		/// <param name="settings">TwitterSettings</param>
		/// <param name="client">HttpClient</param>
		/// <param name="log">ILog</param>
		public TweetinviTwitterDriver(TwitterConfig settings, HttpClient client, ILog log)
			=> (this.settings, this.client, this.log) = (settings, client, log);

		private async Task<IR<IUser>> GetUser(IOkV<string> r)
		{
			// Get user
			return await r
				.Link()
					.Handle().With(setCredentialsException)
					.Run(setCredentials)
				.Link()
					.Handle().With(getUserException)
					.MapAsync(getUser);

			// Set credentials
			void setCredentials()
			{
				log.Information("Setting Twitter authorisation credentials");
				log.Information($"Twitter consumerSecret: {settings.ConsumerSecret}");
				log.Information($"Twitter consumerKey: {settings.ConsumerKey}");
				log.Information($"Twitter userAccessSecret: {settings.UserAccessSecret}");
				log.Information($"Twitter userAccessToken: {settings.UserAccessToken}");

				Auth.SetUserCredentials(
					consumerKey: settings.ConsumerKey,
					consumerSecret: settings.ConsumerSecret,
					userAccessToken: settings.UserAccessToken,
					userAccessSecret: settings.UserAccessSecret);
			}

			// Handle set credentials exception
			void setCredentialsException(IR<string> r, System.Exception ex)
			{
				log.Error(ex, "Error setting credentials");
				r.AddMsg(new ErrorSettingCredentialsMsg(ex));
			}

			// Get user
			static async Task<IR<IUser>> getUser(IOkV<string> r)
			{
				var user = await UserAsync.GetUserFromScreenName(r.Value).ConfigureAwait(false);
				return r.OkV(user);
			}

			// Handle get user exceptions
			void getUserException(IR<string> r, System.Exception ex)
			{
				log.Information("Unable to find user");
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
				log.Information("Twitter profile image: '{0}'", url);
				return r.OkV(url);
			}

			// Handle get profile image URL exceptions
			void getUrlException(IR<IUser> r, System.Exception ex)
			{
				log.Error(ex, "Error getting Twitter Profile image URL");
				r.AddMsg(new ErrorGettingProfileImageUrlMsg(ex));
			}

			// Get profile image stream
			async Task<IR<System.IO.Stream>> getStream(IOkV<string> r)
			{
				var stream = await client.GetStreamAsync(r.Value).ConfigureAwait(false);
				return r.OkV(stream);
			}

			// Handle get profile image stream exceptions
			void getStreamException(IR<string> r, System.Exception ex)
			{
				log.Error(ex, "Error getting Twitter Profile image stream");
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
				var param = new UserTimelineParameters
				{
					MaximumNumberOfTweetsToRetrieve = limit,
					ExcludeReplies = excludeReplies
				};

				// Get tweets - return empty list if null or empty
				var timeline = await TimelineAsync.GetUserTimeline(r.Value, param).ConfigureAwait(false);
				return r.OkV(timeline switch
				{
					IEnumerable<ITweet> x => x.ToList(),
					_ => new List<ITweet>()
				});
			}

			// Handle get timeline exceptions
			void getTimelineException(IR<IUser> r, System.Exception ex)
			{
				log.Error(ex, "Error getting Twitter timeline");
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
									 ProfileImageUrl = t.CreatedBy.ProfileImageUrlHttps
								 },
								 TweetedOn = t.CreatedAt,
								 Text = t.Text
							 };

				return r.OkV(tweets.ToList());
			}

			// Handle get timeline exceptions
			void convertTweetsException(IR<List<ITweet>> r, System.Exception ex)
			{
				log.Error(ex, "Error converting tweets");
				r.AddMsg(new ErrorConvertingTweetsMsg(ex));
			}
		}
	}
}

