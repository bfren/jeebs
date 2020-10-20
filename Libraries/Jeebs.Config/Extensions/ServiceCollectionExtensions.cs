using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Config
{
	/// <summary>
	/// Extensions for IServiceCollection
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Begin Fluent Binding a Configuration Settings to an object
		/// </summary>
		/// <typeparam name="T">Settings object type</typeparam>
		/// <param name="services">IServiceCollection object</param>
		/// <returns>FluentBind object</returns>
		public static FluentBind<T> Bind<T>(this IServiceCollection services)
			where T : class
			=> new FluentBind<T>(services);

		/// <summary>
		/// Fluent Bind
		/// </summary>
		/// <typeparam name="T">Type to bind configuration section to</typeparam>
		public class FluentBind<T>
			where T : class
		{
			/// <summary>
			/// IServiceCollection object
			/// </summary>
			internal IServiceCollection Services { get; }

			/// <summary>
			/// Configuration section key (e.g. 'settings:app')
			/// </summary>
			internal string? SectionKey { get; private set; }

			/// <summary>
			/// IConfiguration object
			/// </summary>
			internal IConfiguration? Config { get; private set; }

			/// <summary>
			/// Setup dependencies
			/// </summary>
			/// <param name="services">IServiceCollection object</param>
			public FluentBind(IServiceCollection services)
				=> Services = services;

			/// <summary>
			/// Bind to the specified section
			/// </summary>
			/// <param name="sectionKey">Section key (e.g. 'settings:app')</param>
			/// <returns>FluentBind object</returns>
			public FluentBind<T> To(string sectionKey)
				=> Check(() => SectionKey = JeebsConfig.GetKey(sectionKey));

			/// <summary>
			/// Bind using the specified IConfigurationRoot
			/// </summary>
			/// <param name="config">IConfigurationRoot object</param>
			/// <returns>FluentBind object</returns>
			public FluentBind<T> Using(IConfiguration config)
				=> Check(() => Config = config);

			/// <summary>
			/// Save the binding to the IServiceCollection
			/// </summary>
			private FluentBind<T> Check(Action run)
			{
				// Run action
				run();

				// Check services
				if (Config is null || SectionKey is null)
				{
					return this;
				}

				// Configure the options and return
				Services.Configure<T>(opt => Config.GetSection(SectionKey).Bind(opt));
				return this;
			}
		}
	}
}
