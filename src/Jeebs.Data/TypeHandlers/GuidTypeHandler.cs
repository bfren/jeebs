// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.TypeHandlers;

/// <summary>
/// Guid TypeHandler
/// </summary>
public sealed class GuidTypeHandler : Dapper.SqlMapper.TypeHandler<Guid>
{
	/// <summary>
	/// Parse Guid
	/// </summary>
	/// <param name="value">Guid value</param>
	/// <returns>Guid</returns>
	public override Guid Parse(object value) =>
		value?.ToString() switch
		{
			string guid when !string.IsNullOrWhiteSpace(guid) =>
				Guid.Parse(guid),

			_ =>
				Guid.Empty
		};

	/// <summary>
	/// Set Guid value
	/// </summary>
	/// <param name="parameter">IDbDataParameter</param>
	/// <param name="value">Guid value</param>
	public override void SetValue(IDbDataParameter parameter, Guid value) =>
		parameter.Value = value;
}
