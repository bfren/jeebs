using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Table
	/// </summary>
	public interface ITable
	{
		/// <summary>
		/// Extract the column names (unescaped) for the table entity
		/// </summary>
		ExtractedColumns Extract();

		/// <summary>
		/// Extract the column names (unescaped) for the model type
		/// </summary>
		/// <typeparam name="TModel">Model Type</typeparam>
		ExtractedColumns Extract<TModel>();

		/// <summary>
		/// Table name (unescaped)
		/// </summary>
		string ToString();
	}
}
