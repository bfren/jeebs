using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Tables;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query wrapper
	/// </summary>
	public sealed partial class Query : Data.Query
	{
		/// <summary>
		/// IWpDb
		/// </summary>
		private readonly IWpDb db;

		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="db">IWpDb</param>
		public Query(IWpDb db) : base(db) => this.db = db;
	}
}
