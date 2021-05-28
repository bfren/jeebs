// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.WordPress.Data;
using Xunit;
using static F.WordPressF.DataF.QueryPostsF;

namespace F.WordPressF.DataF.QueryPostsF_Tests
{
	public class GetTermLists_Tests
	{
		[Fact]
		public void No_TermList_Properties_Returns_Empty_List()
		{
			// Arrange

			// Act
			var result = GetTermLists<NoTermLists>();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void With_TermList_Properties_Returns_PropertyInfo()
		{
			// Arrange

			// Act
			var result = GetTermLists<WithTermLists>();

			// Assert
			Assert.Collection(result,
				x =>
				{
					Assert.Equal(nameof(WithTermLists.Terms0), x.Name);
					Assert.Equal(typeof(TermList), x.PropertyType);
				},
				x =>
				{
					Assert.Equal(nameof(WithTermLists.Terms1), x.Name);
					Assert.Equal(typeof(TermList), x.PropertyType);
				}
			);
		}

		public record NoTermLists;

		public record WithTermLists(TermList Terms0, TermList Terms1);
	}
}
