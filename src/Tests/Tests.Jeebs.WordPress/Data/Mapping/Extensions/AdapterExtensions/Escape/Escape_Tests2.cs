﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Mapping.AdapterExtensions_Tests
{
	public partial class Escape_Tests
	{
		[Fact]
		public void Calls_EscapeColumn_For_Each_Column_And_Returns_String_List()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var table = new FooTable();
			var columns = Extract<Foo>.From(table);

			// Act
			var result = adapter.Escape(columns);

			// Assert
			adapter.Received(3).Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().Escape(table.FooId, nameof(table.FooId), table.ToString());
			adapter.Received().Escape(table.Bar0, nameof(table.Bar0), table.ToString());
			adapter.Received().Escape(table.Bar1, nameof(table.Bar1), table.ToString());
			Assert.Equal(3, result.Count);
		}
	}
}
