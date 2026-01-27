// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Term = Jeebs.WordPress.CustomFields.TermCustomField.Term;

namespace Jeebs.WordPress.CustomFields.TermCustomField_Tests;

public class GetValueAsString_Tests
{
	[Fact]
	public void Returns_Term_Title()
	{
		// Arrange
		var title = Rnd.Str;
		var term = new Term { Title = title };
		var field = Substitute.ForPartsOf<TermCustomField>(Rnd.Str);
		field.ValueObj.Returns(term);

		// Act
		var result = field.GetValueAsStringTest();

		// Assert
		Assert.Equal(title, result);
	}
}
