using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public interface IAdapterWithMapping
	{
		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string CreateSingleAndReturnId<T>();

		/// <summary>
		/// SELECT columns to return a COUNT query
		/// </summary>
		string GetSelectCount();

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string RetrieveSingleById<T>();

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string UpdateSingle<T>();

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		string DeleteSingle<T>();
	}
}
