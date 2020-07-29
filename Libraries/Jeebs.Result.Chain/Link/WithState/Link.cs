using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="ILink{TValue, TState}"/>
	public partial class Link<TValue, TState> : Link<TValue>, ILink<TValue, TState>
	{
		private readonly IR<TValue, TState> result;

		internal Link(IR<TValue, TState> result) : base(result)
			=> this.result = result;

		new public void Dispose()
		{
			base.Dispose();
			result.Dispose();
		}
	}
}
