﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// 32-bit Integer ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public abstract record IntId(int Value) : StrongId<int>(Value)
	{
		/// <summary>
		/// Default value
		/// </summary>
		public const int Default = 0;

		/// <inheritdoc cref="IStrongId.IsDefault"/>
		public override bool IsDefault =>
			Value == Default;

		/// <summary>
		/// Create with default value
		/// </summary>
		public IntId() : this(Default) { }
	}
}
