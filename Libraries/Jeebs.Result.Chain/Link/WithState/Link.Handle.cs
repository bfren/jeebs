using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Fluent;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		/// <inheritdoc/>
		new public Handle<TValue, TState, Exception> Handle() =>
			new(this);

		/// <inheritdoc/>
		new public Handle<TValue, TState, TException> Handle<TException>()
			where TException : Exception =>
			new(this);
	}
}
