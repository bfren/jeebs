// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

		private async Task<Option<IUser>> GetUser(string screenName) =>
			await client.Users.GetUserAsync(screenName).ConfigureAwait(false) switch
			{
				IUser user =>
					Return(user),

				_ =>
					None<IUser>(new Msg.UserNotFoundMsg(screenName))
			};

		/// <inheritdoc/>
		public Task<Option<System.IO.Stream>> GetProfileImageStreamAsync(string screenName)
		{
			return Return(screenName)
				.BindAsync(
					GetUser,
					e => new Msg.GettingUserExceptionMsg(screenName, e)
				)
				.MapAsync(
					getUrl,
					e => new Msg.GettingProfileImageUrlExceptionMsg(screenName, e)
				)
				.BindAsync(
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
			async Task<Option<System.IO.Stream>> getStream(string uri)
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
					GetUser,
					e => new Msg.GettingUserExceptionMsg(screenName, e)
				)
				.BindAsync(
					getTimeline,
					e => new Msg.GettingTimelineExceptionMsg(screenName, e)
				)
				.MapAsync(
					convertTweets,
					e => new Msg.ConvertingTweetsExceptionMsg(e)
				);

			// Get timeline
			async Task<Option<List<ITweet>>> getTimeline(IUser user)
			{
				// Set parameters
				var param = new GetUserTimelineParameters(user)
				{
					PageSize = limit,
					ExcludeReplies = excludeReplies
				};

				// Get tweets - return empty list if null or empty
				var timeline = await client.Timelines.GetUserTimelineAsync(param).ConfigureAwait(false);
				return timeline switch
				{
					IEnumerable<ITweet> x =>
						x.ToList(),

					_ =>
						new List<ITweet>()
				};
			}

			// Convert the tweets to TweetModel
			static List<TweetModel> convertTweets(List<ITweet> tweets)
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
			public sealed record ConvertingTweetsExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Exception getting profile image stream</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record GettingProfileImageStreamExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Exception getting profile image URL</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record GettingProfileImageUrlExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Exception getting user timeline</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record GettingTimelineExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Exception getting user</summary>
			/// <param name="ScreenName">Screen Name</param>
			/// <param name="Exception">Exception object</param>
			public sealed record GettingUserExceptionMsg(string ScreenName, Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>User not found</summary>
			/// <param name="Value">Screen Name</param>
			public sealed record UserNotFoundMsg(string Value) : WithValueMsg<string> { }
		}
	}
}

