using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class ExecuteScalarAsync_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Logs_Query(CommandType commandType)
		{
			LogsQuery(
				w => w.ExecuteScalarAsync<string>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
#pragma warning disable RCS1047 // Non-asynchronous method name should not end with 'Async'.
		public void Calls_Driver_ExecuteScalarAsync(CommandType commandType)
#pragma warning restore RCS1047 // Non-asynchronous method name should not end with 'Async'.
		{
			CallsDriver<Task<string>>(
				w => w.ExecuteScalarAsync<string>,
				d => d.ExecuteScalarAsync<string>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Catches_Exception_Rolls_Back_Logs_Exception_Returns_Error(CommandType commandType)
		{
			HandlesFailure<int>(
				w => w.ExecuteScalarAsync<int>,
				commandType
			);
		}
	}
}
