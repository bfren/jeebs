using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class QueryDynamic_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Logs_Query(CommandType commandType)
		{
			QueryMethodIsLogged(
				w => w.QueryAsync,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Calls_Driver_QueryAsync(CommandType commandType)
		{
			QueryMethodCallsDriver<Task<IEnumerable<dynamic>>>(
				w => w.QueryAsync,
				d => d.QueryAsync,
				commandType
			);
		}
	}
}
