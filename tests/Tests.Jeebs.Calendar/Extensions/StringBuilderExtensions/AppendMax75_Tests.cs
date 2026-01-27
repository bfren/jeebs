// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;

namespace Jeebs.Calendar.Extensions.StringBuilderExtensions_Tests;

public class AppendMax75_Tests
{
	[Theory]
	[InlineData(3)]
	[InlineData(37)]
	[InlineData(74)]
	public void Up_To_74_Characters_Appends_Text(int len)
	{
		// Arrange
		var sb = new StringBuilder();
		var text = Rnd.StringF.Get(len);

		// Act
		sb.AppendMax75(text);

		// Assert
		Assert.Equal(text + Environment.NewLine, sb.ToString());
	}

	[Fact]
	public void Over_74_Characters_Splits_Line()
	{
		// Arrange
		var sb = new StringBuilder();
		var text = Rnd.StringF.Get(200);
		var l0 = text[0..74];
		var l1 = text[74..148];
		var l2 = text[148..];
		var expected = $"{l0}{Environment.NewLine} {l1}{Environment.NewLine} {l2}{Environment.NewLine}";

		// Act
		sb.AppendMax75(text);

		// Assert
		Assert.Equal(expected, sb.ToString());
	}
}
