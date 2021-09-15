// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Data.Querying;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static F.DataF.QueryBuilderF;
using static F.DataF.QueryBuilderF.Msg;

namespace F.DataF.QueryBuilderF_Tests;

public class Build_Tests
{
	[Fact]
	public void Catches_Exception_Running_Builder_Returns_None_With_QueryBuilderExceptionMsg()
	{
		// Arrange
		var builder = Substitute.For<Func<IQueryBuilder, IQueryBuilderWithFrom>>();
		builder.Invoke(Arg.Any<IQueryBuilder>()).Throws<Exception>();

		// Act
		var result = Build<int>(builder);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<QueryBuilderExceptionMsg>(none);
	}
}
