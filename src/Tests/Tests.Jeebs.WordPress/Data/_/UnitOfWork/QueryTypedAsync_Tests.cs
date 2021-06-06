// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Xunit;
using static Jeebs.WordPress.Data.UnitOfWork_Tests.UnitOfWork;

namespace Jeebs.WordPress.Data.UnitOfWork_Tests
{
	public class QueryTypedAsync_Tests
	{
		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Logs_Query(CommandType commandType)
		{
			LogsQuery(
				w => w.QueryAsync<int>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
#pragma warning disable RCS1047 // Non-asynchronous method name should not end with 'Async'.
		public void Calls_Driver_QueryAsync(CommandType commandType)
#pragma warning restore RCS1047 // Non-asynchronous method name should not end with 'Async'.
		{
			CallsDriver<Task<IEnumerable<int>>>(
				w => w.QueryAsync<int>,
				d => d.QueryAsync<int>,
				commandType
			);
		}

		[Theory]
		[InlineData(CommandType.StoredProcedure)]
		[InlineData(CommandType.Text)]
		public void Catches_Exception_Rolls_Back_Logs_Exception_Returns_Error(CommandType commandType)
		{
			HandlesFailure<IEnumerable<int>>(
				w => w.QueryAsync<int>,
				commandType
			);
		}
	}
}
