using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Link.Single
{
	/// <summary>
	/// Used in <see cref="ILink{TValue}.Single{TSingle}"/> when there is more than one item in the list
	/// </summary>
	public sealed class MoreThanOneItemMsg : IMsg
	{
		/// <summary>
		/// Create Message
		/// </summary>
		public MoreThanOneItemMsg() { }
	}
}
