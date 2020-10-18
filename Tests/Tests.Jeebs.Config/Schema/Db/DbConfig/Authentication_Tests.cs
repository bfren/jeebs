using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Config.DbConfig_Tests
{
	public class Authentication_Tests
	{
		[Fact]
		public void Returns_Default_If_Not_Set()
		{
			// Arrange
			var name = F.Rnd.Str;
			var config = new DbConfig { Default = name };

			// Act
			var result = config.Authentication;

			// Assert
			Assert.Equal(name, result);
		}

		[Fact]
		public void Returns_If_Set()
		{
			// Arrange
			var n0 = F.Rnd.Str;
			var n1 = F.Rnd.Str;
			var config = new DbConfig
			{
				Default = n0,
				Authentication = n1
			};

			// Act
			var result = config.Authentication;

			// Assert
			Assert.Equal(n1, result);
		}
	}
}
