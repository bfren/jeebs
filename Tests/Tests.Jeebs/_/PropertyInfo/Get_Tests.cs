// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.PropertyInfo_Tests
{
	public class Get_Tests
	{
		[Fact]
		public void WithPropertySet_ReturnsValue()
		{
			// Arrange
			var foo = new Foo { Bar = JeebsF.Rnd.Str };
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
			var result = info.Get(foo);

			// Assert
			Assert.Equal(foo.Bar, result);
		}

		[Fact]
		public void WithPropertyNotSet_ThrowsInvalidOperationException()
		{
			// Arrange
			var foo = new Foo();
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
			Action result = () => info.Get(foo);

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Theory]
		[InlineData(null)]
		public void FromNullObject_ThrowsArgumentNullException(Foo obj)
		{
			// Arrange
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
			Action result = () => info.Get(obj);

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		public sealed class Foo
		{
			public string? Bar { get; set; }
		}
	}
}
