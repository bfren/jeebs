// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static Jeebs.Data.Querying.QueryOptions_Tests.Setup;

namespace Jeebs.Data.Querying.QueryOptions_Tests;

public class ToParts_Tests : ToParts_Tests<TestOptions, ITestBuilder, TestId>
{
	protected override (TestOptions options, ITestBuilder builder) Setup() =>
		GetOptions();

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
