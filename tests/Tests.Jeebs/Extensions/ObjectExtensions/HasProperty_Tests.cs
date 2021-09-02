// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Reflection.ObjectExtensions_Tests
{
	public class HasProperty_Tests
	{
		[Fact]
		public void Is_Type_Returns_True_If_Type_Has_Property()
		{
			// Arrange
			var type = typeof(Test);

			// Act
			var result = type.HasProperty(nameof(Test.Foo));

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Is_Not_Type_Returns_True_If_Type_Has_Property()
		{
			// Arrange
			var test = new Test(F.Rnd.Str);

			// Act
			var result = test.HasProperty(nameof(Test.Foo));

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Is_Type_Returns_False_If_Type_Does_Not_Have_Property()
		{
			// Arrange
			var type = typeof(Test);

			// Act
			var result = type.HasProperty(F.Rnd.Str);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Is_Not_Type_Returns_False_If_Type_Does_Not_Have_Property()
		{
			// Arrange
			var test = new Test(F.Rnd.Str);

			// Act
			var result = test.HasProperty(F.Rnd.Str);

			// Assert
			Assert.False(result);
		}

		public sealed record class Test(string Foo);
	}
}
