// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			builder.Create<TestModel>(Arg.Any<long?>(), Arg.Any<long>()).Returns(x =>
				new QueryParts(table)
				{
					Maximum = x.ArgAt<long?>(0),
					Skip = x.ArgAt<long>(1)
				}
			);

			return (new(builder), builder);
		}
	}

	public record TestId(long Value) : StrongId(Value)
	{
		public TestId() : this(0) { }
	}

	public record TestOptions : QueryOptions<TestId>
	{
		public TestOptions(ITestBuilder builder) : base(builder) { }
	}

	public interface ITestBuilder : IQueryPartsBuilder<TestId> { }
}
