// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertCollection_Tests : Setup
{
	[Fact]
	public void Collection_And_Inspectors_Different_Lengths__Throws_CollectionException()
	{
		// Arrange
		var collection = Enumerable.Repeat(Rnd.Str, Rnd.Int).ToArray();
		var inspectors = Enumerable.Repeat((string _) => { }, Rnd.Int).ToArray();

		// Act
		var action = () => FluentQueryHelper.AssertCollection(collection, inspectors);

		// Assert
		Assert.Throws<CollectionException>(action);
	}

	[Fact]
	public void Inspector_Throws_FluentQueryHelperException__Catches_And_Rethrows()
	{
		// Arrange
		var collection = new[] { Rnd.Lng };
		var inspectors = new Action<long>[] { _ => throw new EqualTypeException() };

		// Act
		var action = () => FluentQueryHelper.AssertCollection(collection, inspectors);

		// Assert
		Assert.Throws<EqualTypeException>(action);
	}

	[Fact]
	public void Inspector_Throws_Exception__Catches_And_Throws_As_CollectionException()
	{
		// Arrange
		var collection = new[] { Rnd.Lng };
		var inspectors = new Action<long>[] { _ => throw new InvalidOperationException() };

		// Act
		var action = () => FluentQueryHelper.AssertCollection(collection, inspectors);

		// Assert
		Assert.Throws<CollectionException>(action);
	}
}
