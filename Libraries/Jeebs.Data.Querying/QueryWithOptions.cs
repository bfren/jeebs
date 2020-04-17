using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public sealed partial class QueryBuilder<TModel>
	{
		/// <summary>
		/// Saves query options (stage 2) and enables stage 3: build query parts
		/// </summary>
		/// <typeparam name="TOptions">QueryOptions</typeparam>
		public sealed class QueryWithOptions<TOptions> : IQueryWithOptions<TModel, TOptions>
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
			/// Query Stage 3: Use existing query parts
			/// </summary>
			/// <param name="parts">IQueryParts</param>
			public IQueryWithParts<TModel> WithParts(IQueryParts parts)
			{
				return new QueryWithParts(unitOfWork, parts);
			}

			/// <summary>
			/// Query Stage 3: Build the query parts
			/// </summary>
			/// <param name="builder">IQueryPartsBuilder</param>
			public IQueryWithParts<TModel> WithParts(IQueryPartsBuilder<TModel, TOptions> builder)
			{
				return WithParts(builder.Build(options));
			}
		}
	}
}
