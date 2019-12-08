using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Clients.SqlServer
{
	/// <summary>
	/// SqlServer adapter
	/// </summary>
	public sealed class SqlServerAdapter : Adapter
	{
		/// <summary>
		/// Create object
		/// </summary>
		public SqlServerAdapter() : base('.', '[', ']') { }

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <returns>SQL query</returns>
		public override string CreateSingleAndReturnId<T>()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		/// <returns>SQL query</returns>
		public override string RetrieveSingleById<T>(int id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Id value</param>
		/// <param name="version">[Optional] Version</param>
		/// <returns>SQL query</returns>
		public override string UpdateSingle<T>(int id, long? version = null)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="id">Id value</param>
		/// <param name="version">[Optional] Version</param>
		/// <returns>SQL query</returns>
		public override string DeleteSingle<T>(int id, long? version = null)
		{
			throw new NotImplementedException();
		}
	}
}