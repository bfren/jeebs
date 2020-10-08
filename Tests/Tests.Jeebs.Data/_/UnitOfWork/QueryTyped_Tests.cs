using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class QueryTyped_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Logs_Query(CommandType commandType)
		{
			QueryMethodIsLogged(
				w => w.Query<int>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Calls_Driver_Query(CommandType commandType)
		{
			QueryMethodCallsDriver<IEnumerable<int>>(
				w => w.Query<int>,
				d => d.Query<int>,
				commandType
			);
		}
	}
}
