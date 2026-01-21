// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public abstract class Setup
{
	public static TestBuilder GetBuilder(IExtract extract) =>
		new(extract);
}

public sealed record class TestId : ULongId<TestId>;

public record class TestModel;

public class TestBuilder(IExtract extract) : QueryPartsBuilder<TestId>(extract)
{
	public override ITable Table { get; } =
		Substitute.For<ITable>();

	public override IColumn IdColumn { get; } =
		Substitute.For<IColumn>();
}
