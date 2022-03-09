// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Random.Rnd_Tests.StringF_Tests;

public class Passphrase_Tests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(1)]
	public void NumberOfWords_Less_Than_Two_Returns_Null(int input)
	{
		// Arrange

		// Act
		var result = Rnd.StringF.Passphrase(input);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Empty_Word_List_Returns_Null()
	{
		// Arrange
		var empty = Array.Empty<string>();

		// Act
		var result = Rnd.StringF.Passphrase(empty, 3);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void NumberOfWords_Higher_Than_Word_List_Count_Returns_Null()
	{
		// Arrange

		// Act
		var result = Rnd.StringF.Passphrase(7777);

		// Assert
		Assert.Null(result);
	}

	[Theory]
	[InlineData(2)]
	[InlineData(4)]
	[InlineData(8)]
	public void Uses_Correct_Number_Of_Words(int input)
	{
		// Arrange
		const char sep = '|';

		// Act
		var result = Rnd.StringF.Passphrase(input, sep, Rnd.Flip, Rnd.Flip);

		// Assert
		var some = result!.Split(sep);
		Assert.Equal(input, some.Length);
	}

	[Theory]
	[InlineData('|')]
	[InlineData('^')]
	[InlineData('+')]
	public void Joins_Words_Using_Separator(char input)
	{
		// Arrange

		// Act
		var result = Rnd.StringF.Passphrase(3, input, Rnd.Flip, Rnd.Flip);

		// Assert
		Assert.Contains(input, result);
	}

	[Fact]
	public void UpperFirst_True_Makes_First_Letter_Uppercase()
	{
		// Arrange

		// Act
		var result = Rnd.StringF.Passphrase(3, '-', true, Rnd.Flip);

		// Assert
		Assert.NotEqual(result!, result!.ToLower());
	}

	[Fact]
	public void UpperFirst_False_Does_Note_Make_First_Letter_Uppercase()
	{
		// Arrange

		// Act
		var result = Rnd.StringF.Passphrase(3, '-', false, Rnd.Flip);

		// Assert
		Assert.Equal(result!, result!.ToLower());
	}

	[Fact]
	public void IncludeNumber_True_Includes_A_Number()
	{
		// Arrange

		// Act
		var result = Rnd.StringF.Passphrase(3, '-', Rnd.Flip, true);

		// Assert
		Assert.Contains(result!, x => char.IsNumber(x));
	}

	[Fact]
	public void IncludeNumber_False_Does_Note_Include_A_Number()
	{
		// Arrange

		// Act
		var result = Rnd.StringF.Passphrase(3, '-', Rnd.Flip, false);

		// Assert
		Assert.DoesNotContain(result!, x => char.IsNumber(x));
	}

	[Fact]
	public void Does_Not_Repeat_Words()
	{
		// Arrange
		const char sep = '|';

		// Act
		var result = Rnd.StringF.Passphrase(7776, sep, false, false);

		// Assert
		var some = result!.Split(sep);
		Assert.Equal(some.Length, some.Distinct().Count());
	}

	[Fact]
	public void Returns_Different_Phrase_Each_Time()
	{
		// Arrange
		const int iterations = 10000;
		var phrases = new List<string>();

		// Act
		for (int i = 0; i < iterations; i++)
		{
			phrases.Add(Rnd.StringF.Passphrase(2)!);
		}

		var unique = phrases.Distinct();

		// Assert
		Assert.InRange(unique.Count(), phrases.Count - 1, phrases.Count);
	}
}
