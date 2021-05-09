// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddLeftJoin_Tests : AddLeftJoin<TestOptions, TestId>
	{
		protected override TestOptions Create(IMapper mapper) =>
			new(mapper);

		[Fact]
		public override void Test00_Adds_Columns_To_LeftJoin() =>
			Test00();
	}
}
