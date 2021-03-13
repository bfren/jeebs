// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: AuditAsync
	/// </summary>
	public static class OptionExtensions_AuditAsync
	{
		/// <inheritdoc cref="Option{T}.DoAuditAsync(Func{Option{T}, Task})"/>
		internal static Task<Option<T>> DoAuditAsync<T>(Task<Option<T>> @this, Func<Option<T>, Task> audit) =>
			F.OptionF.AuditAsync(@this, audit);

		/// <inheritdoc cref="DoAuditAsync{T}(Task{Option{T}}, Func{Option{T}, Task})"/>
		public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Action<Option<T>> audit) =>
			F.OptionF.AuditAsync(@this, x => { audit(x); return Task.CompletedTask; });

		/// <inheritdoc cref="DoAuditAsync{T}(Task{Option{T}}, Func{Option{T}, Task})"/>
		public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Func<Option<T>, Task> audit) =>
			F.OptionF.AuditAsync(@this, audit);
	}
}
