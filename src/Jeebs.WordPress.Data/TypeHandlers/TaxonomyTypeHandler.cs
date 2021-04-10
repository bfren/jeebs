// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Data.TypeHandlers
{
	/// <summary>
	/// Taxonomy TypeHandler
	/// </summary>
	public sealed class TaxonomyTypeHandler : Dapper.SqlMapper.TypeHandler<Taxonomy>
	{
		/// <summary>
		/// Parse the Taxonomy value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>Taxonomy object</returns>
		public override Taxonomy Parse(object value) =>
			value.ToString() switch
			{
				string taxonomy =>
					Taxonomy.Parse(taxonomy),

				_ =>
					Taxonomy.Blank
			};

		/// <summary>
		/// Set the Taxonomy table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">Taxonomy value</param>
		public override void SetValue(IDbDataParameter parameter, Taxonomy value) =>
			parameter.Value = value.ToString();
	}
}
