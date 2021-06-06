// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Config;
using Jeebs.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.WordPress
{
	/// <summary>
	/// IServiceCollection extension methods
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Register WordPress instance
		/// </summary>
		/// <param name="this">IServiceCollection</param>
		/// <param name="name">Name of the WordPress Instance</param>
		public static FluentAddWordPress AddWordPressInstance(this IServiceCollection @this, string name) =>
			new(@this, name);

		/// <summary>
		/// Fluently configure WordPress registration
		/// </summary>
		public sealed class FluentAddWordPress
		{
			/// <summary>
			/// IServiceCollection
			/// </summary>
			private readonly IServiceCollection services;

			/// <summary>
			/// Configuration Section Key
			/// </summary>
			private readonly string section;

			/// <summary>
			/// Start configuring WordPress instance
			/// </summary>
			/// <param name="services">IServiceCollection</param>
			/// <param name="name">Name of the WordPress Instance</param>
			public FluentAddWordPress(IServiceCollection services, string name) =>
				(this.services, section) = (services, $"{WpConfig.Key}:{name}");

			/// <summary>
			/// Register WordPress instance and configuration type
			/// </summary>
			/// <typeparam name="TWp">WordPress instance</typeparam>
			/// <typeparam name="TWpConfig">WordPress configuration</typeparam>
			/// <param name="config">IConfiguration</param>
			public IServiceCollection Using<TWp, TWpConfig>(IConfiguration config)
				where TWp : class, IWp<TWpConfig>
				where TWpConfig : WpConfig =>
				services
					.Configure<TWpConfig>(config.GetSection(section))
					.AddData()
					.AddSingleton<TWp>();
		}
	}
}
