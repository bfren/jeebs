// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;

namespace Jeebs.Reflection.LinqExtensions_Tests;

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
	public void Property_Does_Not_Exist_Returns_None()
	{
		// Arrange
		Expression<Func<Test, int>> expr = _ => Rnd.Int;

		// Act
		var result = expr.GetPropertyInfo();

		// Assert
		result.AssertNone();
	}

	public class Test
	{
		public int MyProperty { get; set; }
	}
}
