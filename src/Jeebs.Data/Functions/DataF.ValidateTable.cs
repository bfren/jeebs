// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Data;

public static partial class DataF
{
	/// <summary>
	/// Validate that the properties on the entity and the columns on the table match.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <typeparam name="TEntity">Entity type.</typeparam>
	public static (bool valid, List<FailureValue> errors) ValidateTable<TTable, TEntity>()
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
		var errors = new List<FailureValue>();

		// Check for missing table columns
		var missingTableFields = entityPropertyNames.Except(tablePropertyNames);
		if (missingTableFields.Any())
		{
			foreach (var field in missingTableFields)
			{
				errors.Add(new(
					"The definition of table '{Table}' is missing field '{Field}'.",
					tableType.Name, field
				));
			}
		}

		// Check for missing entity properties
		var missingEntityProperties = tablePropertyNames.Except(entityPropertyNames);
		if (missingEntityProperties.Any())
		{
			foreach (var property in missingEntityProperties)
			{
				errors.Add(new(
					"The definition of entity '{Entity}' is missing property '{Property}'.",
					entityType.Name, property
				));
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
