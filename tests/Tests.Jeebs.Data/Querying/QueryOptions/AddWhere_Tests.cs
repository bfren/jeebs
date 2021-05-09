// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddWhere_Tests : AddWhere<TestOptions, TestId>
	{
		protected override TestOptions Create(IMapper mapper) =>
			new(mapper);

		[Fact]
		public override void Test00_Column_Exists_Adds_Where() =>
			Test00();

		[Fact]
		public override void Test01_Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg() =>
			Test01();
	}
}
