using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.QueryPartsBuilder;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddLimitAndOffset_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData(0)]
		[InlineData(10)]
		public void Sets_Parts_Limit(long? input)
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			builder.Parts.Limit = F.Rnd.Int;
			var options = new Options { Limit = input };

			// Act
			builder.AddLimitAndOffset(options);

			// Assert
			Assert.Equal(input, builder.Parts.Limit);
		}

		[Theory]
		[InlineData(null)]
		[InlineData(0)]
		[InlineData(10)]
		public void Sets_Parts_Offset(long? input)
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			builder.Parts.Limit = F.Rnd.Int;
			var options = new Options { Offset = input };

			// Act
			builder.AddLimitAndOffset(options);

			// Assert
			Assert.Equal(input, builder.Parts.Offset);
		}
	}
}
