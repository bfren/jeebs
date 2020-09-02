﻿using System;
using System.Collections.Generic;
using System.Text;
using F;
using Xunit;

namespace Jeebs.Util.Json_Tests
{
	public class WriteJson_Tests
	{
		[Fact]
		public void Serialise_Enumerated_Returns_Json_Value()
		{
			// Arrange
			const string value = "18";
			var enumerated = new Test(value);

			// Act
			var result = JsonF.Serialise(enumerated);

			// Assert
			Assert.Equal($"\"{value}\"", result);
		}

		[Theory]
		[InlineData(null)]
		public void Serialise_Null_Enumerated_Returns_Empty_Json(Enumerated input)
		{
			// Arrange

			// Act
			var result = JsonF.Serialise(input);

			// Assert
			Assert.Equal(JsonF.Empty, result);
		}

		public class Test : Enumerated
		{
			public Test(string name) : base(name) { }
		}
	}
}
