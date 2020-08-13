using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public sealed class PropertyInfo_Tests
	{
		[Fact]
		public void Create_WithInvalidPropertyName_ThrowsInvalidOperationException()
		{
			// Arrange

			// Act
			Action result = () => new PropertyInfo<Foo, object>("does_not_exist");

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Fact]
		public void Create_WithInvalidPropertyType_ThrowsInvalidOperationException()
		{
			// Arrange

			// Act
			Action result = () => new PropertyInfo<Foo, int>(nameof(Foo.Bar));

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Fact]
		public void Get_WithPropertySet_ReturnsValue()
		{
			// Arrange
			var foo = new Foo { Bar = "value" };
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
			var result = info.Get(foo);

			// Assert
			Assert.Equal(foo.Bar, result);
		}

		[Fact]
		public void Get_WithPropertyNotSet_ThrowsInvalidOperationException()
		{
			// Arrange
			var foo = new Foo();
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
			Action result = () => info.Get(foo);

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Fact]
		public void Get_FromNullObject_ThrowsArgumentNullException()
		{
			// Arrange
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			Action result = () => info.Get(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void Set_WithNullObject_ThrowsArgumentNullException()
		{
			// Arrange
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			Action result = () => info.Set(null, "value");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void Set_WithNullValue_ThrowsArgumentNullException()
		{
			// Arrange
			var foo = new Foo();
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			Action result = () => info.Set(foo, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

			// Assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void Set_WithEverythingValid_ChangesValue()
		{
			// Arrange
			var foo = new Foo { Bar = "value" };
			const string newValue = "changed_value";
			var info = new PropertyInfo<Foo, string>(nameof(Foo.Bar));

			// Act
			info.Set(foo, newValue);

			// Assert
			Assert.Equal(newValue, foo.Bar);
		}

		public sealed class Foo
		{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
			public string Bar { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		}
	}
}
