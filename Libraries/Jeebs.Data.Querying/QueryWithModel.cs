// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Data.Querying
{
	public sealed partial class QueryBuilder<TModel>
	{
		/// <inheritdoc cref="IQueryWithModel{TModel}"/>
		public sealed class QueryWithModel : IQueryWithModel<TModel>
		{
			/// <summary>
			/// IUnitOfWork
			/// </summary>
			internal IUnitOfWork UnitOfWork { get; }

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="unitOfWork">IUnitOfWork</param>
			internal QueryWithModel(IUnitOfWork unitOfWork) =>
				UnitOfWork = unitOfWork;

			/// <inheritdoc/>
			public IQueryWithOptions<TModel, TOptions> WithOptions<TOptions>(TOptions options)
				where TOptions : QueryOptions =>
				new QueryWithOptions<TOptions>(UnitOfWork, options);

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
