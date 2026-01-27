// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.WordPress.Enums;
using Base = Jeebs.WordPress.Enums.Taxonomy_Tests.Parse_Tests;

namespace Jeebs.WordPress.TypeHandlers.TaxonomyTypeHandler_Tests;

public class SetValue_Tests
{
	[Theory]
	[MemberData(nameof(Base.Returns_Correct_Taxonomy_Data), MemberType = typeof(Base))]
	public void Sets_Value_To_CommentType_Name(string expected, Taxonomy input)
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
