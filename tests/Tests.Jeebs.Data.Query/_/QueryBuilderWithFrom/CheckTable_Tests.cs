// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.Query.Exceptions;

namespace Jeebs.Data.Query.QueryBuilderWithFrom_Tests;

public class CheckTable_Tests
{
	[Fact]
	public void Table_Not_Added_Throws_Exception()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		var action = void () => builder.CheckTable<TestTable, TestException<TestTable>>();

		// Assert
		_ = Assert.Throws<TestException<TestTable>>(action);
	}

	[Fact]
	public void Table_Added_Does_Nothing()
	{
		// Arrange
		var table = new TestTable();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		var action = bool () =>
		{
			builder.CheckTable<TestTable, TestException<TestTable>>();
			return true;
		};

		// Assert
		Assert.True(action());
	}

	public sealed class TestException<T> : QueryBuilderException<T>
		where T : ITable
	{
		public TestException() { }

		public TestException(string message) : base(message) { }

		public TestException(string message, Exception inner) : base(message, inner) { }
	}

	public sealed record class TestTable() : Table(Rnd.Str);
}
