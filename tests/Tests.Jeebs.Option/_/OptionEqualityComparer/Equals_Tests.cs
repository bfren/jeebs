// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.OptionF;

namespace Jeebs.OptionEqualityComparer_Tests
{
	public class Equals_Tests
	{
		[Fact]
		public void When_One_Is_Unknown_Option_Returns_False()
		{
			// Arrange
			var fake = new FakeOption();
			var option = Some(F.Rnd.Int);
			var comparer = new OptionEqualityComparer<int>();

			// Act
			var r0 = comparer.Equals(fake, option);
			var r1 = comparer.Equals(option, fake);

			// Assert
			Assert.False(r0);
			Assert.False(r1);
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
			var comparer = new OptionEqualityComparer<int>();

			// Act
			var r0 = comparer.Equals(o0, o1);
			var r1 = comparer.Equals(o1, o2);

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
			var comparer = new OptionEqualityComparer<int>();

			// Act
			var r0 = comparer.Equals(o0, o1);
			var r1 = comparer.Equals(o1, o2);

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
			var comparer = new OptionEqualityComparer<int>();

			// Act
			var r0 = comparer.Equals(o0, o1);
			var r1 = comparer.Equals(o1, o0);

			// Assert
			Assert.False(r0);
			Assert.False(r1);
		}

		public record class FakeOption : Option<int> { }

		public record class TestMsg0 : IMsg { }

		public record class TestMsg1 : IMsg { }
	}
}
