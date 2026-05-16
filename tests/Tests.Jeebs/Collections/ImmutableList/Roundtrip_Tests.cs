// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Collections.ImmutableList_Tests;

public class Roundtrip_Tests
{
	[Fact]
	public void Empty_ImmutableList_Roundtrips()
	{
		// Arrange
		var original = new ImmutableList<int>();

		// Act
		var json = JsonF.Serialise(original).Unwrap(_ => string.Empty);
		var result = JsonF.Deserialise<ImmutableList<int>>(json).Unwrap(_ => new());

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Populated_ImmutableList_Of_Int_Roundtrips()
	{
		// Arrange
		var original = new ImmutableList<int>([3, 1, 4, 1, 5, 9, 2, 6]);

		// Act
		var json = JsonF.Serialise(original).Unwrap(_ => string.Empty);
		var result = JsonF.Deserialise<ImmutableList<int>>(json).Unwrap(_ => new());

		// Assert
		Assert.Equal(8, result.Count);
		Assert.Collection(result,
			x => Assert.Equal(3, x),
			x => Assert.Equal(1, x),
			x => Assert.Equal(4, x),
			x => Assert.Equal(1, x),
			x => Assert.Equal(5, x),
			x => Assert.Equal(9, x),
			x => Assert.Equal(2, x),
			x => Assert.Equal(6, x)
		);
	}

	[Fact]
	public void ImmutableList_Of_Record_Roundtrips()
	{
		// Arrange
		var original = new ImmutableList<Sample>([new Sample(1, "a"), new Sample(2, "b")]);

		// Act
		var json = JsonF.Serialise(original).Unwrap(_ => string.Empty);
		var result = JsonF.Deserialise<ImmutableList<Sample>>(json).Unwrap(_ => new());

		// Assert
		Assert.Equal(2, result.Count);
		Assert.Collection(result,
			x => Assert.Equal(new Sample(1, "a"), x),
			x => Assert.Equal(new Sample(2, "b"), x)
		);
	}

	public sealed record class Sample(int Id, string Name);
}
