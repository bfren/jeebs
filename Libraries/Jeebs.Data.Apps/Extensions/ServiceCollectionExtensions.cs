﻿// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Data
{
	/// <summary>
	/// IServiceCollection extension methods
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Register data configuration
		/// </summary>
		/// <param name="this">IServiceCollection</param>
		/// <param name="section">[Optional] Section Key for retrieving database configuration</param>
		public static FluentData AddData(this IServiceCollection @this, string section = DbConfig.Key) =>
			new(@this, section);

		/// <summary>
		/// Fluently configure data registration
		/// </summary>
		public sealed class FluentData
		{
			/// <summary>
			/// IServiceCollection
			/// </summary>
			internal IServiceCollection Services { get; }

			/// <summary>
			/// Configuration Section Key
			/// </summary>
			internal string Section { get; }

			/// <summary>
			/// Start configuring data
			/// </summary>
			/// <param name="services">IServiceCollection</param>
			/// <param name="section">Configuration Section Key</param>
			public FluentData(IServiceCollection services, string section) =>
				(Services, Section) = (services, section);

			/// <summary>
			/// Register data configuration
			/// </summary>
			/// <param name="config">IConfiguration</param>
			public IServiceCollection Using(IConfiguration config)
			{
				Services.Bind<DbConfig>().To(Section).Using(config);
				return Services;
			}
		}
	}
}
