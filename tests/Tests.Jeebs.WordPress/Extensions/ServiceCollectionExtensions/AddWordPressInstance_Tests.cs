// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.WordPress;
using Jeebs.Data.Adapters.Dapper;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Jeebs.WordPress.ServiceCollectionExtensions_Tests;

public class AddWordPressInstance_Tests
{
	[Fact]
	public void Using__Does_Not_Register_App_Wide_Data_Services()
	{
		// Arrange
		var services = new ServiceCollection();
		var config = new ConfigurationBuilder().Build();

		// Act
		_ = services.AddWordPressInstance(Rnd.Str).Using<TestWp, WpConfig>(config);

		// Assert
		Assert.DoesNotContain(services, s => s.ServiceType == typeof(IDbClient));
		Assert.DoesNotContain(services, s => s.ServiceType == typeof(IAdapter));
		Assert.DoesNotContain(services, s => s.ServiceType == typeof(ITypeMapper));
		Assert.DoesNotContain(services, s => s.ServiceType == typeof(ICacheService));
		Assert.Contains(services, s => s.ServiceType == typeof(TestWp));
	}

	[Fact]
	public void Using__Resolves_TWp_With_Own_MySql_Client()
	{
		// Arrange
		var services = new ServiceCollection().AddLogging();
		var config = new ConfigurationBuilder().Build();
		_ = services.AddWordPressInstance(Rnd.Str).Using<TestWp, WpConfig>(config);
		using var provider = services.BuildServiceProvider();
		using var scope = provider.CreateScope();

		// Act
		var result = scope.ServiceProvider.GetRequiredService<TestWp>();

		// Assert
		var client = Assert.IsType<MySqlDbClient>(result.Client);
		_ = Assert.IsType<DapperAdapter>(client.Adapter);
	}

	public sealed class TestWp(IDbClient client, IOptions<WpConfig> config) : IWp<WpConfig>
	{
		public IDbClient Client { get; } = client;

		public WpConfig Config { get; } = config.Value;

		public IWpDb Db =>
			throw new NotSupportedException();

		public void RegisterCustomPostTypes() { }

		public void RegisterCustomTaxonomies() { }
	}
}
