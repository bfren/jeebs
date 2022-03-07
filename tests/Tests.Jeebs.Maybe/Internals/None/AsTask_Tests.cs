// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Internals.None_Tests;

public class AsTask_Tests
{
	[Fact]
	public void Returns_None_As_Generic_Option()
	{
		// Arrange
		var none = Create.None<int>();

		// Act
		var result = none.AsTask;

		// Assert
		_ = Assert.IsType<Task<Maybe<int>>>(result);
	}
}
