using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// IUnitOfWork extensions
	/// </summary>
	public static partial class UnitOfWorkExtensions
	{
		/// <summary>
		/// Shorthand for Table[].ExtractColumns and then IAdapter.Join
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="this">IUnitOfWork</param>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		public static string Extract<T>(this IUnitOfWork @this, params Table[] tables)
			=> @this.Adapter.ExtractEscapeAndJoinColumns<T>(tables);
	}
}
