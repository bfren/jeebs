using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Join for Query builder
	/// </summary>
	public interface IFluentQueryJoin<T>
	{
		/// <summary>
		/// Set the ON column
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="column">Column name</param>
		/// <returns>IQueryJoin object</returns>
		IFluentQueryJoin<T> On(ITable table, string column);

		/// <summary>
		/// Set the EQUALS column
		/// </summary>
		/// <param name="column">Column name</param>
		/// <returns>IQuery object</returns>
		IFluentQuery<T> Equals(string column);
	}
}
