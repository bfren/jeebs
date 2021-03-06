using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.QueryPartsBuilder;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddWhere_Tests
	{
		[Fact]
		public void Null_List_Creates_New_List()
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();

			// Act
			builder.AddWhere(F.Rnd.Str);

			// Assert
			Assert.NotNull(builder.Parts.Where);
		}

		[Fact]
		public void Adds_Where_To_List()
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			var w0 = F.Rnd.Str;
			var w1 = F.Rnd.Str;
			var w2 = F.Rnd.Str;

			// Act
			builder.AddWhere(w0);
			builder.AddWhere(w1);
			builder.AddWhere(w2);

			// Assert
			Assert.Collection(builder.Parts.Where,
				x => Assert.Equal(w0, x),
				x => Assert.Equal(w1, x),
				x => Assert.Equal(w2, x)
			);
		}

		[Fact]
		public void Adds_Parameters_To_Dictionary()
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			var p0 = F.Rnd.Str;
			var p1 = F.Rnd.Str;
			var p2 = F.Rnd.Str;

			// Act
			builder.AddWhere(F.Rnd.Str, new { p0, p1 });
			builder.AddWhere(F.Rnd.Str, new { p2 });

			// Assert
			Assert.Collection(builder.Parts.Parameters,
				x => { Assert.Equal(nameof(p0), x.Key); Assert.Equal(p0, x.Value); },
				x => { Assert.Equal(nameof(p1), x.Key); Assert.Equal(p1, x.Value); },
				x => { Assert.Equal(nameof(p2), x.Key); Assert.Equal(p2, x.Value); }
			);
		}
	}
}
