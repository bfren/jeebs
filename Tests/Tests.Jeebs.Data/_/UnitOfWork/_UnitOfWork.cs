using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NSubstitute;

namespace Jeebs.Data.UnitOfWork_Tests
{
	public static class UnitOfWork
	{
		public static (IUnitOfWork, IDbConnection, IAdapter, ILog) GetUnitOfWork(
			IDbConnection? connection = null,
			IAdapter? adapter = null,
			ILog? log = null
		)
		{
			var c = connection ?? Substitute.For<IDbConnection>();
			var a = adapter ?? Substitute.For<IAdapter>();
			var l = log ?? Substitute.For<ILog>();
			return (new Data.UnitOfWork(c, a, l), c, a, l);
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
	}
}
