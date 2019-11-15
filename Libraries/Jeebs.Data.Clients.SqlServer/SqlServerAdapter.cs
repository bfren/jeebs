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
		/// Setup object
		/// </summary>
		public SqlServerAdapter() : base('.', '[', ']') { }

		public override string CreateSingleAndReturnId<T>()
		{
			throw new NotImplementedException();
		}

		public override string RetrieveSingleById<T>(int id)
		{
			throw new NotImplementedException();
		}

		public override string UpdateSingle<T>(int id, long? version = null)
		{
			throw new NotImplementedException();
		}

		public override string DeleteSingle<T>(int id)
		{
			throw new NotImplementedException();
		}
	}
}