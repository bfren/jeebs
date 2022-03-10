﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Querying;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Querying.Options_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_T()
	{
		// Arrange
		var schema = Substitute.For<IWpDbSchema>();
		var builder = Substitute.For<IQueryPartsBuilder<TestId>>();

		// Act
		var result = new TestOptions(schema, builder);

		// Assert
		Assert.Same(schema, result.TTest);
	}

	public readonly record struct TestId(long Value) : IStrongId;

	public sealed record class TestOptions : Options<TestId>
	{
		public TestOptions(IWpDbSchema schema, IQueryPartsBuilder<TestId> builder) : base(schema, builder) { }
	}
}