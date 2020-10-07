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
	}
}
