﻿using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.Column_AliasComparer_Tests
{
	public class GetHashCode_Tests
	{
		[Fact]
		public void Returns_Alias_Hash()
		{
			// Arrange
			var alias = "alias";
			var ha = alias.GetHashCode();

			var c0 = Substitute.For<IColumn>();
			c0.Name.Returns(F.StringF.Random(6));
			c0.Alias.Returns(alias);
			
			var c1 = Substitute.For<IColumn>();
			c1.Name.Returns(F.StringF.Random(6));
			c1.Alias.Returns(alias);
			
			var comparer = new Column.AliasComparer();

			// Act
			var h0 = comparer.GetHashCode(c0);
			var h1 = comparer.GetHashCode(c1);

			// Assert
			Assert.Equal(ha, h0);
			Assert.Equal(ha, h1);
		}
	}
}
