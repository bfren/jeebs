﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Linq;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests
{
	public class AddWhereSlug_Tests : QueryPartsBuilder_Tests<Query.TermsPartsBuilder, WpTermId>
	{
		protected override Query.TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public void Null_Slug_Does_Nothing()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereSlug(v.Parts, null);

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}

		[Fact]
		public void Adds_Slug_To_Where()
		{
			// Arrange
			var (builder, v) = Setup();
			var slug = F.Rnd.Str;

			// Act
			var result = builder.AddWhereSlug(v.Parts, slug);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(builder.TTest.Term.GetName(), x.column.Table);
					Assert.Equal(builder.TTest.Term.Slug, x.column.Name);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(slug, x.value);
				}
			);
		}
	}
}
