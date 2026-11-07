// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.SQLite;
using Microsoft.Extensions.Logging;

namespace Jeebs.Logging.Serilog.Sqlite.SqliteLogging_Tests;

public class Enable_Tests
{
	private const string ConnectionString = "Data Source=:memory:";

	private static CaptureLogger Setup()
	{
		var log = new CaptureLogger();
		SqliteLogging.Enable(log);
		return log;
	}

	private static void Execute(SQLiteConnection connection, string sql)
	{
		using var command = new SQLiteCommand(sql, connection);
		_ = command.ExecuteNonQuery();
	}

	[Fact]
	public void Logs_Open_Close_And_Statements_At_Debug()
	{
		// Arrange
		var log = Setup();
		var insert = "INSERT INTO foo VALUES (@id)";

		// Act
		using (var connection = new SQLiteConnection(ConnectionString))
		{
			connection.Open();
			Execute(connection, "CREATE TABLE foo (id INTEGER)");

			using var command = new SQLiteCommand(insert, connection);
			_ = command.Parameters.AddWithValue("@id", Rnd.Int);
			_ = command.ExecuteNonQuery();
		}

		// Assert
		Assert.Contains((LogLevel.Debug, "Opened connection."), log.Entries);
		Assert.Contains((LogLevel.Debug, $"Executing {insert}"), log.Entries);
		Assert.Contains((LogLevel.Debug, "Closed connection."), log.Entries);
	}

	[Fact]
	public void Does_Not_Log_Connection_String()
	{
		// Arrange
		var log = Setup();

		// Act
		using (var connection = new SQLiteConnection(ConnectionString))
		{
			connection.Open();
		}

		// Assert
		Assert.NotEmpty(log.Entries);
		Assert.DoesNotContain(log.Entries, x => x.Message.Contains("Data Source"));
	}

	[Fact]
	public void Engine_Error__Logs_At_Error()
	{
		// Arrange
		var log = Setup();

		// Act
		using (var connection = new SQLiteConnection(ConnectionString))
		{
			connection.Open();
			_ = Assert.Throws<SQLiteException>(() => Execute(connection, "SELECT * FROM missing"));
		}

		// Assert
		Assert.Contains(log.Entries, x => x.Level == LogLevel.Error && x.Message.Contains("no such table: missing"));
	}

	private sealed class CaptureLogger : ILogger
	{
		private readonly List<(LogLevel Level, string Message)> entries = [];

		public List<(LogLevel Level, string Message)> Entries
		{
			get
			{
				lock (entries)
				{
					return [.. entries];
				}
			}
		}

		public IDisposable? BeginScope<TState>(TState state)
			where TState : notnull =>
			null;

		public bool IsEnabled(LogLevel logLevel) =>
			true;

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{
			lock (entries)
			{
				entries.Add((logLevel, formatter(state, exception)));
			}
		}
	}
}
