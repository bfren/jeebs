// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Tables;
using Xunit;

namespace Jeebs.WordPress.Entities.Tables.PostMetaTable_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Adds_Prefix_To_Table_Name()
		{
			// Arrange
			var prefix = F.Rnd.Str;
			var expected = $"{prefix}postmeta";

			// Act
			var result = new PostMetaTable(prefix).GetName();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
