using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class SingleAsync_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Logs_Query(CommandType commandType)
		{
			QueryMethodIsLogged(
				w => w.SingleAsync<int>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Calls_Driver_QuerySingleAsync(CommandType commandType)
		{
			QueryMethodCallsDriver<Task<int>>(
				w => w.SingleAsync<int>,
				d => d.QuerySingleAsync<int>,
				commandType
			);
		}
	}
}
