// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.WordPress.Data.Enums;
using NSubstitute;
using Xunit;
using Base = Jeebs.WordPress.Data.Enums.Taxonomy_Tests.Parse_Tests;

namespace Jeebs.WordPress.Data.TypeHandlers.TaxonomyTypeHandler_Tests
{
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
}
