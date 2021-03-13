// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Returns an enumerator to enable use in foreach blocks
		/// </summary>
		public IEnumerator<T> GetEnumerator()
		{
			if (this is Some<T> some)
			{
				yield return some.Value;
			}
		}
	}
}
