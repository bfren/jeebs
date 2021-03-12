// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Extensions;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public static class UnitOfWork
	{
		/// <summary>
		/// Get a substituted Unit of Work
		/// </summary>
		public static (IUnitOfWork, IDbConnection, IDbTransaction, IAdapter, ILog, IQueryDriver) GetUnitOfWork()
		{
			var c = Substitute.For<IDbConnection>();
			var t = Substitute.For<IDbTransaction>();
			var a = Substitute.For<IAdapter>();
			var l = Substitute.For<ILog<Data.UnitOfWork>>();
			var d = Substitute.For<IQueryDriver>();

			t.Connection.Returns(c);
			c.BeginTransaction().Returns(t);

			return (new Data.UnitOfWork(c, a, l, d), c, t, a, l, d);
		}

		public delegate object UnitOfWorkMethod(string query, object? param, CommandType commandType);

		public delegate Option<T> UnitOfWorkMethod<T>(string query, object? param, CommandType commandType);

		public delegate T QueryDriverMethod<T>(IDbConnection cnn, string query, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Test that a query method logs the current query
		/// </summary>
		/// <param name="act">Method to run in 'Act' phase</param>
		/// <param name="commandType">The type of command being run</param>
		public static void LogsQuery(Func<IUnitOfWork, UnitOfWorkMethod> act, CommandType commandType)
		{
			// Arrange		
			var (w, _, _, _, l, _) = GetUnitOfWork();

			var query = JeebsF.Rnd.Str;
			var p0 = JeebsF.MathsF.RandomInt64(max: 1000);
			var p1 = JeebsF.Rnd.Str;
			var parameters = new { p0, p1 };

			var f = act(w);
			var method = f.Method.Name;

			// Act
			f(query, parameters, commandType);

			// Assert
			l.Received().Message(
				Arg.Is<Jm.Data.QueryMsg>(x =>
					x.Method == method
					&& x.Sql == query
					&& x.Parameters == parameters
					&& x.CommandType == commandType
				)
			);
		}

		/// <summary>
		/// Test that a query method calls the correct driver method
		/// </summary>
		/// <typeparam name="TReturn">Driver return type</typeparam>
		/// <param name="act">Method to run in 'Act' phase</param>
		/// <param name="assert">Method to test in 'Assert' phase</param>
		/// <param name="commandType">The type of command being run</param>
		public static void CallsDriver<TReturn>(
			Func<IUnitOfWork, UnitOfWorkMethod> act,
			Func<IQueryDriver, QueryDriverMethod<TReturn>> assert,
			CommandType commandType
		)
		{
			// Arrange
			var (w, connection, transaction, _, log, driver) = GetUnitOfWork();

			var query = JeebsF.Rnd.Str;
			var p0 = JeebsF.MathsF.RandomInt64(max: 1000);
			var p1 = JeebsF.Rnd.Str;
			var parameters = new { p0, p1 };

			// Act
			act(w)
				.Invoke(query, parameters, commandType);

			// Assert
#pragma warning disable NS5000 // Received check.
			assert(driver.Received())
#pragma warning restore NS5000 // Received check.
				.Invoke(connection, query, parameters, transaction, commandType);
		}

		/// <summary>
		/// Test that a query method handles failures correctly
		/// </summary>
		/// <typeparam name="TReturn">Driver return type</typeparam>
		/// <param name="act">Method to run in 'Act' phase</param>
		/// <param name="commandType">The type of command being run</param>
		public static void HandlesFailure<TReturn>(
			Func<IUnitOfWork, UnitOfWorkMethod> act,
			CommandType commandType
		)
		{
			// Arrange
			var (w, _, transaction, _, log, driver) = GetUnitOfWork();

			var ex = JeebsF.Rnd.Str;
			driver.ReturnsForAll<TReturn>(_ => throw new Exception(ex));
			driver.ReturnsForAll<Task<TReturn>>(_ => throw new Exception(ex));

			var query = JeebsF.Rnd.Str;
			var p0 = JeebsF.MathsF.RandomInt64(max: 1000);
			var p1 = JeebsF.Rnd.Str;
			var parameters = new { p0, p1 };

			var f = act(w);
			var method = f.Method.Name;

			// Act
			f.Invoke(query, parameters, commandType);

			// Assert
			transaction.Received().Rollback();

			log.Received().Error(
				Arg.Any<Exception>(),
				"{ExceptionType}: {ExceptionText} | Query: {Sql} | Parameters: {Parameters}",
				Arg.Any<object[]>()
			);
		}
	}
}
