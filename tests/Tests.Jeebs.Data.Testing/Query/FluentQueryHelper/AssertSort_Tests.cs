// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Testing.Exceptions;
using NSubstitute.Core;

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
	public void Incorrect_Method__Throws_MethodNameException(SortOrder input)
	{
		// Arrange
		var fluent = Create();
		fluent.Skip(Rnd.ULng);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertSort<TestEntity, TestId>(c, x => x.Id, input);

		// Assert
		Assert.Throws<MethodNameException>(() => fluent.AssertCalls(action));
	}

	[Theory]
	[InlineData(SortOrder.Ascending)]
	[InlineData(SortOrder.Descending)]
	public void Incorrect_Generic_Argument__Throws_GenericArgumentException(SortOrder input)
	{
		// Arrange
		var fluent = Create();
		fluent.Sort(x => x.Id, input);

		// Act
		var action = (ICall c) => FluentQueryHelper.AssertSort<TestEntity, string>(c, nameof(TestEntity.Id), input);

		// Assert
		Assert.Throws<GenericArgumentException>(() => fluent.AssertCalls(action));
	}
}
