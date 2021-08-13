// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.StrongId_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Id_Value()
		{
			// Arrange
			var value = F.Rnd.Ulng;
			var id = new Test(value);

			// Act
			var result = id.ToString();

			// Assert
			Assert.Equal(value.ToString(), result);
		}

		public record class Test(ulong Value) : StrongId(Value);
	}
}
