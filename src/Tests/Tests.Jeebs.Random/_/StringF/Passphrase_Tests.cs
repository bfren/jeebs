// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs;
using Xunit;
using static F.Rnd.StringF;
using static F.Rnd.StringF.Msg;

namespace F.StringF_Tests
{
	public class Passphrase_Tests
	{
		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1)]
		public void NumberOfWords_Less_Than_Two_Returns_None_With_NumberOfWordsMustBeAtLeastTwoMsg(int input)
		{
			// Arrange

			// Act
			var result = Passphrase(input);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<NumberOfWordsMustBeAtLeastTwoMsg>(none);
		}

		[Fact]
		public void Empty_Word_List_Returns_None_With_EmptyWordListMsg()
		{
			// Arrange
			var empty = Array.Empty<string>();

			// Act
			var result = Passphrase(empty, 3);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<EmptyWordListMsg>(none);
		}

		[Fact]
		public void NumberOfWords_Higher_Than_Word_List_Count_Returns_None_With_NumberOfWordsCannotBeMoreThanWordListMsg()
		{
			// Arrange

			// Act
			var result = Passphrase(7777);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<NumberOfWordsCannotBeMoreThanWordListMsg>(none);
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
			var result = Passphrase(input, separator: sep);

			// Assert
			var some = result.AssertSome().Split(sep);
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
			var result = Passphrase(3, separator: input);

			// Assert
			var some = result.AssertSome();
			Assert.Contains(input, some);
		}

		[Fact]
		public void UpperFirst_True_Makes_First_Letter_Uppercase()
		{
			// Arrange

			// Act
			var result = Passphrase(3, upperFirst: true);

			// Assert
			var some = result.AssertSome();
			Assert.NotEqual(some, some.ToLower());
		}

		[Fact]
		public void UpperFirst_False_Does_Note_Make_First_Letter_Uppercase()
		{
			// Arrange

			// Act
			var result = Passphrase(3, upperFirst: false);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(some, some.ToLower());
		}

		[Fact]
		public void IncludeNumber_True_Includes_A_Number()
		{
			// Arrange

			// Act
			var result = Passphrase(3, includeNumber: true);

			// Assert
			var some = result.AssertSome();
			Assert.Contains(some, x => char.IsNumber(x));
		}

		[Fact]
		public void IncludeNumber_False_Does_Note_Include_A_Number()
		{
			// Arrange

			// Act
			var result = Passphrase(3, includeNumber: false);

			// Assert
			var some = result.AssertSome();
			Assert.DoesNotContain(some, x => char.IsNumber(x));
		}

		[Fact]
		public void Does_Not_Repeat_Words()
		{
			// Arrange
			const char sep = '|';

			// Act
			var result = Passphrase(7776, separator: sep, upperFirst: false, includeNumber: false);

			// Assert
			var some = result.AssertSome().Split(sep);
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
				phrases.Add(Passphrase(2).UnsafeUnwrap());
			}

			var unique = phrases.Distinct();

			// Assert
			Assert.Equal(unique.Count(), phrases.Count);
		}
	}
}
