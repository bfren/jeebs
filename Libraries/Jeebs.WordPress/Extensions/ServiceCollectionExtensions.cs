using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Apps;
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
		/// <param name="services">IServiceCollection</param>
		/// <param name="section">[Optional] Section Key for retrieving WordPress configuration</param>
		public static FluentWordPress AddWordPressInstance(this IServiceCollection services, in string section = WpConfig.Key)
			=> new FluentWordPress(ref services, section);

		/// <summary>
		/// Fluently configure WordPress registration
		/// </summary>
		public sealed class FluentWordPress
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
			/// <param name="section">Configuration Section Key</param>
			public FluentWordPress(ref IServiceCollection services, in string section)
			{
				Services = services;
				this.section = section;
			}

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
