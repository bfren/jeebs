// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class GetHashCode_Tests
	{
		[Fact]
		public void If_Unknown_Option_Throws_UnknownOptionException()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			void action() => option.GetHashCode();

			// Assert
			Assert.Throws<UnknownOptionException>(action);
		}

		[Fact]
		public void Some_With_Same_Value_Generates_Same_HashCode()
		{
			// Arrange
			var value = F.Rnd.Str;
			var s0 = value.Return();
			var s1 = value.Return();

			// Act
			var h0 = s0.GetHashCode();
			var h1 = s1.GetHashCode();

			// Assert
			Assert.Equal(h0, h1);
		}

		[Fact]
		public void Some_With_Same_Type_And_Different_Value_Generates_Different_HashCode()
		{
			// Arrange
			var v0 = F.Rnd.Str;
			var v1 = F.Rnd.Str;
			var s0 = v0.Return();
			var s1 = v1.Return();

			// Act
			var h0 = s0.GetHashCode();
			var h1 = s1.GetHashCode();

			// Assert
			Assert.NotEqual(h0, h1);
		}

		[Fact]
		public void Some_With_Null_Value_And_Same_Type_Generates_Same_HashCode()
		{
			// Arrange
			string? v0 = null;
			string? v1 = null;
			var s0 = Return(v0, true);
			var s1 = Return(v1, true);

			// Act
			var h0 = s0.GetHashCode();
			var h1 = s1.GetHashCode();

			// Assert
			Assert.Equal(h0, h1);
		}

		[Fact]
		public void Some_With_Null_Value_And_Different_Type_Generates_Different_HashCode()
		{
			// Arrange
			string? v0 = null;
			int? v1 = null;
			var s0 = Return(v0, true);
			var s1 = Return(v1, true);

			// Act
			var h0 = s0.GetHashCode();
			var h1 = s1.GetHashCode();

			// Assert
			Assert.NotEqual(h0, h1);
		}

		[Fact]
		public void None_With_Same_Type_And_Same_Reason_Generates_Same_HashCode()
		{
			// Arrange
			var msg = Substitute.For<IMsg>();
			var n0 = None<int>(msg);
			var n1 = None<int>(msg);

			// Act
			var h0 = n0.GetHashCode();
			var h1 = n1.GetHashCode();

			// Assert
			Assert.Equal(h0, h1);
		}

		[Fact]
		public void None_With_Different_Type_And_Same_Reason_Generates_Different_HashCode()
		{
			// Arrange
			var msg = Substitute.For<IMsg>();
			var n0 = None<int>(msg);
			var n1 = None<string>(msg);

			// Act
			var h0 = n0.GetHashCode();
			var h1 = n1.GetHashCode();

			// Assert
			Assert.NotEqual(h0, h1);
		}

		[Fact]
		public void None_With_Same_Type_And_Different_Reason_Generates_Different_HashCode()
		{
			// Arrange
			var m0 = Substitute.For<IMsg>();
			var m1 = Substitute.For<IMsg>();
			var n0 = None<int>(m0);
			var n1 = None<int>(m1);

			// Act
			var h0 = n0.GetHashCode();
			var h1 = n1.GetHashCode();

			// Assert
			Assert.NotEqual(h0, h1);
		}

		public class FakeOption : Option<int> { }
	}
}
