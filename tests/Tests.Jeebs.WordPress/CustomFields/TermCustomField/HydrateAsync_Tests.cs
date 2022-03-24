// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.WordPress.Query;
using static Jeebs.WordPress.CustomFields.TermCustomField.M;
using Term = Jeebs.WordPress.CustomFields.TermCustomField.Term;

namespace Jeebs.WordPress.CustomFields.TermCustomField_Tests;

public class HydrateAsync_Tests
{
	[Fact]
	public async Task Meta_Does_Not_Contain_Key_IsRequired_True_Returns_None_With_MetaKeyNotFoundMsg()
	{
		// Arrange
		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();
		var meta = new MetaDictionary { { Rnd.Str, Rnd.Str } };
		var key = Rnd.Str;
		var field = new Test(Substitute.For<IQueryTerms>(), key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, true).ConfigureAwait(false);

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
		var unitOfWork = Substitute.For<IUnitOfWork>();
		var meta = new MetaDictionary { { Rnd.Str, Rnd.Str } };
		var field = new Test(Substitute.For<IQueryTerms>(), Rnd.Str);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, false).ConfigureAwait(false);

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

		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();

		var key = Rnd.Str;
		var value = Rnd.Lng;
		var meta = new MetaDictionary { { key, value.ToString() } };

		var queryTerms = Substitute.For<IQueryTerms>();
		queryTerms.ExecuteAsync<Term>(db, unitOfWork, Arg.Any<GetTermsOptions>()).Returns(terms);

		var field = new Test(queryTerms, key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, true).ConfigureAwait(false);

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

		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();

		var key = Rnd.Str;
		var meta = new MetaDictionary { { key, Rnd.Lng.ToString() } };

		var queryTerms = Substitute.For<IQueryTerms>();
		queryTerms.ExecuteAsync<Term>(db, unitOfWork, Arg.Any<GetTermsOptions>()).Returns(terms);

		var field = new Test(queryTerms, key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, true).ConfigureAwait(false);

		// Assert
		result.AssertTrue();
		Assert.Same(term, field.ValueObj);
	}

	public class Test : TermCustomField
	{
		public Test(IQueryTerms queryTerms, string key) : base(queryTerms, key) { }
	}
}
