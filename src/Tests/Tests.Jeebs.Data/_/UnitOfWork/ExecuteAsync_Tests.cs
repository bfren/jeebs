// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Threading.Tasks;
using Xunit;
using static Jeebs.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public class ExecuteAsync_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Logs_Query(CommandType commandType)
		{
			LogsQuery(
				w => w.ExecuteAsync,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
#pragma warning disable RCS1047 // Non-asynchronous method name should not end with 'Async'.
		public void Calls_Driver_ExecuteAsync(CommandType commandType)
#pragma warning restore RCS1047 // Non-asynchronous method name should not end with 'Async'.
		{
			CallsDriver<Task<int>>(
				w => w.ExecuteAsync,
				d => d.ExecuteAsync,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Catches_Exception_Rolls_Back_Logs_Exception_Returns_Error(CommandType commandType)
		{
			HandlesFailure<int>(
				w => w.ExecuteAsync,
				commandType
			);
		}
	}
}
