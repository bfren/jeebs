// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Unwrap the value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		internal T DoUnwrap(Func<IMsg?, T> ifNone) =>
			Switch(
				some: v => v,
				none: ifNone
			);

		/// <summary>
		/// Unwrap the value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(T ifNone) =>
			DoUnwrap(
				ifNone: _ => ifNone
			);

		/// <summary>
		/// Unwrap the value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<T> ifNone) =>
			DoUnwrap(
				ifNone: _ => ifNone()
			);

		/// <summary>
		/// Unwrap the value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<IMsg?, T> ifNone) =>
			DoUnwrap(
				ifNone: ifNone
			);
	}
}
