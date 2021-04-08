// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
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
		[InlineData(typeof(FooLevel2), typeof(DoesImplement<>))]
		[InlineData(typeof(FooLevel2), typeof(DoesImplement<string>))]
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
		public class FooLevel2 : Foo { }
	}
}
