// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Xunit;
using static F.DataF.QueryF;

namespace F.DataF.QueryF_Tests
{
	public class GetParameterValue_Tests
	{
		[Fact]
		public void Not_StrongId_Returns_Original_Value()
		{
			// Arrange
			var value = Rnd.Str;

			// Act
			var result = GetParameterValue(value);

			// Assert
			Assert.Same(value, result);
		}

		[Fact]
		public void StrongId_Returns_StrongId_Value()
		{
			// Arrange
			var value = Rnd.Ulng;

			// Act
			var result = GetParameterValue(new TestId(value));

			// Assert
			Assert.Equal(value, result);
		}

		public sealed record TestId(ulong Value) : StrongId(Value);
	}
}
