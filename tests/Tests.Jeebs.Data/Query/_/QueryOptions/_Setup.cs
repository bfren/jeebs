// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Id;

namespace Jeebs.Data.Query.QueryOptions_Tests;

public static class Setup
{
	public static (TestOptions options, ITestBuilder builder) GetOptions()
	{
		var table = Substitute.For<ITable>();

		var builder = Substitute.For<ITestBuilder>();
		_ = builder.Create<TestModel>(Arg.Any<ulong?>(), Arg.Any<ulong>()).Returns(x =>
			  new QueryParts(table)
			  {
				  Maximum = x.ArgAt<ulong?>(0),
				  Skip = x.ArgAt<ulong>(1)
			  }
		);

		return (new(builder), builder);
	}
}

public readonly record struct TestId(long Value) : IStrongId;

public record class TestOptions : QueryOptions<TestId>
{
	public TestOptions(ITestBuilder builder) : base(builder) { }
}

public interface ITestBuilder : IQueryPartsBuilder<TestId> { }
