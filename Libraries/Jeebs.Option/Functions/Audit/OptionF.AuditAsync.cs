// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}})"/>
		public static async Task<Option<T>> AuditAsync<T>(Option<T> option, Func<Option<T>, Task> audit)
		{
			// Perform the audit
			try
			{
				await audit(option);
			}
			catch (Exception e)
			{
				HandleAuditException(e);
			}

			// Return the original object
			return option;
		}

		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}})"/>
		public static async Task<Option<T>> AuditAsync<T>(Task<Option<T>> option, Func<Option<T>, Task> audit) =>
			await AuditAsync(await option, audit);
	}
}
