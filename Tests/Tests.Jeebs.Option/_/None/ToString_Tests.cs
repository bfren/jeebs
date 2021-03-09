// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.None_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Has_Reason_Returns_Reason_ToString()
		{
			// Arrange
			var value = F.Rnd.Str;
			var msg = new TestMsg(value);
			var option = Option.None<int>().AddReason(msg);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public void Null_Reason_Returns_Type()
		{
			// Arrange
			var option = Option.None<int>();

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal("None: " + typeof(int).ToString(), result);
		}

		public record TestMsg(string Value) : IMsg
		{
			public override string ToString() =>
				Value;
		}
	}
}
