// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public static class Setup
	{
		public static (TestOptions options, ITestBuilder builder) GetOptions()
		{
			var table = Substitute.For<ITable>();

			var builder = Substitute.For<ITestBuilder>();
			builder.Create<TestModel>(Arg.Any<ulong?>(), Arg.Any<ulong>()).Returns(x =>
				new QueryParts(table)
				{
					Maximum = x.ArgAt<ulong?>(0),
					Skip = x.ArgAt<ulong>(1)
				}
			);

			return (new(builder), builder);
		}
	}

	public record class TestId(ulong Value) : StrongId(Value)
	{
		public TestId() : this(0) { }
	}

	public record class TestOptions : QueryOptions<TestId>
	{
		public TestOptions(ITestBuilder builder) : base(builder) { }
	}

	public interface ITestBuilder : IQueryPartsBuilder<TestId> { }
}
