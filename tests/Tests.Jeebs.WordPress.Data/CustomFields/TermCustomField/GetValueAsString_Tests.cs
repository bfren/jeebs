// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.TermCustomField;

namespace Jeebs.WordPress.Data.CustomFields.TermCustomField_Tests;

public class GetValueAsString_Tests
{
	[Fact]
	public void Returns_Term_Title()
	{
		// Arrange
		var title = F.Rnd.Str;
		var term = new Term { Title = title };
		var field = Substitute.ForPartsOf<TermCustomField>(F.Rnd.Str);
		field.ValueObj.Returns(term);

		// Act
		var result = field.GetValueAsStringTest();

		// Assert
		Assert.Equal(title, result);
	}
}
