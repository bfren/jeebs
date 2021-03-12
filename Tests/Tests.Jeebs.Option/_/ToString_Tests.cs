// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using Xunit;

namespace JeebsF.Option_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Is_Some_With_Value_Returns_Value_ToString()
		{
			// Arrange
			var value = JeebsF.Rnd.Lng;
			var option = OptionF.Return(value);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal(value.ToString(), result);
		}

		[Fact]
		public void Is_Some_Value_Is_Null_Returns_Type()
		{
			// Arrange
			int? value = null;
			var option = OptionF.Return(value, true);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal("Some: " + typeof(int?).ToString(), result);
		}

		[Fact]
		public void Is_None_With_Reason_Returns_Reason_ToString()
		{
			// Arrange
			var value = JeebsF.Rnd.Str;
			var msg = new TestMsg(value);
			var option = OptionF.None<int>(msg);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal($"{nameof(TestMsg)}: {value}", result);
		}

		[Fact]
		public void Is_None_Without_Reason_Returns_Type()
		{
			// Arrange
			var option = OptionF.None<int>(true);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal("None: " + typeof(int).ToString(), result);
		}

		public record TestMsg(string Value) : IMsg
		{
			public override string ToString() =>
				$"{nameof(TestMsg)}: {Value}";
		}
	}
}
