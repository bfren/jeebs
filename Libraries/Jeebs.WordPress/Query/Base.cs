using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Jeebs.Data;
using Jeebs.Data.Enums;

namespace Jeebs.WordPress
{
	public static partial class Query
	{
		/// <summary>
		/// Provides base options and methods for Queries
		/// </summary>
		public abstract class Base<TOptions> : Data.Query
			where TOptions : BaseOptions
		{
			/// <summary>
			/// IWpDb
			/// </summary>
			public IWpDb WpDb { get; }

			/// <summary>
			/// Setup object
			/// </summary>
			/// <param name="wpDb">IWpDb</param>
			/// <param name="unitOfWork">[Optional] IUnitOfWork</param>
			protected Base(IWpDb wpDb, IUnitOfWork? unitOfWork = null) : base(() => wpDb.UnitOfWork, unitOfWork) => WpDb = wpDb;

			/// <summary>
			/// Build the query using default options
			/// </summary>
			/// <typeparam name="T">Entity type</typeparam>
			/// <param name="modifyOptions">[Optional] Action to modify default options</param>
			public abstract void Build<T>(Action<TOptions>? modifyOptions = null)
				where T : IEntity;
		}
	}
}
