// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Net.Http;
using Jeebs.Config;
using Microsoft.Extensions.Options;

namespace Jeebs.Services.Drivers.Twitter.Tweetinvi;

/// <summary>
/// Tweetinvi Twitter Driver arguments
/// </summary>
public sealed class TweetinviTwitterDriverArgs : DriverArgs<TwitterConfig>
{
	/// <summary>
	/// IHttpClientFactory
	/// </summary>
	public IHttpClientFactory Factory { get; set; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="factory">IHttpClientFactory</param>
	/// <param name="log">ILog</param>
	/// <param name="jeebsConfig">JeebsConfig</param>
	public TweetinviTwitterDriverArgs(
		IHttpClientFactory factory,
		ILog log,
		IOptions<JeebsConfig> jeebsConfig
	) : base(log, jeebsConfig, c => c.Twitter) =>
		Factory = factory;
}
