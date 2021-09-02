// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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
			var o0 = Some(v0);
			var o1 = Some(v0);
			var o2 = Some(v1);

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
			var o0 = Some(F.Rnd.Int);
			var o1 = Create.None<int>();

			// Act
			var r0 = o0.Equals(o1);
			var r1 = o1.Equals(o0);

			// Assert
			Assert.False(r0);
			Assert.False(r1);
		}

		public record class FakeOption : Option<int> { }

		public record class TestMsg0 : IMsg { }

		public record class TestMsg1 : IMsg { }
	}
}
