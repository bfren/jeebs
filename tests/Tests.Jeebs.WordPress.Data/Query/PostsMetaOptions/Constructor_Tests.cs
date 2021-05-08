// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaOptions_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Maximum_To_Null()
		{
			// Arrange
			var db = Substitute.For<IWpDb>();

			// Act
			var result = new Query.PostsMetaOptions(db);

			// Assert
			Assert.Null(result.Maximum);
		}
	}
}
