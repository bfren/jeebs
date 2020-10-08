using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public static class UnitOfWork
	{
		public static (IUnitOfWork, IDbConnection, IDbTransaction, IAdapter, ILog, IQueryDriver) GetUnitOfWork(
			IDbConnection? connection = null,
			IDbTransaction? transaction = null,
			IAdapter? adapter = null,
			ILog? log = null,
			IQueryDriver? driver = null
		)
		{
			var c = connection ?? Substitute.For<IDbConnection>();
			var t = transaction ?? Substitute.For<IDbTransaction>();
			var a = adapter ?? Substitute.For<IAdapter>();
			var l = log ?? Substitute.For<ILog>();
			var d = driver ?? Substitute.For<IQueryDriver>();

			if (transaction == null)
			{
				t.Connection.Returns(c);
			}

			if (connection == null)
			{
				c.BeginTransaction().Returns(t);
			}

			return (new Data.UnitOfWork(c, a, l, d), c, t, a, l, d);
		}

		public static (IOk, ILogger, MsgList) GetResult(
			ILogger? logger = null,
			MsgList? messages = null
		)
		{
			var l = logger ?? Substitute.For<ILogger>();
			var m = messages ?? Substitute.For<MsgList>();

			var r = Substitute.For<IOk>();
			r.Logger.Returns(l);
			r.Messages.Returns(m);

			return (r, l, m);
		}

		public static void QueryMethodIsLogged(
			Func<IUnitOfWork, TestAction> act,
			CommandType commandType
		)
		{
			// Arrange		
			var (w, _, _, _, _, _) = GetUnitOfWork();
			var (r, logger, messages) = GetResult();

			var query = "Some query";
			var p0 = 18;
			var p1 = "seven";
			var parameters = new { p0, p1 };

			var f = act(w);
			var method = f.Method.Name;

			// Act
			f(r, query, parameters, commandType);

			// Assert
			logger.Received().Message(
				Arg.Is<Jm.Data.QueryMsg>(x =>
					x.Method == method
					&& x.Sql == query
					&& x.Parameters == parameters
					&& x.CommandType == commandType
				)
			);

			Assert.Collection(messages,
				x => Assert.Equal(
					$"{method}() - Query [{commandType}]: {query} - Parameters: {{ {nameof(p0)} = {p0}, {nameof(p1)} = {p1} }}",
					x.ToString()
				)
			);
		}

		public static void QueryMethodCallsDriver<TReturn>(
			Func<IUnitOfWork, TestAction> act,
			Func<IQueryDriver, TestAssertion<TReturn>> assert,
			CommandType commandType
		)
		{
			// Arrange
			var (w, connection, transaction, _, _, driver) = GetUnitOfWork();
			var (r, _, _) = GetResult();

			var query = "Some query";
			var p0 = 18;
			var p1 = "seven";
			var parameters = new { p0, p1 };

			// Act
			act(w)
				.Invoke(r, query, parameters, commandType);

			// Assert
#pragma warning disable NS5000 // Received check.
			assert(driver.Received())
#pragma warning restore NS5000 // Received check.
				.Invoke(connection, query, parameters, transaction, commandType);
		}

		public delegate object TestAction(IOk r, string query, object? param, CommandType commandType);

		public delegate T TestAssertion<T>(IDbConnection cnn, string query, object? param, IDbTransaction transaction, CommandType commandType);
	}
}
