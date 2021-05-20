// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using NSubstitute.Extensions;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class Setup
	{
		public static TestBuilder GetBuilder() =>
			Substitute.ForPartsOf<TestBuilder>();
	}

	public record TestId(long Value) : StrongId(Value)
	{
		public TestId() : this(0) { }
	}

	public record TestModel;

	public abstract class TestBuilder : QueryPartsBuilder<TestId> { }
}
