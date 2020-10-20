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
			LogsQuery(
				w => w.SingleAsync<int>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
#pragma warning disable RCS1047 // Non-asynchronous method name should not end with 'Async'.
		public void Calls_Driver_QuerySingleAsync(CommandType commandType)
#pragma warning restore RCS1047 // Non-asynchronous method name should not end with 'Async'.
		{
			CallsDriver<Task<int>>(
				w => w.SingleAsync<int>,
				d => d.QuerySingleAsync<int>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Catches_Exception_Rolls_Back_Logs_Exception_Returns_Error(CommandType commandType)
		{
			HandlesFailure<int>(
				w => w.SingleAsync<int>,
				commandType
			);
		}
	}
}
