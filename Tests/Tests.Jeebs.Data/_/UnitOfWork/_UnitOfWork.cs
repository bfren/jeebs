using System;
using System.Data;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Extensions;
using Xunit;

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
			var l = Substitute.For<ILog>();
			var d = Substitute.For<IQueryDriver>();

			t.Connection.Returns(c);
			c.BeginTransaction().Returns(t);

			return (new Data.UnitOfWork(c, a, l, d), c, t, a, l, d);
		}

		/// <summary>
		/// Get a substituted OK Result
		/// </summary>
		public static (IOk, ILogger, MsgList) GetOkResult()
		{
			var l = Substitute.For<ILogger>();
			var m = Substitute.For<MsgList>();

			var r = Substitute.For<IOk>();
			r.Logger.Returns(l);
			r.Messages.Returns(m);

			return (r, l, m);
		}

		/// <summary>
		/// Get a substituted Error Result
		/// </summary>
		/// <param name="messages">[Optional] Messages List - if not set a fresh substitute will be made</param>
		public static IError<T> GetErrorResult<T>(MsgList? messages = null)
		{
			var l = Substitute.For<ILogger>();
			var m = messages ?? Substitute.For<MsgList>();

			var r = Substitute.For<IError<T>>();
			r.Logger.Returns(l);
			r.Messages.Returns(m);

			return r;
		}

		public delegate object UnitOfWorkMethod(IOk r, string query, object? param, CommandType commandType);

		public delegate IR<T> UnitOfWorkMethod<T>(IOk r, string query, object? param, CommandType commandType);

		public delegate T QueryDriverMethod<T>(IDbConnection cnn, string query, object? param, IDbTransaction transaction, CommandType commandType);

		/// <summary>
		/// Test that a query method logs the current query
		/// </summary>
		/// <param name="act">Method to run in 'Act' phase</param>
		/// <param name="commandType">The type of command being run</param>
		public static void LogsQuery(Func<IUnitOfWork, UnitOfWorkMethod> act, CommandType commandType)
		{
			// Arrange		
			var (w, _, _, _, _, _) = GetUnitOfWork();
			var (r, logger, messages) = GetOkResult();

			var query = F.Rnd.Str;
			var p0 = F.MathsF.RandomInt64(max: 1000);
			var p1 = F.Rnd.Str;
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

			Assert.Collection(messages.GetEnumerable(),
				x => Assert.Equal(
					$"{method}() - Query [{commandType}]: {query} - Parameters: {{ {nameof(p0)} = {p0}, {nameof(p1)} = {p1} }}",
					x.ToString()
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
			var (w, connection, transaction, _, _, driver) = GetUnitOfWork();
			var (r, _, _) = GetOkResult();

			var query = F.Rnd.Str;
			var p0 = F.MathsF.RandomInt64(max: 1000);
			var p1 = F.Rnd.Str;
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

			var ex = F.Rnd.Str;
			driver.ReturnsForAll<TReturn>(_ => throw new Exception(ex));
			driver.ReturnsForAll<Task<TReturn>>(_ => throw new Exception(ex));

			var (r, _, messages) = GetOkResult();
			var error = GetErrorResult<TReturn>(messages: messages);
			r.Error<TReturn>().Returns(error);

			var query = F.Rnd.Str;
			var p0 = F.MathsF.RandomInt64(max: 1000);
			var p1 = F.Rnd.Str;
			var parameters = new { p0, p1 };

			var f = act(w);
			var method = f.Method.Name;

			// Act
			f.Invoke(r, query, parameters, commandType);

			// Assert
			transaction.Received().Rollback();

			log.Received().Error(
				Arg.Any<Exception>(),
				"{ExceptionType}: {ExceptionText} | Query: {Sql} | Parameters: {Parameters}",
				Arg.Any<object[]>()
			);

			Assert.Collection(messages.GetEnumerable(),
				x => Assert.Equal(
					$"{method}() - Query [{commandType}]: {query} - Parameters: {{ {nameof(p0)} = {p0}, {nameof(p1)} = {p1} }}",
					x.ToString()
				),
				x => Assert.Equal(
					$"{typeof(Exception)}: {ex} | Query: {query} | Parameters: {{ {nameof(p0)} = {p0}, {nameof(p1)} = {p1} }}",
					x.ToString()
				)
			);
		}
	}
}
