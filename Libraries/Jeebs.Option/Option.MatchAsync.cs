// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		internal Task<U> DoMatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: v => some(v), none: r => none(r));

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, U none) =>
			F.OptionF.MatchAsync(this, some: some, none: _ => Task.FromResult(none));

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Task<U> none) =>
			F.OptionF.MatchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none);

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Task<U> none) =>
			F.OptionF.MatchAsync(this, some: some, none: _ => none);

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<U> none) =>
			F.OptionF.MatchAsync(this, some: some, none: _ => Task.FromResult(none()));

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Func<Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none());

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: some, none: _ => none());

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, U> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: v => Task.FromResult(some(v)), none: none);

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, U> none) =>
			F.OptionF.MatchAsync(this, some: some, none: r => Task.FromResult(none(r)));

		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		public Task<U> MatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: some, none: none);
	}
}
