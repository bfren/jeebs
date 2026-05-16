// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Collections.PagedList_Tests;

public class Roundtrip_Tests
{
	[Fact]
	public void Empty_PagedList_Roundtrips()
	{
		// Arrange
		var original = new PagedList<int>();

		// Act
		var json = JsonF.Serialise(original).Unwrap(_ => string.Empty);
		var result = JsonF.Deserialise<PagedList<int>>(json).Unwrap(_ => new());

		// Assert
		Assert.Empty(result);
		Assert.Equal(new PagingValues(), result.Values);
	}

	[Fact]
	public void Populated_PagedList_Of_Int_Roundtrips()
	{
		// Arrange
		var values = new PagingValues(items: 10, page: 2, itemsPer: 5, pagesPer: 3);
		var original = new PagedList<int>(values, [6, 7, 8, 9, 10]);

		// Act
		var json = JsonF.Serialise(original).Unwrap(_ => string.Empty);
		var result = JsonF.Deserialise<PagedList<int>>(json).Unwrap(_ => new());

		// Assert
		Assert.Equal(5, result.Count);
		Assert.Collection(result,
			x => Assert.Equal(6, x),
			x => Assert.Equal(7, x),
			x => Assert.Equal(8, x),
			x => Assert.Equal(9, x),
			x => Assert.Equal(10, x)
		);
		Assert.Equal(values, result.Values);
	}

	[Fact]
	public void Preserves_All_PagingValues_Fields()
	{
		// Arrange
		var values = new PagingValues(items: 47, page: 3, itemsPer: 5, pagesPer: 4);
		var original = new PagedList<int>(values, [11, 12, 13, 14, 15]);

		// Act
		var json = JsonF.Serialise(original).Unwrap(_ => string.Empty);
		var result = JsonF.Deserialise<PagedList<int>>(json).Unwrap(_ => new());

		// Assert
		Assert.Equal(values.Items, result.Values.Items);
		Assert.Equal(values.ItemsPer, result.Values.ItemsPer);
		Assert.Equal(values.FirstItem, result.Values.FirstItem);
		Assert.Equal(values.LastItem, result.Values.LastItem);
		Assert.Equal(values.Page, result.Values.Page);
		Assert.Equal(values.Pages, result.Values.Pages);
		Assert.Equal(values.PagesPer, result.Values.PagesPer);
		Assert.Equal(values.LowerPage, result.Values.LowerPage);
		Assert.Equal(values.UpperPage, result.Values.UpperPage);
		Assert.Equal(values.Skip, result.Values.Skip);
		Assert.Equal(values.Take, result.Values.Take);
	}

	[Fact]
	public void PagedList_Of_Record_Roundtrips()
	{
		// Arrange
		var when = new DateTime(2026, 5, 16, 12, 30, 45, DateTimeKind.Utc);
		var values = new PagingValues(items: 2, page: 1);
		var original = new PagedList<Sample>(values, [new Sample(1, "a", when), new Sample(2, "b", when)]);

		// Act
		var json = JsonF.Serialise(original).Unwrap(_ => string.Empty);
		var result = JsonF.Deserialise<PagedList<Sample>>(json).Unwrap(_ => new());

		// Assert
		Assert.Equal(2, result.Count);
		Assert.Collection(result,
			x => Assert.Equal(new Sample(1, "a", when), x),
			x => Assert.Equal(new Sample(2, "b", when), x)
		);
	}

	public sealed record class Sample(int Id, string Name, DateTime When);
}
