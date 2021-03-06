﻿// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs.Config;
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
			new(ref @this, name);

		/// <summary>
		/// Fluently configure WordPress registration
		/// </summary>
		public sealed class FluentAddWordPress
		{
			/// <summary>
			/// IServiceCollection
			/// </summary>
			private IServiceCollection Services { get; }

			/// <summary>
			/// Configuration Section Key
			/// </summary>
			private readonly string section;

			/// <summary>
			/// Start configuring WordPress instance
			/// </summary>
			/// <param name="services">IServiceCollection</param>
			/// <param name="name">Name of the WordPress Instance</param>
			public FluentAddWordPress(ref IServiceCollection services, string name) =>
				(Services, section) = (services, $"{WpConfig.Key}:{name}");

			/// <summary>
			/// Register WordPress instance and configuration type
			/// </summary>
			/// <typeparam name="TWp">WordPress instance</typeparam>
			/// <typeparam name="TWpConfig">WordPress configuration</typeparam>
			/// <param name="config">IConfiguration</param>
			public IServiceCollection Using<TWp, TWpConfig>(IConfiguration config)
				where TWp : class, IWp<TWpConfig>
				where TWpConfig : WpConfig
			{
				Services.AddSingleton<TWp>();
				Services.Bind<TWpConfig>().To(section).Using(config);
				return Services;
			}
		}
	}
}
