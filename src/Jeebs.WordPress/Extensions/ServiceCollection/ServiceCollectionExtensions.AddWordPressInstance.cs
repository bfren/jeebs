// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Config.WordPress;
using Jeebs.Data.Adapters.Dapper;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress;

public static partial class ServiceCollectionExtensions
{
	/// <summary>
	/// Register WordPress instance.
	/// </summary>
	/// <param name="this">IServiceCollection.</param>
	/// <param name="name">Name of the WordPress instance in configuration settings.</param>
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
		/// <param name="services">IServiceCollection.</param>
		/// <param name="name">Name of the WordPress instance in configuration settings.</param>
		public FluentAddWordPress(IServiceCollection services, string name) =>
			(this.services, section) = (services, $"{WpConfig.Key}:{name}");

		/// <summary>
		/// Register WordPress instance and configuration type.
		/// </summary>
		/// <typeparam name="TWp">WordPress instance type.</typeparam>
		/// <typeparam name="TWpConfig">WordPress configuration type.</typeparam>
		/// <param name="config">IConfiguration.</param>
		public IServiceCollection Using<TWp, TWpConfig>(IConfiguration config)
			where TWp : class, IWp<TWpConfig>
			where TWpConfig : WpConfig
		{
			// TWp is the app's own type, so its constructor is only known at run time - build the factory
			// once here rather than in every scope
			var createWp = ActivatorUtilities.CreateFactory<TWp>([typeof(IDbClient)]);

			// WordPress gets its own cached client, so the app's other databases keep their own
			// IDbClient, IAdapter and ICacheService
			_ = services
				.AddDistributedMemoryCache()
				.Configure<TWpConfig>(config.GetSection(section))
				.AddScoped(p =>
				{
					var cache = new DistributedCacheService(p.GetRequiredService<IOptions<DbConfig>>(), p.GetRequiredService<IDistributedCache>());
					var adapter = new DapperAdapter(DapperTypeMapper.Instance, cache);
					var client = new MySqlDbClient(adapter, p.GetRequiredService<ILoggerFactory>());
					return createWp(p, [client]);
				});

			return services;
		}
	}
}
