using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query Builder
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	public sealed partial class QueryBuilder<TModel>
	{
		/// <summary>
		/// Saves query model (stage 1) and enables stage 2: save query options
		/// </summary>
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
			internal QueryWithModel(IUnitOfWork unitOfWork)
			{
				this.unitOfWork = unitOfWork;
			}

			/// <summary>
			/// Query Stage 2: Set the options for this query
			/// </summary>
			/// <typeparam name="TOptions">QueryOptions</typeparam>
			/// <param name="options">Options to use</param>
			public IQueryWithOptions<TModel, TOptions> WithOptions<TOptions>(TOptions options)
				where TOptions : QueryOptions, new()
			{
				return new QueryWithOptions<TOptions>(unitOfWork, options);
			}

			/// <summary>
			/// Query Stage 2: Set the options for this query
			/// </summary>
			/// <typeparam name="TOptions">QueryOptions</typeparam>
			/// <param name="modify">[Optional] Action to modify default options</param>
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
