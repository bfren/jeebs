// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Linq;

/// <summary>
/// <see cref="Option{T}"/> Extensions: Linq Methods
/// </summary>
public static partial class OptionExtensions
{
	/// <summary>
	/// Enables LINQ select many on Option objects, e.g.
	/// <code>from x in Option<br/>
	/// from y in Option<br/>
	/// select y</code>
	/// </summary>
	/// <typeparam name="T">Option type</typeparam>
	/// <typeparam name="U">Interim type</typeparam>
	/// <typeparam name="V">Return type</typeparam>
	/// <param name="this">Option</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Option<V> SelectMany<T, U, V>(this Option<T> @this, Func<T, Option<U>> f, Func<T, U, V> g) =>
		F.OptionF.Bind(@this,
			x =>
				f(x)
					.Map(y => g(x, y), F.OptionF.DefaultHandler)
		);

	/// <inheritdoc cref="SelectMany{T, U, V}(Option{T}, Func{T, Option{U}}, Func{T, U, V})"/>
	public static Task<Option<V>> SelectMany<T, U, V>(this Option<T> @this, Func<T, Task<Option<U>>> f, Func<T, U, V> g) =>
		F.OptionF.BindAsync(@this,
			x =>
				f(x)
					.MapAsync(y => g(x, y), F.OptionF.DefaultHandler)
		);

	/// <inheritdoc cref="SelectMany{T, U, V}(Option{T}, Func{T, Option{U}}, Func{T, U, V})"/>
	/// <param name="this">Option (awaitable)</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Task<Option<V>> SelectMany<T, U, V>(this Task<Option<T>> @this, Func<T, Option<U>> f, Func<T, U, V> g) =>
		F.OptionF.BindAsync(@this,
			x =>
				Task.FromResult(
					f(x)
						.Map(y => g(x, y), F.OptionF.DefaultHandler)
				)
		);

	/// <inheritdoc cref="SelectMany{T, U, V}(Option{T}, Func{T, Option{U}}, Func{T, U, V})"/>
	/// <param name="this">Option (awaitable)</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Task<Option<V>> SelectMany<T, U, V>(this Task<Option<T>> @this, Func<T, Task<Option<U>>> f, Func<T, U, V> g) =>
		F.OptionF.BindAsync(@this,
			x =>
				f(x)
					.MapAsync(y => g(x, y), F.OptionF.DefaultHandler)
		);
}
