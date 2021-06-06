// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.TextCustomField.Msg;

namespace Jeebs.WordPress.Data.CustomFields.TextCustomField_Tests
{
	public class HydrateAsync_Tests
	{
		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public async Task Meta_Contains_Key_Sets_Value_Returns_True_Option(bool isRequired)
		{
			// Arrange
			var db = Substitute.For<IWpDb>();
			var key = F.Rnd.Str;
			var value = F.Rnd.Str;
			var meta = new MetaDictionary { { key, value } };
			var field = new Test(key);

			// Act
			var result = await field.HydrateAsync(db, meta, isRequired);

			// Assert
			result.AssertTrue();
			Assert.Equal(value, field.ValueObj);
		}

		[Fact]
		public async Task Meta_Does_Not_Contain_Key_IsRequired_True_Returns_None_With_MetaKeyNotFoundMsg()
		{
			// Arrange
			var db = Substitute.For<IWpDb>();
			var meta = new MetaDictionary { { F.Rnd.Str, F.Rnd.Str } };
			var key = F.Rnd.Str;
			var field = new Test(key);

			// Act
			var result = await field.HydrateAsync(db, meta, true);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<MetaKeyNotFoundMsg>(none);
			Assert.Equal(typeof(Test), msg.Type);
			Assert.Equal(key, msg.Value);
		}

		[Fact]
		public async Task Meta_Does_Not_Contain_Key_IsRequired_False_Returns_False_Option()
		{
			// Arrange
			var db = Substitute.For<IWpDb>();
			var meta = new MetaDictionary { { F.Rnd.Str, F.Rnd.Str } };
			var field = new Test(F.Rnd.Str);

			// Act
			var result = await field.HydrateAsync(db, meta, false);

			// Assert
			result.AssertFalse();
			Assert.Equal(string.Empty, field.ValueObj);
		}

		public record Test(string Key) : TextCustomField(Key);
	}
}
