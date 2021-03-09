// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Data.Mapping.Extensions.UnitOfWork
{
	/// <summary>
	/// See <see cref="Jeebs.Data.Mapping.UnitOfWorkExtensions.Create{T}(Jeebs.Data.IUnitOfWork, Jeebs.IOkV{T})"/>
	/// </summary>
	public sealed class CheckIdExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public CheckIdExceptionMsg(Exception e) : base(e) { }
	}
}
