using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="ILink{TValue}"/>
	public partial class Link<TValue> : Link, ILink<TValue>
	{
		private readonly IR<TValue> result;

		internal Link(IR<TValue> result) : base(result)
			=> this.result = result;


	}
}
