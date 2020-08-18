using Jeebs.WordPress.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jeebs.WordPress.TypeHandlers
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
		public override Taxonomy Parse(object value)
			=> Taxonomy.Parse(value.ToString());

		/// <summary>
		/// Set the Taxonomy table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">Taxonomy value</param>
		public override void SetValue(IDbDataParameter parameter, Taxonomy value)
			=> parameter.Value = value.ToString();
	}
}
