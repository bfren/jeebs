// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class Setup
	{
		public static TestBuilder GetBuilder(IExtract extract) =>
			new(extract);
	}

	public readonly record struct TestId(ulong Value) : IStrongId;

	public record class TestModel;

	public class TestBuilder : QueryPartsBuilder<TestId>
	{
		public override ITable Table { get; }

		public override IColumn IdColumn { get; }

		public TestBuilder(IExtract extract) : base(extract)
		{
			Table = Substitute.For<ITable>();
			IdColumn = Substitute.For<IColumn>();
		}
	}
}
