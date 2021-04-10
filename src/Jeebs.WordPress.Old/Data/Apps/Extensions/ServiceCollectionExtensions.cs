// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// IServiceCollection extension methods
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Configure data
		/// </summary>
		/// <param name="this">IServiceCollection</param>
		public static IServiceCollection AddData(this IServiceCollection @this)
		{
			// Add DbLogs as Singleton so Db instance can be persisted
			@this.AddSingleton<DbLogs>();

			// Return
			return @this;
		}
	}
}
