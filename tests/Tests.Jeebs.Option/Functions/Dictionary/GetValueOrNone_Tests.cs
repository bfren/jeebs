// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.MaybeF.Dictionary;

namespace F.MaybeF_Tests.Dictionary;

public class GetValueOrNone_Tests : Jeebs_Tests.Dictionary.GetValueOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_Dictionary_Returns_None_With_ListIsEmptyMsg()
	{
		Test00((dict, key) => GetValueOrNone(dict, key));
	}

	[Theory]
	[InlineData(null)]
	public override void Test01_Null_Key_Returns_None_With_KeyCannotBeNullMsg(string input)
	{
		Test01(dict => GetValueOrNone(dict, input));
	}

	[Fact]
	public override void Test02_Key_Does_Not_Exists_Returns_None_With_KeyDoesNotExistMsg()
	{
		Test02((dict, key) => GetValueOrNone(dict, key));
	}

	[Fact]
	public override void Test03_Key_Exists_Null_Item_Returns_None_With_NullValueMsg()
	{
		Test03((dict, key) => GetValueOrNone(dict, key));
	}

	[Fact]
	public override void Test04_Key_Exists_Valid_Item_Returns_Some_With_Value()
	{
		Test04((dict, key) => GetValueOrNone(dict, key));
	}
}
