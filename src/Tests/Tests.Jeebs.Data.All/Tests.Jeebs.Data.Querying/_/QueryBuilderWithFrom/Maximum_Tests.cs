// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests
{
	public class Maximum_Tests
	{
		[Fact]
		public void Sets_Maximum()
		{
			// Arrange
			var table = Substitute.For<ITable>();
			var builder = new QueryBuilderWithFrom(table);
			var value = F.Rnd.Lng;

			// Act
			var result = builder.Maximum(value);

			// Assert
			var from = Assert.IsType<QueryBuilderWithFrom>(result);
			Assert.Equal(value, from.Parts.Maximum);
		}
	}
}
