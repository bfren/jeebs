// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using NSubstitute.ExceptionExtensions;

namespace Jeebs.Data.Functions.DataF_Tests;

public class BuildQuery_Tests
{
	[Fact]
	public void Catches_Exception_Running_Builder_Returns_None_With_QueryBuilderExceptionMsg()
	{
		// Arrange
		var builder = Substitute.For<Func<IQueryBuilder, IQueryBuilderWithFrom>>();
		builder.Invoke(Arg.Any<IQueryBuilder>()).Throws<Exception>();

		// Act
		var result = DataF.BuildQuery<int>(builder);

		// Assert
		_ = result.AssertFailure("Error building query.");
	}
}
