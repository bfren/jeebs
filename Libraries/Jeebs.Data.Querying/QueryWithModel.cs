using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public sealed partial class QueryBuilder<TModel>
	{
		/// <inheritdoc cref="IQueryWithModel{TModel}"/>
		public sealed class QueryWithModel : IQueryWithModel<TModel>
		{
			/// <summary>
			/// IUnitOfWork
			/// </summary>
			private readonly IUnitOfWork unitOfWork;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="unitOfWork">IUnitOfWork</param>
			internal QueryWithModel(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

			/// <inheritdoc/>
			public IQueryWithOptions<TModel, TOptions> WithOptions<TOptions>(TOptions options)
				where TOptions : QueryOptions, new()
			{
				return new QueryWithOptions<TOptions>(unitOfWork, options);
			}

			/// <inheritdoc/>
			public IQueryWithOptions<TModel, TOptions> WithOptions<TOptions>(Action<TOptions>? modify = null)
				where TOptions : QueryOptions, new()
			{
				// Create options
				var options = new TOptions();
				modify?.Invoke(options);

				// Pass to next stage
				return WithOptions(options);
			}
		}
	}
}
