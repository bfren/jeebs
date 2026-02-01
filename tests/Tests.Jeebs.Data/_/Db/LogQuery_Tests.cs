// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Db_Tests;

public class LogQuery_Tests : Db_Setup
{
	public class With_Null_Param
	{
		[Fact]
		public void Sends_MessageTo_Verbose_Log()
		{
			// Arrange
			var (db, v) = Setup();
			var message = Rnd.Str;

			// Act
			db.LogQueryTest<Guid>((message, null));

			// Assert
			v.Log.Received().Vrb(
				"Query Returns: {Return} | {Query}",
				typeof(Guid), message
			);
		}
	}

	public class With_Param
	{
		[Fact]
		public void Sends_Message_And_Args_To_Verbose_Log()
		{
			// Arrange
			var (db, v) = Setup();
			var message = Rnd.Str;
			var args = new { one = Rnd.Int, two = Rnd.Int };

			// Act
			db.LogQueryTest<Guid>((message, args));

			// Assert
			v.Log.Received().Vrb(
				"Query Returns: {Return} | {Query} | Parameters: {Param}",
				typeof(Guid), message, args.ToString()
			);
		}
	}
}
