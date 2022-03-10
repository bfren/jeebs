// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;

namespace Jeebs.Data.Query.QueryParts_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_From_Table()
	{
		// Arrange
		var table = Substitute.For<ITable>();

		// Act
		var result = new QueryParts(table);

		// Assert
		Assert.Same(table, result.From);
	}

	[Fact]
	public void Copies_All_Values()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var parts = new QueryParts(table)
		{
			SelectCount = Rnd.Flip,
			SelectColumns = Substitute.For<IColumnList>(),
			InnerJoin = Substitute.For<IImmutableList<(IColumn, IColumn)>>(),
			LeftJoin = Substitute.For<IImmutableList<(IColumn, IColumn)>>(),
			RightJoin = Substitute.For<IImmutableList<(IColumn, IColumn)>>(),
			Where = Substitute.For<IImmutableList<(IColumn, Compare, object)>>(),
			WhereCustom = Substitute.For<IImmutableList<(string, IQueryParametersDictionary)>>(),
			Sort = Substitute.For<IImmutableList<(IColumn, SortOrder)>>(),
			SortRandom = Rnd.Flip,
			Maximum = Rnd.Ulng,
			Skip = Rnd.Ulng
		};

		// Act
		var result = new QueryParts(parts);

		// Assert
		Assert.Equal(parts.SelectCount, result.SelectCount);
		Assert.Same(parts.From, result.From);
		Assert.Same(parts.SelectColumns, result.SelectColumns);
		Assert.Same(parts.InnerJoin, result.InnerJoin);
		Assert.Same(parts.LeftJoin, result.LeftJoin);
		Assert.Same(parts.RightJoin, result.RightJoin);
		Assert.Same(parts.Where, result.Where);
		Assert.Same(parts.WhereCustom, result.WhereCustom);
		Assert.Same(parts.Sort, result.Sort);
		Assert.Equal(parts.SortRandom, result.SortRandom);
		Assert.Equal(parts.Maximum, result.Maximum);
		Assert.Equal(parts.Skip, result.Skip);
	}
}
