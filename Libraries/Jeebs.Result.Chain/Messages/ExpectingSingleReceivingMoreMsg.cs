using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Used in <see cref="RExtensions_Single.Single{TResult, TValue}(TResult)"/> when there is more than one item in the list
	/// </summary>
	public sealed class ExpectingSingleReceivingMoreMsg : IMsg
	{
		/// <summary>
		/// Create Message
		/// </summary>
		public ExpectingSingleReceivingMoreMsg() { }
	}
}
