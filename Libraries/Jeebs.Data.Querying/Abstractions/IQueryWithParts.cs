using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Saves query parts (stage 3) and enables stage 4: get query object
	/// </summary>
	/// <typeparam name="T">Model Type</typeparam>
	public interface IQueryWithParts<T>
	{
		/// <summary>
		/// Query Stage 4: Get query object
		/// </summary>
		public IQuery<T> GetQuery();
	}
}
