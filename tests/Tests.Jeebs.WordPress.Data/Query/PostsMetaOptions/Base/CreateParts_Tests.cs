// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.QueryOptions_Tests;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaOptions_Tests
{
	public class CreateParts_Tests : CreateParts<Query.PostsMetaOptions, WpPostMetaId>
	{
		protected override Query.PostsMetaOptions Create(IMapper mapper) =>
			new(Substitute.For<IWpDb>());

		[Fact]
		public override void Test00_Returns_New_QueryParts_With_Table() =>
			Test00();

		[Fact]
		public override void Test01_Returns_New_QueryParts_With_Select() =>
			Test01();

		[Fact]
		public override void Test02_Returns_New_QueryParts_With_Maximum() =>
			Test02();

		[Fact]
		public override void Test03_Returns_New_QueryParts_With_Skip() =>
			Test03();
	}
}
