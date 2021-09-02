// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbQuery_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var db = Substitute.For<IDb>();
		var log = Substitute.For<ILog>();

		// Act
		var result = new TestQuery(db, log);

		// Assert
		Assert.Same(db, result.DbTest);
		Assert.Same(log, result.LogTest);
	}

	public sealed class TestQuery : DbQuery<IDb>
	{
		public TestQuery(IDb db, ILog log) : base(db, log) { }
	}
}
