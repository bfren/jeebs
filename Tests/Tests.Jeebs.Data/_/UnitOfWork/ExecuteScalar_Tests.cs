using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class ExecuteScalar_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Logs_Query(CommandType commandType)
		{
			QueryMethodIsLogged(
				w => w.ExecuteScalar<string>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Calls_Driver_ExecuteScalar(CommandType commandType)
		{
			QueryMethodCallsDriver<string>(
				w => w.ExecuteScalar<string>,
				d => d.ExecuteScalar<string>,
				commandType
			);
		}
	}
}
