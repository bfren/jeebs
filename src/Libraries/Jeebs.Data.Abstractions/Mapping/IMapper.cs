// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data
{
	public interface IMapper
	{
		/// <summary>
		/// Map the specified <typeparamref name="TEntity"/> to the specified <paramref name="table"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="table">Table to map</param>
		ITableMap Map<TEntity>(ITable table)
			where TEntity : IEntity;

		/// <summary>
		/// Get table map for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		Option<ITableMap> GetTableMapFor<TEntity>()
			where TEntity : IEntity;
	}
}