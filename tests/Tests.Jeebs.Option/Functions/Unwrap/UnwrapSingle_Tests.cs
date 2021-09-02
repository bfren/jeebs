﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class UnwrapSingle_Tests : Jeebs_Tests.UnwrapSingle_Tests
	{
		[Fact]
		public override void Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			Test00(opt => UnwrapSingle<int, int>(opt, null, null, null));
		}

		[Fact]
		public override void Test01_None_Returns_None()
		{
			Test01(opt => UnwrapSingle<int, int>(opt, null, null, null));
		}

		[Fact]
		public override void Test02_None_With_Reason_Returns_None_With_Reason()
		{
			Test02(opt => UnwrapSingle<int, int>(opt, null, null, null));
		}

		[Fact]
		public override void Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg()
		{
			Test03(opt => UnwrapSingle<int[], int>(opt, null, null, null));
		}

		[Fact]
		public override void Test04_No_Items_Runs_NoItems()
		{
			Test04((opt, noItems) => UnwrapSingle<int[], int>(opt, noItems, null, null));
		}

		[Fact]
		public override void Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg()
		{
			Test05(opt => UnwrapSingle<int[], int>(opt, null, null, null));
		}

		[Fact]
		public override void Test06_Too_Many_Items_Runs_TooMany()
		{
			Test06((opt, tooMany) => UnwrapSingle<int[], int>(opt, null, tooMany, null));
		}

		[Fact]
		public override void Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg()
		{
			Test07(opt => UnwrapSingle<int, int>(opt, null, null, null));
		}

		[Fact]
		public override void Test08_Not_A_List_Runs_NotAList()
		{
			Test08((opt, notAList) => UnwrapSingle<int, int>(opt, null, null, notAList));
		}

		[Fact]
		public override void Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg()
		{
			Test09(opt => UnwrapSingle<int[], string>(opt, null, null, null));
		}

		[Fact]
		public override void Test10_List_With_Single_Item_Returns_Single()
		{
			Test10(opt => UnwrapSingle<int[], int>(opt, null, null, null));
		}
	}
}
