// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Data.Querying.QueryParameters_Tests;

public class Merge_Tests
{
	[Fact]
	public void Adds_QueryParameters_To_Dictionary()
	{
		// Arrange
		var p0 = F.Rnd.Str;
		var p1 = F.Rnd.Str;
		var p2 = F.Rnd.Str;
		var parametersToAdd = new QueryParameters
		{
			{ nameof(p0), p0 },
			{ nameof(p1), p1 },
			{ nameof(p2), p2 }
		};

		var parameters = new QueryParameters();

		// Act
		var result = parameters.Merge(parametersToAdd);

		// Assert
		Assert.True(result);
		Assert.Collection(parameters,
			x => { Assert.Equal(nameof(p0), x.Key); Assert.Equal(p0, x.Value); },
			x => { Assert.Equal(nameof(p1), x.Key); Assert.Equal(p1, x.Value); },
			x => { Assert.Equal(nameof(p2), x.Key); Assert.Equal(p2, x.Value); }
		);
	}

	[Fact]
	public void Returns_False_If_Key_Exists()
	{
		// Arrange
		var p0 = F.Rnd.Str;
		var parameterToAdd = new QueryParameters
		{
			{ nameof(p0), p0 }
		};

		var parameters = new QueryParameters();
		parameters.TryAdd(parameterToAdd);

		// Act
		var result = parameters.Merge(parameterToAdd);

		// Assert
		Assert.False(result);
		Assert.Collection(parameters,
			x => { Assert.Equal(nameof(p0), x.Key); Assert.Equal(p0, x.Value); }
		);
	}
}
