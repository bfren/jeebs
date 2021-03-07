// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Data
{
	public abstract partial class Adapter
	{
		/// <summary>
		/// Checks for <see cref="CreateSingleAndReturnId(string, List{string}, List{string})"/>
		/// </summary>
		/// <param name="table"></param>
		/// <param name="columns"></param>
		/// <param name="aliases"></param>
		protected void CreateSingleAndReturnIdChecks(string table, List<string> columns, List<string> aliases)
		{
			// Handle invalid table
			if (IsInvalidIdentifier(table))
			{
				throw new InvalidOperationException($"Table is invalid: '{table}'.");
			}

			// Handle empty columns
			if (columns.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(columns)} cannot be empty.");
			}

			// Handle empty aliases
			if (aliases.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(aliases)} cannot be empty.");
			}

			// Columns and aliases must contain the same number of items
			if (columns.Count != aliases.Count)
			{
				throw new InvalidOperationException($"The number of {nameof(columns)} ({columns.Count}) and {nameof(aliases)} ({aliases.Count}) must be the same.");
			}
		}

		/// <summary>
		/// Checks for <see cref="Retrieve(IQueryParts)"/>
		/// </summary>
		/// <param name="parts"></param>
		protected void RetrieveChecks(IQueryParts parts)
		{
			// Make sure FROM is not empty
			if (IsInvalidIdentifier(parts.From))
			{
				throw new InvalidOperationException($"Table is invalid: '{parts.From}'.");
			}
		}

		/// <summary>
		/// Checks for <see cref="RetrieveSingleById(string, List{string}, string, string)"/>
		/// </summary>
		/// <param name="table"></param>
		/// <param name="columns"></param>
		/// <param name="idColumn"></param>
		/// <param name="idAlias"></param>
		protected void RetrieveSingleByIdChecks(string table, List<string> columns, string idColumn, string? idAlias = null)
		{
			// Handle invalid table
			if (IsInvalidIdentifier(table))
			{
				throw new InvalidOperationException($"Table is invalid: '{table}'.");
			}

			// Handle empty columns
			if (columns.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(columns)} cannot be empty.");
			}

			// Handle invalid ID column
			if (IsInvalidIdentifier(idColumn))
			{
				throw new InvalidOperationException($"ID Column is invalid: '{idColumn}'.");
			}

			// Handle invalid ID alias
			if (IsInvalidIdentifier(idAlias))
			{
				throw new InvalidOperationException($"ID Alias is invalid: '{idAlias}'.");
			}
		}

		/// <summary>
		/// Checks for <see cref="UpdateSingle(string, List{string}, List{string}, string, string, string?, string?)"/>
		/// </summary>
		/// <param name="table"></param>
		/// <param name="columns"></param>
		/// <param name="aliases"></param>
		/// <param name="idColumn"></param>
		/// <param name="idAlias"></param>
		protected void UpdateSingleChecks(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias)
		{
			// Handle invalid table
			if (IsInvalidIdentifier(table))
			{
				throw new InvalidOperationException($"Table is invalid: '{table}'.");
			}

			// Handle empty columns
			if (columns.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(columns)} cannot be empty.");
			}

			// Handle empty aliases
			if (aliases.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(aliases)} cannot be empty.");
			}

			// Columns and aliases must contain the same number of items
			if (columns.Count != aliases.Count)
			{
				throw new InvalidOperationException($"The number of {nameof(columns)} ({columns.Count}) and {nameof(aliases)} ({aliases.Count}) must be the same.");
			}

			// Handle invalid ID column
			if (IsInvalidIdentifier(idColumn))
			{
				throw new InvalidOperationException($"ID Column is invalid: '{idColumn}'.");
			}

			// Handle invalid ID Alias
			if (IsInvalidIdentifier(idAlias))
			{
				throw new InvalidOperationException($"ID Alias is invalid: '{idAlias}'.");
			}
		}

		/// <summary>
		/// Checks for <see cref="DeleteSingle(string, string, string, string?, string?)"/>
		/// </summary>
		/// <param name="table"></param>
		/// <param name="idColumn"></param>
		/// <param name="idAlias"></param>
		protected void DeleteSingleChecks(string table, string idColumn, string idAlias)
		{
			// Handle invalid table
			if (IsInvalidIdentifier(table))
			{
				throw new InvalidOperationException($"Table is invalid: '{table}'.");
			}

			// Handle invalid ID column
			if (IsInvalidIdentifier(idColumn))
			{
				throw new InvalidOperationException($"ID Column is invalid: '{idColumn}'.");
			}

			// Handle invalid ID Alias
			if (IsInvalidIdentifier(idAlias))
			{
				throw new InvalidOperationException($"ID Alias is invalid: '{idAlias}'.");
			}
		}
	}
}
