// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.Option_Tests
{
	public class Escape_Tests
	{
		[Fact]
		public void Escape_Table_Calls_Client_Escape()
		{
			// Arrange
			var (client, _, _, table, options) = Query_Setup.Get();

			// Act
			_ = options.EscapeTest(table);

			// Assert
			client.Received().Escape(table);
		}

		[Fact]
		public void Escape_Column_Calls_Client_Escape()
		{
			// Arrange
			var (client, _, _, table, options) = Query_Setup.Get();

			// Act
			_ = options.EscapeTest(table, t => t.Id);

			// Assert
			client.Received().EscapeWithTable(Arg.Any<IColumn>());
		}
	}
}
