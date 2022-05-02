// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using NSubstitute.Core;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertSort_Tests : Setup
{
	[Theory]
	[InlineData(SortOrder.Ascending)]
	[InlineData(SortOrder.Descending)]
	public void Asserts_Sort(SortOrder input)
	{
		// Arrange
		var fluent = Create();
		fluent.Sort(x => x.Id, input);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertSort<TestEntity, TestId>(c, x => x.Id, input);

		// Assert
		fluent.AssertCalls(action);
	}

	[Theory]
	[InlineData(SortOrder.Ascending)]
	[InlineData(SortOrder.Descending)]
	public void Incorrect_Method__Throws_CollectionException(SortOrder input)
	{
		// Arrange
		var fluent = Create();
		fluent.Skip(Rnd.ULng);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertSort<TestEntity, TestId>(c, x => x.Id, input);

		// Assert
		var ex = Assert.Throws<CollectionException>(() => fluent.AssertCalls(action));
		Assert.Contains("Assert.Equal() Failure", ex.Message);
	}

	[Theory]
	[InlineData(SortOrder.Ascending)]
	[InlineData(SortOrder.Descending)]
	public void Incorrect_Generic_Argument__Throws_CollectionException(SortOrder input)
	{
		// Arrange
		var fluent = Create();
		fluent.Sort(x => x.Id, input);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, string>(c, nameof(TestEntity.Id), false);

		// Assert
		Assert.Throws<CollectionException>(() => fluent.AssertCalls(action));
	}

	[Theory]
	[InlineData(SortOrder.Ascending)]
	[InlineData(SortOrder.Descending)]
	public void Not_Property_Expression__Throws_CollectionException(SortOrder input)
	{
		// Arrange
		var fluent = Create();
		fluent.Sort(nameof(TestEntity.Id), input);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertExecute<TestEntity, TestId>(c, x => x.Id, false);

		// Assert
		Assert.Throws<CollectionException>(() => fluent.AssertCalls(action));
	}
}
