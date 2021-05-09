// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.DataF_Tests.QueryOptionsF_Tests;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddInnerJoin_Tests
	{
		[Fact]
		public void Adds_Columns_To_InnerJoin()
		{
			Adds_Columns_To_InnerJoin<TestOptions, TestId>(mapper => new TestOptions(mapper));
		}
	}
}
