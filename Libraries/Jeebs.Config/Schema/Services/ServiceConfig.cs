// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Config
{
	/// <summary>
	/// Service configuration
	/// </summary>
	public abstract record ServiceConfig
	{
		/// <summary>
		/// Whether or not this service configuration is valid
		/// </summary>
		public abstract bool IsValid { get; }
	}
}
