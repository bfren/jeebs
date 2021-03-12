// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace JeebsF
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: AuditAsync
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <inheritdoc cref="Option{T}.DoAuditAsync(Func{Option{T}, Task})"/>
		internal static async Task<Option<T>> DoAuditAsync<T>(Task<Option<T>> @this, Func<Option<T>, Task> audit) =>
			await (await @this).DoAuditAsync(audit);

		/// <inheritdoc cref="Option{T}.DoAuditAsync(Func{Option{T}, Task})"/>
		public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Action<Option<T>> audit) =>
			DoAuditAsync(@this, x => { audit(x); return Task.CompletedTask; });

		/// <inheritdoc cref="Option{T}.DoAuditAsync(Func{Option{T}, Task})"/>
		public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Func<Option<T>, Task> audit) =>
			DoAuditAsync(@this, audit);
	}
}
