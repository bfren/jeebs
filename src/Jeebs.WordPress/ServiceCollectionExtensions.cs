// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.WordPress;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.WordPress;

/// <summary>
/// IServiceCollection extension methods.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Register WordPress instance.
	/// </summary>
	/// <param name="this">IServiceCollection</param>
	/// <param name="name">Name of the WordPress instance in configuration settings</param>
	public static FluentAddWordPress AddWordPressInstance(this IServiceCollection @this, string name) =>
		new(@this, name);

	/// <summary>
	/// Fluently configure WordPress registration.
	/// </summary>
	public sealed class FluentAddWordPress
	{
		/// <summary>
		/// IServiceCollection.
		/// </summary>
		private readonly IServiceCollection services;

		/// <summary>
		/// Configuration Section Key.
		/// </summary>
		private readonly string section;

		/// <summary>
		/// Start configuring WordPress instance.
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		/// <param name="name">Name of the WordPress instance in configuration settings</param>
		public FluentAddWordPress(IServiceCollection services, string name) =>
			(this.services, section) = (services, $"{WpConfig.Key}:{name}");

		/// <summary>
		/// Register WordPress instance and configuration type.
		/// </summary>
		/// <typeparam name="TWp">WordPress instance type</typeparam>
		/// <typeparam name="TWpConfig">WordPress configuration type</typeparam>
		/// <param name="config">IConfiguration</param>
		public IServiceCollection Using<TWp, TWpConfig>(IConfiguration config)
			where TWp : class, IWp<TWpConfig>
			where TWpConfig : WpConfig =>
			services
				.Configure<TWpConfig>(config.GetSection(section))
				.AddScoped<TWp>();
	}
}
