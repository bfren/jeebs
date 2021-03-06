﻿// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Data
{
	/// <summary>
	/// Enables agnostic interaction with a database
	/// </summary>
	public interface IDb
	{
		/// <summary>
		/// Create new IUnitOfWork
		/// </summary>
		IUnitOfWork UnitOfWork { get; }
	}
}
