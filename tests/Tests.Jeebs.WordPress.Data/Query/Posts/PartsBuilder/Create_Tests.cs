﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests
{
	public class Create_Tests : Create_Tests<Query.PostsPartsBuilder, WpPostId, WpPostEntity>
	{
		protected override Query.PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public override void Test00_Returns_With_Table() =>
			Test00();

		[Fact]
		public override void Test01_Calls_Extract_From() =>
			Test01();

		[Fact]
		public override void Test02_Returns_With_Maximum() =>
			Test02();

		[Fact]
		public override void Test03_Returns_With_Skip() =>
			Test03();
	}
}
