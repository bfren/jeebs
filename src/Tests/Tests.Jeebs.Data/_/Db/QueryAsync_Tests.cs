// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class QueryAsync_Tests
	{
		[Fact]
		public void Logs_Query_Info_To_Verbose()
		{
			// Arrange
			var (_, log, _, _, db) = Db_Setup.Get();
			var query = F.Rnd.Str;
			object parameters = F.Rnd.Guid;
			var type = CommandType.StoredProcedure;

			// Act
			var _ = db.QueryAsync<int>(query, parameters, type);

			// Assert
			log.Received().Verbose("{Query} ({Type}) {@Parameters}", query, type, parameters);
		}
	}
}
