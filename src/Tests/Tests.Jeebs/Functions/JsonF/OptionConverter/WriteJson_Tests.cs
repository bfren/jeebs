﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using Xunit;
using static F.OptionF;

namespace F.JsonF_Tests.OptionConverter_Tests
{
	public class WriteJson_Tests
	{
		[Fact]
		public void Serialise_Some_Returns_Some_Value_As_Json()
		{
			// Arrange
			var valueStr = Rnd.Str;
			var valueInt = Rnd.Int;
			var value = new Test(valueStr, valueInt);
			var option = Return(value);
			var json = $"{{\"foo\":\"{valueStr}\",\"bar\":{valueInt}}}";

			// Act
			var result = JsonF.Serialise(option);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(json, some);
		}

		[Fact]
		public void Serialise_None_Returns_Empty_Json()
		{
			// Arrange
			var option = Create.EmptyNone<int>();

			// Act
			var result = JsonF.Serialise(option);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(JsonF.Empty, some);
		}

		public record Test(string Foo, int Bar);
	}
}