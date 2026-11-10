// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;

namespace Jeebs.Calendar.StringBuilderExtensions_Tests;

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
		Assert.Equal(text + "\r\n", sb.ToString());
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
		var expected = $"{l0}\r\n {l1}\r\n {l2}\r\n";

		// Act
		sb.AppendMax75(text);

		// Assert
		Assert.Equal(expected, sb.ToString());
	}

	[Fact]
	public void Accented_Text__Folds_At_74_Bytes()
	{
		// Arrange
		var sb = new StringBuilder();
		var text = new string('é', 50); // 2 bytes each

		// Act
		sb.AppendMax75(text);

		// Assert
		Assert.Equal($"{new string('é', 37)}\r\n {new string('é', 13)}\r\n", sb.ToString());
	}

	[Fact]
	public void Emoji__Is_Not_Split()
	{
		// Arrange
		var sb = new StringBuilder();
		var text = new string('a', 72) + "😀"; // 72 bytes + 4 bytes

		// Act
		sb.AppendMax75(text);

		// Assert
		Assert.Equal($"{new string('a', 72)}\r\n 😀\r\n", sb.ToString());
	}

	[Fact]
	public void Every_Line_Is_At_Most_75_Bytes()
	{
		// Arrange
		var sb = new StringBuilder();
		var text = string.Concat(Enumerable.Repeat("aé😀", 100));

		// Act
		sb.AppendMax75(text);

		// Assert
		var lines = sb.ToString().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
		Assert.All(lines, x => Assert.True(Encoding.UTF8.GetByteCount(x) <= 75));
		Assert.Equal(text, string.Concat(lines.Select((x, i) => i == 0 ? x : x[1..])));
	}
}
