using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public sealed partial class QueryBuilder<TModel>
	{
		/// <inheritdoc cref="IQueryWithParts{T}"/>
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
				=> (this.unitOfWork, this.parts) = (unitOfWork, parts);

			/// <inheritdoc/>
			public IQuery<TModel> GetQuery()
				=> new Query<TModel>(unitOfWork, parts);
		}
	}
}
