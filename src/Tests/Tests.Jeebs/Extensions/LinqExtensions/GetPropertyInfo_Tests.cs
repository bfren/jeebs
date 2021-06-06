// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Linq.Expressions;
using Jeebs.Linq;
using Xunit;

namespace Jeebs.LinqExtensions_Tests
{
	public class GetPropertyInfo_Tests
	{
		[Fact]
		public void Returns_PropertyInfo()
		{
			// Arrange
			Expression<Func<Test, int>> expr = t => t.MyProperty;

			// Act
			var result = expr.GetPropertyInfo();

			// Assert
			var some = result.AssertSome();
			Assert.IsType<PropertyInfo<Test, int>>(some);
		}

		public class Test
		{
			public int MyProperty { get; set; }
		}
	}
}
