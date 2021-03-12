// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			builder.AddWhere(JeebsF.Rnd.Str);

			// Assert
			Assert.NotNull(builder.Parts.Where);
		}

		[Fact]
		public void Adds_Where_To_List()
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			var w0 = JeebsF.Rnd.Str;
			var w1 = JeebsF.Rnd.Str;
			var w2 = JeebsF.Rnd.Str;

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
			var p0 = JeebsF.Rnd.Str;
			var p1 = JeebsF.Rnd.Str;
			var p2 = JeebsF.Rnd.Str;

			// Act
			builder.AddWhere(JeebsF.Rnd.Str, new { p0, p1 });
			builder.AddWhere(JeebsF.Rnd.Str, new { p2 });

			// Assert
			Assert.Collection(builder.Parts.Parameters,
				x => { Assert.Equal(nameof(p0), x.Key); Assert.Equal(p0, x.Value); },
				x => { Assert.Equal(nameof(p1), x.Key); Assert.Equal(p1, x.Value); },
				x => { Assert.Equal(nameof(p2), x.Key); Assert.Equal(p2, x.Value); }
			);
		}
	}
}
