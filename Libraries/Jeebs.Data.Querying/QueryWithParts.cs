using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public sealed partial class QueryBuilder<TModel>
	{
		/// <summary>
		/// Saves query parts (stage 3) and enables stage 4: get query object
		/// </summary>
		public sealed class QueryWithParts : IQueryWithParts<TModel>
		{
			/// <summary>
			/// IUnitOfWork
			/// </summary>
			private readonly IUnitOfWork unitOfWork;

			/// <summary>
			/// IQueryParts
			/// </summary>
			private readonly IQueryParts parts;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="unitOfWork">IUnitOfWork</param>
			/// <param name="parts">TOptions</param>
			internal QueryWithParts(IUnitOfWork unitOfWork, IQueryParts parts)
			{
				this.unitOfWork = unitOfWork;
				this.parts = parts;
			}

			/// <summary>
			/// Query Stage 4: Get query object
			/// </summary>
			public IQuery<TModel> GetQuery()
			{
				return new Query<TModel>(unitOfWork, parts);
			}
		}
	}
}
