// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Linq;
using Xunit;
using static F.OptionF.Dictionary.Msg;

namespace Jeebs.DictionaryExtensions_Tests
{
	public class GetValueOrNone_Tests
	{
		[Fact]
		public void Empty_Dictionary_Returns_None_With_ListIsEmptyMsg()
		{
			// Arrange
			var dictionary = new Dictionary<int, string>();

			// Act
			var result = dictionary.GetValueOrNone(F.Rnd.Int);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<DictionaryIsEmptyMsg>(none.Reason);
		}

		[Theory]
		[InlineData(null)]
		public void Null_Key_Returns_None_With_KeyCannotBeNullMsg(string input)
		{
			// Arrange
			var dictionary = new Dictionary<string, int>
			{
				{ F.Rnd.Str, F.Rnd.Int }
			};

			// Act
			var result = dictionary.GetValueOrNone(input);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<KeyCannotBeNullMsg>(none.Reason);
		}

		[Fact]
		public void Key_Does_Not_Exists_Returns_None_With_KeyDoesNotExistMsg()
		{
			// Arrange
			var dictionary = new Dictionary<string, int>
			{
				{ F.Rnd.Str, F.Rnd.Int }
			};
			var key = F.Rnd.Str;

			// Act
			var result = dictionary.GetValueOrNone(key);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			var msg = Assert.IsType<KeyDoesNotExistMsg<string>>(none.Reason);
			Assert.Equal(key, msg.Key);
		}

		[Theory]
		[InlineData(null)]
		public void Null_Item_Returns_None_With_NullValueMsg(string input)
		{
			// Arrange
			var key = F.Rnd.Int;
			var dictionary = new Dictionary<int, string>
			{
				{ key, input }
			};

			// Act
			var result = dictionary.GetValueOrNone(key);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<NullValueMsg<int>>(none.Reason);
			Assert.Equal(key, msg.Key);
		}

		[Fact]
		public void Returns_Single_Element()
		{
			// Arrange
			var key = F.Rnd.Int;
			var value = F.Rnd.Str;
			var dictionary = new Dictionary<int, string>
			{
				{ key, value }
			};

			// Act
			var result = dictionary.GetValueOrNone(key);

			// Assert
			var some = Assert.IsType<Some<string>>(result);
			Assert.Equal(value, some.Value);
		}
	}
}
