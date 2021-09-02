// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// <see cref="Option{T}"/> Extensions: IfNullAsync
/// </summary>
public static class OptionExtensions_IfNullAsync
{
	/// <inheritdoc cref="F.OptionF.IfNullAsync{T, TMsg}(Task{Option{T}}, Func{TMsg})"/>
	public static Task<Option<T>> IfNullAsync<T>(this Task<Option<T>> @this, Func<Option<T>> ifNull) =>
		F.OptionF.IfNullAsync(@this, () => Task.FromResult(ifNull()));

	/// <inheritdoc cref="F.OptionF.IfNullAsync{T, TMsg}(Task{Option{T}}, Func{TMsg})"/>
	public static Task<Option<T>> IfNullAsync<T>(this Task<Option<T>> @this, Func<Task<Option<T>>> ifNull) =>
		F.OptionF.IfNullAsync(@this, ifNull);

	/// <inheritdoc cref="F.OptionF.IfNullAsync{T, TMsg}(Task{Option{T}}, Func{TMsg})"/>
	public static Task<Option<T>> IfNullAsync<T, TMsg>(this Task<Option<T>> @this, Func<TMsg> ifNull)
		where TMsg : IMsg =>
		F.OptionF.IfNullAsync(@this, ifNull);
}
