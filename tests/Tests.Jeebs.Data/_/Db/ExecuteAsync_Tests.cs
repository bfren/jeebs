// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
			var parameters = F.Rnd.Guid.ToString();
			const CommandType type = CommandType.Text;

			// Act
			var _ = db.ExecuteAsync(query, parameters, type);

			// Assert
			log.Received().Verbose("{Type}: {Query} Parameters: {@Parameters}", type, query, parameters);
		}

		[Fact]
		public void With_Return_Logs_Query_Info_To_Verbose()
		{
			// Arrange
			var (_, log, _, _, db) = Db_Setup.Get();
			var query = F.Rnd.Str;
			var parameters = F.Rnd.Guid.ToString();
			const CommandType type = CommandType.Text;

			// Act
			var _ = db.ExecuteAsync<int>(query, parameters, type);

			// Assert
			log.Received().Verbose("{Type}: {Query} Parameters: {@Parameters}", type, query, parameters);
		}
	}
}
