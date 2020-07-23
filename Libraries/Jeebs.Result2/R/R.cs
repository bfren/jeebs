﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="IR{TValue}"/>
	public abstract class R<TValue> : IR<TValue>
	{
		/// <inheritdoc/>
		public MsgList Messages { get; internal set; }

		internal protected R() => Messages = new MsgList();

		/// <summary>
		/// Clear all messages
		/// </summary>
		public virtual void Dispose() => Messages.Clear();

		/// <inheritdoc/>
		public IError<TValue> SkipAhead() => SkipAhead<TValue>();

		/// <inheritdoc cref="IR.SkipAhead{TValue}"/>
		public IError<TNext> SkipAhead<TNext>() => this switch
		{
			IError<TNext> e => e,
			_ => new RError<TNext> { Messages = Messages }
		};

		#region Explicit implementations

		IError IR.SkipAhead() => SkipAhead();

		#endregion
	}
}
