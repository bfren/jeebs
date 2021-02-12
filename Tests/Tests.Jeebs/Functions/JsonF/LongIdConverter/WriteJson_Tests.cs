using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F.JsonF_Tests.LongIdConverter_Tests
{
	public class WriteJson_Tests
	{
		[Fact]
		public void Serialise_Returns_Json_Value()
		{
			// Arrange
			var value = Rnd.Lng;
			var id = new TestLongId { Value = value };

			// Act
			var result = JsonF.Serialise(id);

			// Assert
			Assert.Equal($"\"{value}\"", result);
		}

		[Theory]
		[InlineData(null)]
		public void Serialise_Returns_Empty_Json(TestLongId input)
		{
			// Arrange

			// Act
			var result = JsonF.Serialise(input);

			// Assert
			Assert.Equal(JsonF.Empty, result);
		}

		public record TestLongId : LongId { }
	}
}
