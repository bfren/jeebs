// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs;
using Xunit;
using static F.OptionF.Dictionary.Msg;

namespace Jeebs_Tests.Dictionary
{
	public abstract class GetValueOrNone_Tests
	{
		public abstract void Test00_Empty_Dictionary_Returns_None_With_ListIsEmptyMsg();

		protected static void Test00(Func<IDictionary<string, int>, string, Option<int>> act)
		{
			// Arrange
			var dictionary = new Dictionary<string, int>();

			// Act
			var result = act(dictionary, F.Rnd.Str);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<DictionaryIsEmptyMsg>(none);
		}

		public abstract void Test01_Null_Key_Returns_None_With_KeyCannotBeNullMsg(string input);

		protected static void Test01(Func<IDictionary<string, int>, Option<int>> act)
		{
			// Arrange
			var dictionary = new Dictionary<string, int>
			{
				{ F.Rnd.Str, F.Rnd.Int }
			};

			// Act
			var result = act(dictionary);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<KeyCannotBeNullMsg>(none);
		}

		public abstract void Test02_Key_Does_Not_Exists_Returns_None_With_KeyDoesNotExistMsg();

		protected static void Test02(Func<IDictionary<string, int>, string, Option<int>> act)
		{
			// Arrange
			var dictionary = new Dictionary<string, int>
			{
				{ F.Rnd.Str, F.Rnd.Int }
			};
			var key = F.Rnd.Str;

			// Act
			var result = act(dictionary, key);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<KeyDoesNotExistMsg<string>>(none);
			Assert.Equal(key, msg.Key);
		}

		public abstract void Test03_Key_Exists_Null_Item_Returns_None_With_NullValueMsg();

		protected static void Test03(Func<IDictionary<int, string>, int, Option<string>> act)
		{
			// Arrange
			var key = F.Rnd.Int;
			var dictionary = new Dictionary<int, string>
			{
				{ key, null! }
			};

			// Act
			var result = act(dictionary, key);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<NullValueMsg<int>>(none);
			Assert.Equal(key, msg.Key);
		}

		public abstract void Test04_Key_Exists_Valid_Item_Returns_Some_With_Value();

		protected static void Test04(Func<IDictionary<int, string>, int, Option<string>> act)
		{
			// Arrange
			var key = F.Rnd.Int;
			var value = F.Rnd.Str;
			var dictionary = new Dictionary<int, string>
			{
				{ key, value }
			};

			// Act
			var result = act(dictionary, key);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}
	}
}
