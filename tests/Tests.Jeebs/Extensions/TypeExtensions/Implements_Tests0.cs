// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Logging;
using Xunit;

namespace Jeebs.TypeExtensions_Tests
{
	public partial class Implements_Tests
	{
		[Fact]
		public void With_Generic_Base_Is_Object_Type_Returns_False()
		{
			// Arrange
			var @base = typeof(object);

			// Act
			var result = @base.Implements<Foo>();

			// Assert
			Assert.False(result);
		}

		[Theory]
		[InlineData(typeof(Foo))]
		[InlineData(typeof(IDoesImplement))]
		public void Identical_Types_Returns_False(Type type)
		{
			// Arrange

			// Act
			var result = type.Implements(type);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void With_Generic_Base_Is_Value_Type_Returns_False()
		{
			// Arrange
			var b0 = typeof(bool);
			var b1 = typeof(int);
			var b2 = typeof(long);
			var b3 = typeof(float);
			var b4 = typeof(double);
			var b5 = typeof(char);
			var b6 = typeof(LogLevel);

			// Act
			var r0 = b0.Implements<Foo>();
			var r1 = b1.Implements<Foo>();
			var r2 = b2.Implements<Foo>();
			var r3 = b3.Implements<Foo>();
			var r4 = b4.Implements<Foo>();
			var r5 = b5.Implements<Foo>();
			var r6 = b6.Implements<Foo>();

			// Assert
			Assert.False(r0);
			Assert.False(r1);
			Assert.False(r2);
			Assert.False(r3);
			Assert.False(r4);
			Assert.False(r5);
			Assert.False(r6);
		}

		[Fact]
		public void With_Generic_Base_Is_SubclassOf_Type_Returns_True()
		{
			// Arrange
			var @base = typeof(Foo);

			// Act
			var result = @base.Implements<DoesImplement>();

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void With_Generic_Interface_Is_AssignableFrom_Base_Returns_True()
		{
			// Arrange
			var @base = typeof(Foo);

			// Act
			var result = @base.Implements<IDoesImplement>();

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void With_Generic_Base_Does_Implement_Generic_Returns_True()
		{
			// Arrange
			var b0 = typeof(Foo);
			var b1 = typeof(Foo<string>);

			// Act
			var r0 = b0.Implements<IDoesImplement<string>>();
			var r1 = b1.Implements<IDoesImplement<string>>();
			var r2 = b0.Implements<DoesImplement<string>>();
			var r3 = b1.Implements<DoesImplement<string>>();

			// Assert
			Assert.True(r0);
			Assert.True(r1);
			Assert.True(r2);
			Assert.True(r3);
		}

		[Fact]
		public void With_Generic_Base_Does_Not_Implement_Generic_Returns_False()
		{
			// Arrange
			var b0 = typeof(Foo);
			var b1 = typeof(Foo<string>);

			// Act
			var r0 = b0.Implements<IDoesNotImplement<string>>();
			var r1 = b1.Implements<IDoesNotImplement<string>>();
			var r2 = b0.Implements<DoesNotImplement<string>>();
			var r3 = b1.Implements<DoesNotImplement<string>>();

			// Assert
			Assert.False(r0);
			Assert.False(r1);
			Assert.False(r2);
			Assert.False(r3);
		}

		[Fact]
		public void With_Generic_Base_Does_Not_Implement_Type_Returns_False()
		{
			// Arrange
			var @base = typeof(Foo);

			// Act
			var result = @base.Implements<DoesNotImplement>();

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void With_Generic_Interface_Is_Not_AssignableFrom_Base_Returns_False()
		{
			// Arrange
			var @base = typeof(Foo);

			// Act
			var result = @base.Implements<IDoesNotImplement>();

			// Assert
			Assert.False(result);
		}

		public interface IDoesImplement { }
		public interface IDoesNotImplement { }
		public interface IDoesImplement<T> : IDoesImplement { }
		public interface IDoesNotImplement<T> { }

		public class DoesImplement { }
		public class DoesNotImplement { }
		public class DoesImplement<T> : DoesImplement { }
		public class DoesNotImplement<T> { }
		public class Foo : DoesImplement<string>, IDoesImplement, IDoesImplement<string> { }
		public class Foo<T> : DoesImplement<T>, IDoesImplement<T> { }
	}
}
