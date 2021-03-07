// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerAdapter_Tests
{
	public class Retrieve_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_Table_Throws_InvalidOperationException(string input)
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			parts.From.Returns(input);

			// Act
			void action() => adapter.Retrieve(parts);

			// Assert
			var ex = Assert.Throws<InvalidOperationException>(action);
			Assert.Equal($"Table is invalid: '{input}'.", ex.Message);
		}

		[Fact]
		public void No_Select_Selects_All()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;
			parts.From.Returns(from);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from};", result);
		}

		[Theory]
		[InlineData("two, three")]
		[InlineData("`two`,`three`")]
		public void Selects_Columns(string input)
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;
			parts.From.Returns(from);
			parts.Select.Returns(input);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT {input} FROM {from};", result);
		}

		[Fact]
		public void Adds_Inner_Joins()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;

			var ij0_table = F.Rnd.Str;
			var ij0_on = F.Rnd.Str;
			var ij0_equals = F.Rnd.Str;
			var ij1_table = F.Rnd.Str;
			var ij1_on = F.Rnd.Str;
			var ij1_equals = F.Rnd.Str;

			var innerJoin = new List<(string, string, string)>
			{
				(ij0_table, ij0_on, ij0_equals),
				(ij1_table, ij1_on, ij1_equals)
			};

			parts.From.Returns(from);
			parts.InnerJoin.Returns(innerJoin);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from} " +
				$"INNER JOIN {ij0_table} ON {ij0_on} = {ij0_equals} " +
				$"INNER JOIN {ij1_table} ON {ij1_on} = {ij1_equals};", result);
		}

		[Fact]
		public void Adds_Left_Joins()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;

			var lj0_table = F.Rnd.Str;
			var lj0_on = F.Rnd.Str;
			var lj0_equals = F.Rnd.Str;
			var lj1_table = F.Rnd.Str;
			var lj1_on = F.Rnd.Str;
			var lj1_equals = F.Rnd.Str;

			var leftJoin = new List<(string, string, string)>
			{
				(lj0_table, lj0_on, lj0_equals),
				(lj1_table, lj1_on, lj1_equals)
			};

			parts.From.Returns(from);
			parts.LeftJoin.Returns(leftJoin);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from} " +
				$"LEFT JOIN {lj0_table} ON {lj0_on} = {lj0_equals} " +
				$"LEFT JOIN {lj1_table} ON {lj1_on} = {lj1_equals};", result);
		}

		[Fact]
		public void Adds_Right_Joins()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;

			var rj0_table = F.Rnd.Str;
			var rj0_on = F.Rnd.Str;
			var rj0_equals = F.Rnd.Str;
			var rj1_table = F.Rnd.Str;
			var rj1_on = F.Rnd.Str;
			var rj1_equals = F.Rnd.Str;

			var leftJoin = new List<(string, string, string)>
			{
				(rj0_table, rj0_on, rj0_equals),
				(rj1_table, rj1_on, rj1_equals)
			};

			parts.From.Returns(from);
			parts.RightJoin.Returns(leftJoin);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from} " +
				$"RIGHT JOIN {rj0_table} ON {rj0_on} = {rj0_equals} " +
				$"RIGHT JOIN {rj1_table} ON {rj1_on} = {rj1_equals};", result);
		}

		[Fact]
		public void Adds_Where()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;
			var w0 = F.Rnd.Str;
			var w1 = F.Rnd.Str;

			parts.From.Returns(from);
			parts.Where.Returns(new List<string> { w0, w1 });

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from} WHERE {w0} AND {w1};", result);
		}

		[Fact]
		public void Adds_OrderBy()
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;
			var ob0 = F.Rnd.Str;
			var ob1 = F.Rnd.Str;

			parts.From.Returns(from);
			parts.OrderBy.Returns(new List<string> { ob0, ob1 });

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from} ORDER BY {ob0}, {ob1};", result);
		}

		[Theory]
		[InlineData(-1, "")]
		[InlineData(0, "")]
		[InlineData(24, " LIMIT 24")]
		public void Adds_Limit(int limit, string expected)
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;

			parts.From.Returns(from);
			parts.Limit.Returns(limit);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from}{expected};", result);
		}

		[Theory]
		[InlineData(-1, "")]
		[InlineData(0, "")]
		[InlineData(24, "24, ")]
		public void Adds_Limit_And_Offset(int offset, string expected)
		{
			// Arrange
			var adapter = new SqlServerAdapter();
			var parts = Substitute.For<IQueryParts>();
			var from = F.Rnd.Str;
			var limit = F.Rnd.Int;

			parts.From.Returns(from);
			parts.Limit.Returns(limit);
			parts.Offset.Returns(offset);

			// Act
			var result = adapter.Retrieve(parts);

			// Assert
			Assert.Equal($"SELECT * FROM {from} LIMIT {expected}{limit};", result);
		}
	}
}
