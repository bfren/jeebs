// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;
using Jeebs.Logging;

namespace Jeebs.Data.Common.UnitOfWork_Tests;

public abstract class UnitOfWork_Setup
{
	/// <param name="begun">If true, the transaction is begun (as a write would) before the test acts.</param>
	internal static (UnitOfWork, Vars) Setup(bool begun = true)
	{
		var connection = Substitute.ForPartsOf<DbConnection>();
		var transaction = Substitute.ForPartsOf<DbTransaction>();
		var log = Substitute.For<ILog>();
		var unitOfWork = new UnitOfWork(connection, new(Task.FromResult(transaction)), log);
		if (begun)
		{
			_ = unitOfWork.Transaction.Value;
		}

		return (unitOfWork, new(connection, transaction, log));
	}

	internal sealed record class Vars(
		DbConnection Connection,
		DbTransaction Transaction,
		ILog Log
	);
}
