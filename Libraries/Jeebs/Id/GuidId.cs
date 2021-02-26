﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Guid ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public abstract record GuidId(Guid Value) : StrongId<Guid>(Value)
	{
		/// <summary>
		/// Default value
		/// </summary>
		public static Guid Default =>
			Guid.Empty;

		/// <inheritdoc cref="IStrongId.IsDefault"/>
		public override bool IsDefault =>
			Value == Default;

		/// <summary>
		/// Create with default value
		/// </summary>
		public GuidId() : this(Default) { }
	}
}