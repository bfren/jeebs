// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Auth.Data.UncachedServiceProvider_Tests;

public class GetService_Tests
{
	private static IServiceProvider Setup(ICacheService cache) =>
		new ServiceCollection()
			.AddSingleton(cache)
			.AddTransient<IAdapter, TestAdapter>()
			.AddSingleton(Substitute.For<ITypeMapper>())
			.BuildServiceProvider();

	[Fact]
	public void ICacheService__Returns_NullCacheService()
	{
		// Arrange
		var provider = new UncachedServiceProvider(Setup(Substitute.For<ICacheService>()));

		// Act
		var result = provider.GetService(typeof(ICacheService));

		// Assert
		_ = Assert.IsType<NullCacheService>(result);
	}

	[Fact]
	public void IAdapter__Returns_App_Adapter_Type_Without_App_Cache()
	{
		// Arrange
		var cache = Substitute.For<ICacheService>();
		var app = Setup(cache);
		var provider = new UncachedServiceProvider(app);

		// Act
		var result = provider.GetService(typeof(IAdapter));

		// Assert
		var adapter = Assert.IsType<TestAdapter>(result);
		_ = Assert.IsType<NullCacheService>(adapter.Cache);
		Assert.Same(app.GetRequiredService<ITypeMapper>(), adapter.Mapper);
	}

	[Fact]
	public void Client_Created_Through_Provider__Gets_Uncached_Adapter()
	{
		// Arrange
		var provider = new UncachedServiceProvider(Setup(Substitute.For<ICacheService>()));

		// Act
		var result = ActivatorUtilities.CreateInstance<TestClient>(provider);

		// Assert
		var adapter = Assert.IsType<TestAdapter>(result.Adapter);
		_ = Assert.IsType<NullCacheService>(adapter.Cache);
	}

	[Fact]
	public void Other_Service__Returns_App_Service()
	{
		// Arrange
		var app = Setup(Substitute.For<ICacheService>());
		var provider = new UncachedServiceProvider(app);

		// Act
		var result = provider.GetService(typeof(ITypeMapper));

		// Assert
		Assert.Same(app.GetRequiredService<ITypeMapper>(), result);
	}

	public sealed class TestClient(IAdapter adapter)
	{
		public IAdapter Adapter { get; } = adapter;
	}

	public sealed class TestAdapter(ITypeMapper mapper, ICacheService cache) : IAdapter
	{
		public ITypeMapper Mapper { get; } = mapper;

		public ICacheService Cache { get; } = cache;

		public Task<Result<int>> ExecuteAsync(IDbConnection connection, IDbTransaction transaction, string query, object? param, CommandType type) =>
			throw new NotSupportedException();

		public Task<Result<T>> ExecuteAsync<T>(IDbConnection connection, IDbTransaction transaction, string query, object? param, CommandType type) =>
			throw new NotSupportedException();

		public Task<Result<IEnumerable<T>>> QueryAsync<T>(IDbConnection connection, IDbTransaction? transaction, string query, object? param, CommandType type) =>
			throw new NotSupportedException();

		public Task<Result<T>> QuerySingleAsync<T>(IDbConnection connection, IDbTransaction? transaction, string query, object? param, CommandType type) =>
			throw new NotSupportedException();
	}
}
