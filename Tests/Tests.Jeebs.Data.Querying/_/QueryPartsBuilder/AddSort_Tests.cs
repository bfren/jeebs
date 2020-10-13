using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddSort_Tests
	{
		[Fact]
		public void Random_Calls_Adapter_GetRandomSortOrder()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var from = F.Rnd.String;
			var builder = Substitute.ForPartsOf<QueryPartsBuilder<string, Options>>(adapter, from);
			var options = new Options { SortRandom = true };

			// Act
			builder.AddSort(options);

			// Assert
			adapter.Received().GetRandomSortOrder();
		}

		public class Options : QueryOptions { }
	}
}
