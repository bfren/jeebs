// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.TermCustomField.Msg;

namespace Jeebs.WordPress.Data.CustomFields.TermCustomField_Tests
{
	public class HydrateAsync_Tests
	{
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
		}

		public record Test(string Key) : TermCustomField(Key);
	}
}
