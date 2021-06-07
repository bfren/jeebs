// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.TermCustomField;
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

		[Fact]
		public async Task Meta_Contains_Key_Multiple_Terms_Found_Returns_None_With_MultipleTermsFoundMsg()
		{
			// Arrange
			var t0 = new Term();
			var t1 = new Term();
			var terms = new[] { t0, t1 };

			var query = Substitute.For<IWpDbQuery>();
			query.TermsAsync<Term>(Arg.Any<Query.GetTermsOptions>()).Returns(terms);

			var db = Substitute.For<IWpDb>();
			db.Query.Returns(query);

			var key = F.Rnd.Str;
			var value = F.Rnd.Lng;
			var meta = new MetaDictionary { { key, value.ToString() } };
			var field = new Test(key);

			// Act
			var result = await field.HydrateAsync(db, meta, true);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<MultipleTermsFoundMsg>(none);
			Assert.Equal(value.ToString(), msg.Value);
		}

		[Fact]
		public async Task Meta_Contains_Key_Single_Item_Found_Sets_ValueObj_Returns_True()
		{
			// Arrange
			var term = new Term();
			var terms = new[] { term };

			var query = Substitute.For<IWpDbQuery>();
			query.TermsAsync<Term>(Arg.Any<Query.GetTermsOptions>()).Returns(terms);

			var db = Substitute.For<IWpDb>();
			db.Query.Returns(query);

			var key = F.Rnd.Str;
			var value = F.Rnd.Lng;
			var meta = new MetaDictionary { { key, value.ToString() } };
			var field = new Test(key);

			// Act
			var result = await field.HydrateAsync(db, meta, true);

			// Assert
			result.AssertTrue();
			Assert.Same(term, field.ValueObj);

		}

		public record Test(string Key) : TermCustomField(Key);
	}
}
