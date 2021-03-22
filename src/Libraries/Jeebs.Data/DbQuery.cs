// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbQuery"/>
	public abstract class DbQuery : IDbQuery
	{
		/// <summary>
		/// IDb
		/// </summary>
		protected IDb Db { get; }

		/// <summary>
		/// ILog (should be given a context of the implementing class)
		/// </summary>
		protected ILog Log { get; }

		/// <summary>
		/// Inject database and log objects
		/// </summary>
		/// <param name="db">IDb</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		protected DbQuery(IDb db, ILog log) =>
			(Db, Log) = (db, log);

		/// <summary>
		/// Use Verbose log by default - override to send elsewhere (or to disable entirely)
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Log message arguments</param>
		protected virtual void WriteToLog(string message, object[] args) =>
			Log.Verbose(message, args);
	}
}
