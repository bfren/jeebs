// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Data.Common;
using Jeebs.Config.Db;
using Jeebs.Functions;
using Jeebs.Logging;

namespace Jeebs.Data.Common.Db_Tests;

public abstract class Db_Setup
{
	/// <summary>
	/// Runs all work in <paramref name="unitOfWork"/>, so tests can check what was done with it.
	/// </summary>
	public abstract class TestDb(IDbClient client, DbConnectionConfig config, ILog log, IUnitOfWork unitOfWork) :
		Db(client, config, log)
	{
		protected override Task<IUnitOfWork> StartWorkAsync() =>
			Task.FromResult(unitOfWork);
	}

	public static (Db, Vars) Setup()
	{
		var connectionString = Rnd.Str;
		var config = new DbConnectionConfig { ConnectionString = connectionString };

		var log = Substitute.For<ILog>();

		var connection = Substitute.ForPartsOf<DbConnection>();

		var transaction = Substitute.ForPartsOf<DbTransaction>();

		var client = Substitute.For<IDbClient>();
		client.GetConnection(Arg.Any<string>()).Returns(connection);

		var adapter = Substitute.For<IAdapter>();
		adapter.QueryAsync<int>(connection, Arg.Any<IDbTransaction?>(), Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<CommandType>())
			.Returns(R.Wrap(new[] { Rnd.Int }.AsEnumerable()));
		adapter.QuerySingleAsync<int>(connection, Arg.Any<IDbTransaction?>(), Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<CommandType>())
			.Returns(Rnd.Int);
		adapter.ExecuteAsync(connection, transaction, Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<CommandType>())
			.Returns(Rnd.Flip ? 0 : 1);
		adapter.ExecuteAsync<int>(connection, transaction, Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<CommandType>())
			.Returns(Rnd.Int);
		client.Adapter.Returns(adapter);

		var unitOfWork = Substitute.For<IUnitOfWork>();
		unitOfWork.Connection.Returns(connection);
		unitOfWork.Transaction.Returns(new LazyAsync<DbTransaction>(Task.FromResult(transaction)));

		var db = Substitute.ForPartsOf<TestDb>(client, config, log, unitOfWork);

		return (db, new(client, config, connection, log, transaction, unitOfWork));
	}

	public sealed record Vars(
		IDbClient Client,
		DbConnectionConfig Config,
		IDbConnection Connection,
		ILog Log,
		IDbTransaction Transaction,
		IUnitOfWork UnitOfWork
	);

	public static bool Cmp(object expected, object actual) =>
		Equals(
			JsonF.Serialise(expected).Unsafe().Unwrap(),
			JsonF.Serialise(actual).Unsafe().Unwrap()
		);
}
