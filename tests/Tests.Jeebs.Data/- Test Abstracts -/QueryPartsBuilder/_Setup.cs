﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using NSubstitute.Extensions;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class QueryPartsBuilder_Tests<TBuilder, TId>
		where TBuilder : QueryPartsBuilder<TId>
		where TId : StrongId
	{
		protected abstract TBuilder GetConfiguredBuilder();

		public (TBuilder builder, Vars v) Setup()
		{
			var builder = GetConfiguredBuilder();

			var parts = new QueryParts(builder.Table);

			return (builder, new(parts));
		}

		public (TBuilder builder, VarsWithColumns v) Setup<TModel>()
		{
			var (builder, v) = Setup();

			var columns = Substitute.For<IColumnList>();
			builder.Configure().GetColumns<TModel>().Returns(columns);

			return (builder, new(columns, v.Parts));
		}

		public record Vars(
			QueryParts Parts
		);

		public record VarsWithColumns(
			IColumnList Columns,
			QueryParts Parts
		) : Vars(Parts);
	}

	public record TestTable0 : ITable
	{
		private readonly string name;

		public string Foo { get; init; }

		public TestTable0(string name, string foo) =>
			(this.name, Foo) = (name, foo);

		public string GetName() =>
			name;
	}

	public record TestTable1 : ITable
	{
		private readonly string name;

		public string Bar { get; init; }

		public TestTable1(string name, string bar) =>
			(this.name, Bar) = (name, bar);

		public string GetName() =>
			name;
	}
}
