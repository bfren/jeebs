// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// Create <see cref="Option{T}"/> types and begin chains
	/// </summary>
	public static partial class Option
	{
		/// <summary>
		/// Exception handler delegate - takes exception and returns message of type <see cref="IExceptionMsg"/>
		/// </summary>
		/// <param name="e">Exception</param>
		public delegate IExceptionMsg Handler(Exception e);

		/// <summary>
		/// Special case for boolean - returns Some{bool}(true)
		/// </summary>
		public static Option<bool> True =>
			Wrap(true);

		/// <summary>
		/// Special case for boolean - returns Some{bool}(false)
		/// </summary>
		public static Option<bool> False =>
			Wrap(false);
	}
}
