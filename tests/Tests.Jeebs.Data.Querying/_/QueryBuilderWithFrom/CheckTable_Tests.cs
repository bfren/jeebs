﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.Exceptions;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests;

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
		Assert.Throws<TestException<TestTable>>(action);
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

	public sealed record class TestTable() : Table(F.Rnd.Str);
}
