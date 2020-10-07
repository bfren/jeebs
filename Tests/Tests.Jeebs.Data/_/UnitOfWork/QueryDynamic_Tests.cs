using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NSubstitute;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class QueryDynamic_Tests
	{
		[Fact]
		public void Logs_Query()
		{
			// Arrange		
			var (w, _, _, _) = GetUnitOfWork();
			var (r, logger, messages) = GetResult();

			var query = "Some query";
			var p0 = 18;
			var p1 = "seven";
			var parameters = new { p0, p1 };

			// Act
			w.Query(r, query, parameters);

			// Assert
			logger.Received().Message(Arg.Is<Jm.Data.QueryMsg>(
				x => x.Method == nameof(IUnitOfWork.Query)
				&& x.Sql == query
				&& x.Parameters == parameters
				&& x.CommandType == CommandType.Text
			));

			Assert.Collection(messages,
				x => Assert.Equal("Query() - Query [Text]: Some query - Parameters: { p0 = 18, p1 = seven }", x.ToString())
			);
		}
	}
}
