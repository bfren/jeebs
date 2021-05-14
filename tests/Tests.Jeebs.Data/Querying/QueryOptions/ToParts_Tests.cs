// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class ToParts_Tests : QueryOptions_Tests
	{
		[Fact]
		public override void Test00_Calls_Builder_Create_With_Maximum_And_Skip()
		{
			var (options, builder) = Setup();
			Test00(options, builder);
		}

		[Fact]
		public override void Test01_Id_Null_Ids_Empty_Does_Not_Call_Builder_AddWhereId()
		{
			var (options, builder) = Setup();
			Test01(options, builder);
		}

		[Fact]
		public override void Test02_Id_Not_Null_Calls_Builder_AddWhereId()
		{
			var (options, builder) = Setup();
			Test02(options, builder);
		}

		[Fact]
		public override void Test03_Ids_Not_Empty_Calls_Builder_AddWhereId()
		{
			var (options, builder) = Setup();
			Test03(options, builder);
		}

		[Fact]
		public override void Test04_SortRandom_False_Sort_Empty_Does_Not_Call_Builder_AddSort()
		{
			var (options, builder) = Setup();
			Test04(options, builder);
		}

		[Fact]
		public override void Test05_SortRandom_True_Calls_Builder_AddSort()
		{
			var (options, builder) = Setup();
			Test05(options, builder);
		}

		[Fact]
		public override void Test06_Sort_Not_Empty_Calls_Builder_AddSort()
		{
			var (options, builder) = Setup();
			Test06(options, builder);
		}
	}
}
