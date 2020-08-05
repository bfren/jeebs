using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Link.Single
{
	/// <summary>
	/// Used in <see cref="ILink{TValue}.Single{TSingle}"/> when the type requested doesn't match the containing list
	/// </summary>
	public sealed class IncorrectTypeMsg : IMsg
	{
		/// <summary>
		/// Create Message
		/// </summary>
		public IncorrectTypeMsg() { }
	}
}
