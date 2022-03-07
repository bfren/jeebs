// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;
using static F.MaybeF;

namespace F.MaybeF_Tests;

public class UnwrapSingleAsync_Tests : Jeebs_Tests.UnwrapSingleAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00(mbe => UnwrapAsync(mbe, x => x.Single<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_None_Returns_None()
	{
		await Test01(mbe => UnwrapAsync(mbe, x => x.Single<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_None_With_Reason_Returns_None_With_Reason()
	{
		await Test02(mbe => UnwrapAsync(mbe, x => x.Single<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg()
	{
		await Test03(mbe => UnwrapAsync(mbe, x => x.Single<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_No_Items_Runs_NoItems()
	{
		await Test04((mbe, noItems) => UnwrapAsync(mbe, x => x.Single<int>(noItems, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg()
	{
		await Test05(mbe => UnwrapAsync(mbe, x => x.Single<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test06_Too_Many_Items_Runs_TooMany()
	{
		await Test06((mbe, tooMany) => UnwrapAsync(mbe, x => x.Single<int>(null, tooMany, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg()
	{
		await Test07(mbe => UnwrapAsync(mbe, x => x.Single<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test08_Not_A_List_Runs_NotAList()
	{
		await Test08((mbe, notAList) => UnwrapAsync(mbe, x => x.Single<int>(null, null, notAList))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg()
	{
		await Test09(mbe => UnwrapAsync(mbe, x => x.Single<string>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test10_List_With_Single_Item_Returns_Single()
	{
		await Test10(mbe => UnwrapAsync(mbe, x => x.Single<int>(null, null, null))).ConfigureAwait(false);
	}
}
