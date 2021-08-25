// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
		public static async Task<Option<T>> AuditAsync<T>(Option<T> option, Func<Option<T>, Task>? any, Func<T, Task>? some, Func<IMsg, Task>? none)
		{
			// Do nothing if the user gave us nothing to do!
			if (any == null && some == null && none == null)
			{
				return option;
			}

			// Work out which audit function to use
			Func<Task> audit = Switch<T, Func<Task>>(
				option,
				some: v => () => some?.Invoke(v) ?? Task.CompletedTask,
				none: r => () => none?.Invoke(r) ?? Task.CompletedTask
			);

			// Perform the audit
			try
			{
				if (any != null)
				{
					await any(option);
				}

				await audit();
			}
			catch (Exception e)
			{
				HandleAuditException(e);
			}

			// Return the original object
			return option;
		}

		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
		public static async Task<Option<T>> AuditAsync<T>(Task<Option<T>> option, Action<Option<T>>? any, Action<T>? some, Action<IMsg>? none) =>
			await AuditAsync(await option,
				x => { any?.Invoke(x); return Task.CompletedTask; },
				x => { some?.Invoke(x); return Task.CompletedTask; },
				x => { none?.Invoke(x); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="Audit{T}(Option{T}, Action{Option{T}}, Action{T}?, Action{IMsg}?)"/>
		public static async Task<Option<T>> AuditAsync<T>(Task<Option<T>> option, Func<Option<T>, Task>? any, Func<T, Task>? some, Func<IMsg, Task>? none) =>
			await AuditAsync(await option, any, some, none);
	}
}
