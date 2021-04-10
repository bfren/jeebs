// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Allows easy injection of different logs into Db instance
	/// </summary>
	public sealed record DbLogs
	{
		/// <summary>
		/// ILog for Db object
		/// </summary>
		public ILog<Db> DbLog { get; set; }

		/// <summary>
		/// ILog for UnitOfWork object
		/// </summary>
		public ILog<UnitOfWork> UnitOfWorkLog { get; set; }

		/// <summary>
		/// Inject logs
		/// </summary>
		/// <param name="dbLog">ILog for Db object</param>
		/// <param name="wLog">ILog for UnitOfWork object</param>
		public DbLogs(ILog<Db> dbLog, ILog<UnitOfWork> wLog) =>
			(DbLog, UnitOfWorkLog) = (dbLog, wLog);
	}
}
