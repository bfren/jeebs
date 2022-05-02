// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using NSubstitute.Core;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertExecute_Tests : Setup
{
	[Fact]
	public async Task Asserts_ExecuteAsync()
	{
		// Arrange
		var fluent = Create();
		await fluent.ExecuteAsync(x => x.Id);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, TestId>(c, x => x.Id, false);

		// Assert
		fluent.AssertCalls(action);
	}

	[Fact]
	public async Task Asserts_ExecuteAsync__With_Transaction()
	{
		// Arrange
		var fluent = Create();
		var transaction = Substitute.For<IDbTransaction>();
		await fluent.ExecuteAsync(x => x.Id, transaction);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, TestId>(c, x => x.Id, true);

		// Assert
		fluent.AssertCalls(action);
	}

	[Fact]
	public async Task Incorrect_Method__Throws_CollectionException()
	{
		// Arrange
		var fluent = Create();
		await fluent.CountAsync();

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, TestId>(c, x => x.Id, true);

		// Assert
		var ex = Assert.Throws<CollectionException>(() => fluent.AssertCalls(action));
		Assert.Contains("Assert.Equal() Failure", ex.Message);
	}

	[Fact]
	public async Task Incorrect_Generic_Argument__Throws_CollectionException()
	{
		// Arrange
		var fluent = Create();
		await fluent.ExecuteAsync(x => x.Id);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, string>(c, nameof(TestEntity.Id), false);

		// Assert
		Assert.Throws<CollectionException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public async Task Not_Property_Expression__Throws_CollectionException()
	{
		// Arrange
		var fluent = Create();
		await fluent.ExecuteAsync<TestId>(nameof(TestEntity.Id));

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, TestId>(c, x => x.Id, false);

		// Assert
		Assert.Throws<CollectionException>(() => fluent.AssertCalls(action));
	}
}
