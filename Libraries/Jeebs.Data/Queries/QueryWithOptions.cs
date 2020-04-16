using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public sealed partial class QueryBuilder
	{
		/// <summary>
		/// Saves query options (stage 2) and enables stage 3: build query parts
		/// </summary>
		/// <typeparam name="TOptions">QueryOptions</typeparam>
		public sealed class QueryWithOptions<TOptions>
			where TOptions : QueryOptions
		{
			/// <summary>
			/// IUnitOfWork
			/// </summary>
			private readonly IUnitOfWork unitOfWork;

			/// <summary>
			/// TOptions
			/// </summary>
			private readonly TOptions options;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="unitOfWork">IUnitOfWork</param>
			/// <param name="options">TOptions</param>
			internal QueryWithOptions(IUnitOfWork unitOfWork, TOptions options)
			{
				this.unitOfWork = unitOfWork;
				this.options = options;
			}

			/// <summary>
			/// Query Stage 3: Build the query parts
			/// </summary>
			/// <param name="build">Function to return QueryParts</param>
			public Query<TModel> BuildParts<TModel>(Func<TOptions, QueryParts<TModel>> build)
			{
				return new Query<TModel>(unitOfWork, build(options));
			}
		}
	}
}
