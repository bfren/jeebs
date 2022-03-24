﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute.ExceptionExtensions;
using static Jeebs.Data.Query.Functions.QueryBuilderF.M;

namespace Jeebs.Data.Query.Functions.QueryBuilderF_Tests;

public class Build_Tests
{
	[Fact]
	public void Catches_Exception_Running_Builder_Returns_None_With_QueryBuilderExceptionMsg()
	{
		// Arrange
		var builder = Substitute.For<Func<IQueryBuilder, IQueryBuilderWithFrom>>();
		builder.Invoke(Arg.Any<IQueryBuilder>()).Throws<Exception>();

		// Act
		var result = QueryBuilderF.Build<int>(builder);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<QueryBuilderExceptionMsg>(none);
	}
}
