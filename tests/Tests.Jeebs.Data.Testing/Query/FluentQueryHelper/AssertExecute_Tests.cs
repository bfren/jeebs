// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;

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
	public async Task Incorrect_Method__Throws_MethodNameException()
	{
		// Arrange
		var fluent = Create();
		await fluent.CountAsync();

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, TestId>(c, x => x.Id, true);

		// Assert
		Assert.Throws<MethodNameException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public async Task Incorrect_Generic_Argument__Throws_GenericArgumentException()
	{
		// Arrange
		var fluent = Create();
		await fluent.ExecuteAsync(x => x.Id);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, string>(c, nameof(TestEntity.Id), false);

		// Assert
		Assert.Throws<GenericArgumentException>(() => fluent.AssertCalls(action));
	}

	[Fact]
	public async Task Not_Property_Expression__Throws_PropertyExpressionException()
	{
		// Arrange
		var fluent = Create();
		await fluent.ExecuteAsync<TestId>(nameof(TestEntity.Id));

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, TestId>(c, x => x.Id, false);

		// Assert
		Assert.Throws<PropertyExpressionException>(() => fluent.AssertCalls(action));
	}
}
