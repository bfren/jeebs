// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.TermCustomField;

namespace Jeebs.WordPress.Data.CustomFields.TermCustomField_Tests
{
	public class GetTerms_Tests
	{
		[Fact]
		public void Calls_Db_Query_TermsAsync_With_TermId()
		{
			// Arrange
			var query = Substitute.For<IWpDbQuery>();
			var schema = Substitute.For<IWpDbSchema>();
			var db = Substitute.For<IWpDb>();
			db.Query.Returns(query);
			var id = F.Rnd.Lng;

			// Act
			_ = GetTerms(db, new(id));

			// Assert
			db.Query.Received().TermsAsync<Term>(Arg.Is<Query.GetTermsOptions>(opt => opt(new(schema)).Id! == id));
		}
	}
}
