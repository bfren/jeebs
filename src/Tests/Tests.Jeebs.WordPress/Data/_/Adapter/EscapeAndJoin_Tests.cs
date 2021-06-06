// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Xunit;
using static Jeebs.WordPress.Data.Adapter_Tests.Adapter;

namespace Jeebs.WordPress.Data.Adapter_Tests
{
	public class EscapeAndJoin_Tests
	{
		[Fact]
		public void No_Elements_Returns_Empty_String()
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeAndJoin();

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Empty_Elements_Returns_Empty_String()
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeAndJoin(Array.Empty<object?>());

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
