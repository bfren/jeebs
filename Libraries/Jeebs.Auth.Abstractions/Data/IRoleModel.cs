// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Role model - allows consistent interaction in user interface
	/// </summary>
	public interface IRoleModel
	{
		/// <summary>
		/// Rple ID
		/// </summary>
		public RoleId RoleId { get; init; }

		/// <summary>
		/// Role name
		/// </summary>
		string Name { get; init; }
	}
}
