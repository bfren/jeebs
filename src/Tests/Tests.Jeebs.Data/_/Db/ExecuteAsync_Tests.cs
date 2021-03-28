// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class ExecuteAsync_Tests
	{
		[Fact]
		public void No_Return_Logs_Query_Info_To_Verbose()
		{
			// Arrange
			var (_, log, _, _, db) = Db_Setup.Get();
			var query = F.Rnd.Str;
			object parameters = F.Rnd.Guid;
			var type = CommandType.Text;

			// Act
			var _ = db.ExecuteAsync(query, parameters, type);

			// Assert
			log.Received().Verbose("{Query} ({Type}) {@Parameters}", query, type, parameters);
		}

		[Fact]
		public void With_Return_Logs_Query_Info_To_Verbose()
		{
			// Arrange
			var (_, log, _, _, db) = Db_Setup.Get();
			var query = F.Rnd.Str;
			object parameters = F.Rnd.Guid;
			var type = CommandType.Text;

			// Act
			var _ = db.ExecuteAsync<int>(query, parameters, type);

			// Assert
			log.Received().Verbose("{Query} ({Type}) {@Parameters}", query, type, parameters);
		}
	}
}
