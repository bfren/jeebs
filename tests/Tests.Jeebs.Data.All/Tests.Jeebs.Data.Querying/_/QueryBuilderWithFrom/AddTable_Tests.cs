// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests
{
	public class AddTable_Tests
	{
		[Fact]
		public void Table_Not_Added_Adds_Table()
		{
			// Arrange
			var table = Substitute.For<ITable>();
			var builder = new QueryBuilderWithFrom(table);

			// Act
			builder.AddTable<TestTable>();

			// Assert
			Assert.Collection(builder.Tables,
				x => Assert.Same(table, x),
				x => Assert.IsType<TestTable>(x)
			);
		}

		[Fact]
		public void Table_Already_Added_Does_Nothing()
		{
			// Arrange
			var table = new TestTable();
			var builder = new QueryBuilderWithFrom(table);

			// Act
			builder.AddTable<TestTable>();

			// Assert
			Assert.Collection(builder.Tables,
				x => Assert.IsType<TestTable>(x)
			);
		}

		public sealed record TestTable() : Table(F.Rnd.Str);
	}
}
