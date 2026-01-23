// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;

namespace Jeebs.Data.Map.Functions;

/// <summary>
/// Mapping Functions.
/// </summary>
public static partial class MapF
{
	/// <summary>
	/// Validate that the properties on the entity and the columns on the table match.
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public static (bool valid, List<FailValue> errors) ValidateTable<TTable, TEntity>()
		where TTable : ITable
		where TEntity : IWithId
	{
		// Get types
		var tableType = typeof(TTable);
		var entityType = typeof(TEntity);

		// Get the table field names
		static IEnumerable<string> getPropertyNames(Type type) =>
			from p in type.GetProperties()
			where p.PropertyType.IsPublic
			&& p.GetCustomAttribute<IgnoreAttribute>() is null
			select p.Name;

		var tablePropertyNames = getPropertyNames(tableType);
		var entityPropertyNames = getPropertyNames(entityType);

		// Compare the table columns with the entity properties
		var errors = new List<FailValue>();

		// Check for missing table columns
		var missingTableFields = entityPropertyNames.Except(tablePropertyNames);
		if (missingTableFields.Any())
		{
			foreach (var field in missingTableFields)
			{
				var f = FailValue.Create(
					"The definition of table '{Table}' is missing field '{Field}'.",
					tableType.Name, field
				);
				errors.Add(f);
			}
		}

		// Check for missing entity properties
		var missingEntityProperties = tablePropertyNames.Except(entityPropertyNames);
		if (missingEntityProperties.Any())
		{
			foreach (var property in missingEntityProperties)
			{
				var f = FailValue.Create(
					"The definition of entity '{Entity}' is missing property '{Property}'.",
					entityType.Name, property
				);
				errors.Add(f);
			}
		}

		// If there are any errors, return false and list errors on new lines
		if (errors.Count > 0)
		{
			return (false, errors);
		}

		// Return valid with no errors
		return (true, []);
	}
}
