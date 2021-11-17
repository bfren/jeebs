// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Linq;
using Xunit;
using static Jeebs.Linq.LinqExpressionExtensions.M;

namespace Jeebs.LinqExtensions_Tests;

public class GetPropertyInfo_Tests
{
	[Fact]
	public void Property_Exists_Returns_Some_With_PropertyInfo()
	{
		// Arrange
		Expression<Func<Test, int>> expr = t => t.MyProperty;

		// Act
		var result = expr.GetPropertyInfo();

		// Assert
		var some = result.AssertSome();
		Assert.IsType<PropertyInfo<Test, int>>(some);
	}

	[Fact]
	public void Property_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg()
	{
		// Arrange
		Expression<Func<Test, int>> expr = _ => F.Rnd.Int;

		// Act
		var result = expr.GetPropertyInfo();

		// Assert
		var none = result.AssertNone();
		Assert.IsType<PropertyDoesNotExistOnTypeMsg<Test>>(none);
	}

	public class Test
	{
		public int MyProperty { get; set; }
	}
}
