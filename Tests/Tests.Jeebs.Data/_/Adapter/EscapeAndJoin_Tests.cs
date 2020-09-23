using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Jeebs.Data.Adapter_Tests.Adapter;

namespace Jeebs.Data.Adapter_Tests
{
	public class EscapeAndJoin_Tests
	{
		[Fact]
		public void Empty_Elements_Returns_Empty_String()
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeAndJoin(new object?[] { null });

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Escapes_And_Joins()
		{
			// Arrange
			var adapter = GetAdapter();
			var input = new[] { "one", null, "two", "", " ", "three" };

			// Act
			var result = adapter.EscapeAndJoin(input);

			// Assert
			Assert.Equal("[one].[two].[three]", result);
		}
	}
}
