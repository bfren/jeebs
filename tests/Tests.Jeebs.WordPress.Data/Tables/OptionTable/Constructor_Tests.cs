// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Tables;
using Xunit;

namespace Jeebs.WordPress.Entities.Tables.OptionTable_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Adds_Prefix_To_Table_Name()
		{
			// Arrange
			var prefix = F.Rnd.Str;
			var expected = $"{prefix}options";

			// Act
			var result = new OptionTable(prefix).GetName();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
