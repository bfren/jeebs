// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using StrongId;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public abstract class Setup
{
	public static TestBuilder GetBuilder(IExtract extract) =>
		new(extract);
}

public sealed record class TestId : LongId;

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
