using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class Execute_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Logs_Query(CommandType commandType)
		{
			QueryMethodIsLogged(
				w => w.Execute,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Calls_Driver_Execute(CommandType commandType)
		{
			QueryMethodCallsDriver<int>(
				w => w.Execute,
				d => d.Execute,
				commandType
			);
		}
	}
}
