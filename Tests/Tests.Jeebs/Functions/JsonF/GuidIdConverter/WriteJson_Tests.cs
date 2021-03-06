using System;
using Jeebs;
using Xunit;

namespace F.JsonF_Tests.GuidIdConverter_Tests
{
	public class WriteJson_Tests
	{
		[Fact]
		public void Serialise_Returns_Json_Value()
		{
			// Arrange
			var value = Guid.NewGuid();
			var id = new TestGuidId { Value = value };

			// Act
			var result = JsonF.Serialise(id);

			// Assert
			Assert.Equal($"\"{value}\"", result);
		}

		[Theory]
		[InlineData(null)]
		public void Serialise_Returns_Empty_Json(TestGuidId input)
		{
			// Arrange

			// Act
			var result = JsonF.Serialise(input);

			// Assert
			Assert.Equal(JsonF.Empty, result);
		}

		public record TestGuidId : GuidId { }
	}
}
