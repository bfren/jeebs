// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.Exceptions;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests
{
	public class Where_Tests
	{
		[Fact]
		public void Table_Not_Added_Throws_WhereTableNotAddedException()
		{
			// Arrange
			var table = Substitute.For<ITable>();
			var builder = new QueryBuilderWithFrom(table);

			// Act
			void action() => builder.Where<TestTable>(t => t.Foo, SearchOperator.Equal, F.Rnd.Str);

			// Assert
			Assert.Throws<WhereTableNotAddedException<TestTable>>(action);
		}

		[Fact]
		public void Adds_Predicate_To_Where_List()
		{
			// Arrange
			var table = new TestTable();
			var builder = new QueryBuilderWithFrom(table);
			var value = F.Rnd.Str;

			// Act
			builder.Where<TestTable>(t => t.Foo, SearchOperator.Like, value);

			// Assert
			Assert.Collection(builder.Parts.Where,
				x =>
				{
					Assert.Equal("TestTable", x.column.Table);
					Assert.Equal("TestFoo", x.column.Name);
					Assert.Equal("Foo", x.column.Alias);
					Assert.Equal(SearchOperator.Like, x.op);
					Assert.Equal(value, x.value);
				}
			);
		}

		public sealed record TestTable() : Table(nameof(TestTable))
		{
			public const string Prefix = "Test";

			public string Foo { get; set; } = Prefix + nameof(Foo);
		}
	}
}
