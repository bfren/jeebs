using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.TypeExtensions_Tests
{
	public class ImplementsGenericClass_Tests
	{
		[Theory]
		[InlineData(typeof(Foo), typeof(DoesImplement<>))]
		[InlineData(typeof(Foo), typeof(DoesImplement<string>))]
		[InlineData(typeof(Foo<>), typeof(DoesImplement<>))]
		[InlineData(typeof(Foo<string>), typeof(DoesImplement<string>))]
		public void Does_Implement_Returns_True(Type @base, Type @class)
		{
			// Arrange

			// Act
			var result = @base.ImplementsGenericClass(@class);

			// Assert
			Assert.True(result);
		}

		[Theory]
		[InlineData(typeof(Foo), typeof(DoesNotImplement<>))]
		[InlineData(typeof(Foo<>), typeof(DoesNotImplement<>))]
		[InlineData(typeof(Foo<string>), typeof(DoesNotImplement<int>))]
		public void Does_Not_Implement_Returns_False(Type @base, Type @class)
		{
			// Arrange

			// Act
			var result = @base.ImplementsGenericClass(@class);

			// Assert
			Assert.False(result);
		}

		public class DoesImplement<T> { }
		public class DoesNotImplement<T> { }
		public class Foo : DoesImplement<string> { }
		public class Foo<T> : DoesImplement<T> { }
	}
}
