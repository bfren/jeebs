// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Xunit;

namespace F.JsonF_Tests.StrongIdConverter_Tests
{
	public class WriteJson_Tests
	{
		[Fact]
		public void Serialise_Returns_Json_Value()
		{
			// Arrange
			var value = Rnd.Lng;
			var id = new TestId { Value = value };

			// Act
			var result = JsonF.Serialise(id);

			// Assert
			Assert.Equal($"\"{value}\"", result);
		}

		[Theory]
		[InlineData(null)]
		public void Serialise_Returns_Empty_Json(TestId input)
		{
			// Arrange

			// Act
			var result = JsonF.Serialise(input);

			// Assert
			Assert.Equal(JsonF.Empty, result);
		}

		public record TestId() : StrongId(0) { }
	}
}
