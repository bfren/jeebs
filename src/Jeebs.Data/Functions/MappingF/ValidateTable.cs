// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs;
using Jeebs.Data.Entities;
using Jeebs.Data.Mapping;

namespace F.DataF
{
	/// <summary>
	/// Mapping Functions
	/// </summary>
	public static partial class MappingF
	{
		/// <summary>
		/// Validate that the properties on the entity and the columns on the table match
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="table">Table object</param>
		public static (bool valid, List<string> errors) ValidateTable<TEntity>(ITable table)
			where TEntity : IWithId
		{
			// Get types
			var tableType = table.GetType();
			var entityType = typeof(TEntity);

			// Get the table field names
			var tablePropertyNames = from p in tableType.GetProperties()
									 where p.PropertyType.IsPublic
									 select p.Name;

			// Get the entity property names
			var entityPropertyNames = from p in entityType.GetProperties()
									  where p.GetCustomAttribute<IgnoreAttribute>() == null
									  select p.Name;

			// Compare the table columns with the entity properties
			var errors = new List<string>();

			// Check for missing table columns
			var missingTableFields = entityPropertyNames.Except(tablePropertyNames);
			if (missingTableFields.Any())
			{
				foreach (var field in missingTableFields)
				{
					errors.Add($"The definition of table '{tableType.FullName}' is missing field '{field}'.");
				}
			}

			// Check for missing entity properties
			var missingEntityProperties = tablePropertyNames.Except(entityPropertyNames);
			if (missingEntityProperties.Any())
			{
				foreach (var property in missingEntityProperties)
				{
					errors.Add($"The definition of entity '{entityType.FullName}' is missing property '{property}'.");
				}
			}

			// If there are any errors, return false and list errors on new lines
			if (errors.Count > 0)
			{
				return (false, errors);
			}

			// Return valid with no errors
			return (true, new());
		}
	}
}
