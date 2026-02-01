// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Repository_Tests;

public class RetrieveAsync_Tests : Repository_Setup
{
	[Fact]
	public async Task Calls_Client_GetRetrieveQuery()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;

		// Act
		await repo.RetrieveAsync<FooModel>(FooId.Wrap(value));
		await repo.RetrieveAsync<FooModel>(FooId.Wrap(value), v.Transaction);

		// Assert
		v.Client.Received(2).GetRetrieveQuery<Foo, FooModel>(value);
	}

	[Fact]
	public async Task Calls_Db_QuerySingleAsync()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = IdGen.LongId<FooId>();

		// Act
		await repo.RetrieveAsync<FooModel>(value);
		await repo.RetrieveAsync<FooModel>(value, v.Transaction);

		// Assert
		await v.Db.Received(2).QuerySingleAsync<FooModel>(Arg.Any<string>(), Arg.Any<object?>(), CommandType.Text, v.Transaction);
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;

		// Act
		_ = await repo.RetrieveAsync<FooModel>(FooId.Wrap(value));
		_ = await repo.RetrieveAsync<FooModel>(FooId.Wrap(value), v.Transaction);

		// Assert
		v.Log.ReceivedWithAnyArgs(2).Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
