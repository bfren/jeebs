// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// Create option types
	/// </summary>
	public static partial class Option
	{
		/// <summary>
		/// Exception handler - takes exception and returns message of type <see cref="IExceptionMsg"/>
		/// </summary>
		/// <param name="e">Exception</param>
		public delegate IExceptionMsg Handler(Exception e);

		/// <summary>
		/// Starts an Option chain, if using <see cref="Wrap{T}(T, bool)"/> is not appropriate
		/// </summary>
		public static Option<bool> Chain =>
			True;

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
