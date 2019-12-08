using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Apps
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
			where T : class => new FluentBind<T>(services);

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
			private readonly IServiceCollection services;

			/// <summary>
			/// Configuration section key (e.g. 'settings:app')
			/// </summary>
			private string? sectionKey;

			/// <summary>
			/// IConfiguration object
			/// </summary>
			private IConfiguration? config;

			/// <summary>
			/// Setup dependencies
			/// </summary>
			/// <param name="services">IServiceCollection object</param>
			public FluentBind(in IServiceCollection services) => this.services = services;

			/// <summary>
			/// Bind to the specified section
			/// </summary>
			/// <param name="sectionKey">Section key (e.g. 'settings:app')</param>
			/// <returns>FluentBind object</returns>
			public FluentBind<T> To(string sectionKey) => Check(() => this.sectionKey = sectionKey);

			/// <summary>
			/// Bind using the specified IConfigurationRoot
			/// </summary>
			/// <param name="config">IConfigurationRoot object</param>
			/// <returns>FluentBind object</returns>
			public FluentBind<T> Using(IConfiguration config) => Check(() => this.config = config);

			/// <summary>
			/// Save the binding to the IServiceCollection
			/// </summary>
			private FluentBind<T> Check(Action run)
			{
				// Run action
				run();

				// Check services
				if (services == null || config == null || sectionKey == null)
				{
					return this;
				}

				// Get section key
				sectionKey = JeebsConfig.GetKey(sectionKey);

				// Configure the options and return
				services.Configure<T>(opt => config.GetSection(sectionKey).Bind(opt));
				return this;
			}
		}
	}
}
