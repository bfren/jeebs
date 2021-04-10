// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.TaxonomyTypeHandler_Tests
{
	public class SetValue_Tests
	{
		public static TheoryData<Taxonomy, string> Sets_Value_To_Taxonomy_Name_Data =>
			new()
			{
				{ Taxonomy.Blank, string.Empty },
				{ Taxonomy.PostCategory, "category" },
				{ Taxonomy.LinkCategory, "link_category" },
				{ Taxonomy.NavMenu, "nav_menu" },
				{ Taxonomy.PostTag, "post_tag" }
			};

		[Theory]
		[MemberData(nameof(Sets_Value_To_Taxonomy_Name_Data))]
		public void Sets_Value_To_CommentType_Name(Taxonomy input, string expected)
		{
			// Arrange
			var handler = new TaxonomyTypeHandler();
			var parameter = Substitute.For<IDbDataParameter>();

			// Act
			handler.SetValue(parameter, input);

			// Assert
			parameter.Received().Value = expected;
		}
	}
}
