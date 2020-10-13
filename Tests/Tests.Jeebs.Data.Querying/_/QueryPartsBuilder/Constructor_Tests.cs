using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Adapter_And_Parts()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var from = F.Rnd.String;

			// Act
			var result = Substitute.For<QueryPartsBuilder<string, QueryOptions>>(adapter, from);

			// Assert
			Assert.Equal(adapter, result.Adapter);
			var parts = Assert.IsType<QueryParts>(result.Parts);
			Assert.Equal(from, parts.From);
		}
	}
}
