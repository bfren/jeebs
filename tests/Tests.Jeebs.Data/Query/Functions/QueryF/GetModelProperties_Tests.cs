﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;

namespace Jeebs.Data.Query.Functions.QueryF_Tests;

public class GetModelProperties_Tests
{
	[Fact]
	public void Returns_Properties_Without_Ignored()
	{
		// Arrange

		// Act
		var result = QueryF.GetModelProperties<TestModel>();

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(nameof(TestModel.Foo), x.Name),
			x => Assert.Equal(nameof(TestModel.Bar), x.Name)
		);
	}

	public sealed record class TestModel
	{
		public int Foo { get; set; }

		public string Bar { get; set; } = string.Empty;

		[Ignore]
		public bool Ignore { get; set; }
	}
}
