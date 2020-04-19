using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public sealed partial class QueryBuilder<TModel>
	{
		/// <inheritdoc cref="IQueryWithOptions{TModel, TOptions}"/>
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

			/// <inheritdoc/>
			public IQueryWithParts<TModel> WithParts(IQueryParts parts) => new QueryWithParts(unitOfWork, parts);

			/// <inheritdoc/>
			public IQueryWithParts<TModel> WithParts(IQueryPartsBuilder<TModel, TOptions> builder) => WithParts(builder.Build(options));
		}
	}
}
