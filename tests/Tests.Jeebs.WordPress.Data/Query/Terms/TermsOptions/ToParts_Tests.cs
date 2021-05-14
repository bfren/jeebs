// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Querying.QueryOptions_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.TermsOptions_Tests
{
	public class ToParts_Tests : ToParts_Tests<Query.TermsOptions, IQueryTermsPartsBuilder, WpTermId>
	{
		protected override (Query.TermsOptions options, IQueryTermsPartsBuilder builder) Setup()
		{
			var schema = new WpDbSchema(F.Rnd.Str);
			var builder = GetConfiguredBuilder(schema.Term);
			var options = new Query.TermsOptions(schema, builder);

			return (options, builder);
		}

		[Fact]
		public override void Test00_Calls_Builder_Create_With_Maximum_And_Skip() =>
			Test00();

		[Fact]
		public override void Test01_Id_Null_Ids_Empty_Does_Not_Call_Builder_AddWhereId() =>
			Test01();

		[Fact]
		public override void Test02_Id_Not_Null_Calls_Builder_AddWhereId() =>
			Test02();

		[Fact]
		public override void Test03_Ids_Not_Empty_Calls_Builder_AddWhereId() =>
			Test03();

		[Fact]
		public override void Test04_SortRandom_False_Sort_Empty_Does_Not_Call_Builder_AddSort() =>
			Test04();

		[Fact]
		public override void Test05_SortRandom_True_Calls_Builder_AddSort() =>
			Test05();

		[Fact]
		public override void Test06_Sort_Not_Empty_Calls_Builder_AddSort() =>
			Test06();
	}
}
