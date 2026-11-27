// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Data.Adapters.Dapper.DapperAdapter_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Resolves_From_DI_Without_Mapper_Or_Cache()
	{
		// Arrange
		var provider = new ServiceCollection()
			.AddTransient<DapperAdapter>()
			.BuildServiceProvider();

		// Act
		var result = provider.GetRequiredService<DapperAdapter>();

		// Assert
		Assert.Same(DapperTypeMapper.Instance, result.Mapper);
	}

	[Fact]
	public void Resolves_From_DI_With_Mapper_And_Cache()
	{
		// Arrange
		var mapper = Substitute.For<ITypeMapper>();
		var provider = new ServiceCollection()
			.AddTransient<DapperAdapter>()
			.AddSingleton(mapper)
			.AddSingleton(Substitute.For<ICacheService>())
			.BuildServiceProvider();

		// Act
		var result = provider.GetRequiredService<DapperAdapter>();

		// Assert
		Assert.Same(mapper, result.Mapper);
	}

	[Fact]
	public void Resolves_From_DI_With_Mapper_Only__Uses_Mapper()
	{
		// Arrange
		var mapper = Substitute.For<ITypeMapper>();
		var provider = new ServiceCollection()
			.AddTransient<DapperAdapter>()
			.AddSingleton(mapper)
			.BuildServiceProvider();

		// Act
		var result = provider.GetRequiredService<DapperAdapter>();

		// Assert
		Assert.Same(mapper, result.Mapper);
	}

	[Fact]
	public async Task Resolves_From_DI_With_Cache_Only__Uses_Cache()
	{
		// Arrange
		var cache = Substitute.For<ICacheService>();
		var provider = new ServiceCollection()
			.AddTransient<DapperAdapter>()
			.AddSingleton(cache)
			.BuildServiceProvider();
		var connection = Substitute.For<IDbConnection>();
		_ = connection.ConnectionString.Returns(Rnd.Str);

		// Act
		var result = provider.GetRequiredService<DapperAdapter>();
		_ = await result.QueryAsync<string>(connection, null, Rnd.Str, null, CommandType.Text);

		// Assert
		Assert.Same(DapperTypeMapper.Instance, result.Mapper);
		_ = await cache.Received(1).GetOrCreateAsync(Arg.Any<string>(), Arg.Any<object?>(),
			Arg.Any<Func<Task<IEnumerable<string>>>>());
	}
}
