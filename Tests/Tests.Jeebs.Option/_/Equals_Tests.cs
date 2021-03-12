// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class Equals_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData(56897)]
		[InlineData("ransfl39vdv")]
		[InlineData(true)]
		public void When_Other_Is_Not_Option_Returns_False(object? other)
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = option.Equals(other);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Some_Compares_Values()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v0);
			var o2 = Return(v1);

			// Act
			var r0 = o0.Equals(o1);
			var r1 = o1.Equals(o2);

			// Assert
			Assert.True(r0);
			Assert.False(r1);
		}

		[Fact]
		public void None_Compares_Reasons()
		{
			// Arrange
			var m0 = new TestMsg0();
			var m1 = new TestMsg1();
			var o0 = None<int>(m0);
			var o1 = None<int>(m0);
			var o2 = None<int>(m1);

			// Act
			var r0 = o0.Equals(o1);
			var r1 = o1.Equals(o2);

			// Assert
			Assert.True(r0);
			Assert.False(r1);
		}

		[Fact]
		public void Mixed_Returns_False()
		{
			// Arrange
			var o0 = Return(F.Rnd.Int);
			var o1 = None<int>(true);

			// Act
			var r0 = o0.Equals(o1);
			var r1 = o1.Equals(o0);

			// Assert
			Assert.False(r0);
			Assert.False(r1);
		}

		public class FakeOption : Option<int> { }

		public class TestMsg0 : IMsg { }

		public class TestMsg1 : IMsg { }
	}
}
