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
		public static FluentAddWordPress AddWordPressInstance(this IServiceCollection services, string section = WpConfig.Key)
			=> new FluentAddWordPress(ref services, section);

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
			/// <param name="section">Configuration Section Key</param>
			public FluentAddWordPress(ref IServiceCollection services, string section)
			{
				Services = services;
				this.section = $"{WpConfig.Key}:{section}";
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
