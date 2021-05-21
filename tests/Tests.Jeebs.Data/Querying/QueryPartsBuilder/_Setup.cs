﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class Setup
	{
		public static TestBuilder GetBuilder(IExtract extract) =>
			new(extract);
	}

	public record TestId(long Value) : StrongId(Value)
	{
		public TestId() : this(0) { }
	}

	public record TestModel;

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
