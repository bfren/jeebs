using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Build the parts required to create a query
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <typeparam name="TOptions">QueryOptions</typeparam>
	public interface IQueryPartsBuilder<TModel, TOptions>
		where TOptions : QueryOptions
	{
		/// <summary>
		/// IAdapter
		/// </summary>
		IAdapter Adapter { get; }

		/// <summary>
		/// Build the query
		/// </summary>
		/// <param name="opt">TOptions</param>
		IQueryParts Build(TOptions opt);

		/// <summary>
		/// Shorthand for IAdapter.SplitAndEscape
		/// </summary>
		/// <param name="element">The element to split and escape</param>
		string Escape(object element);

		/// <summary>
		/// Shorthand for IAdapter.EscapeAndJoin
		/// </summary>
		/// <param name="elements">The elements to escape and join</param>
		string Escape(params object?[] elements);
	}
}
