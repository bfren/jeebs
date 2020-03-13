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
		public abstract class Base : Data.Query
		{
			/// <summary>
			/// IWpDb
			/// </summary>
			protected IWpDb WpDb { get; }

			/// <summary>
			/// Setup object
			/// </summary>
			/// <param name="wpDb">IWpDb</param>
			/// <param name="unitOfWork">[Optional] IUnitOfWork</param>
			protected Base(IWpDb wpDb, IUnitOfWork? unitOfWork) : base(() => wpDb.UnitOfWork, unitOfWork) => WpDb = wpDb;
		}
	}
}
