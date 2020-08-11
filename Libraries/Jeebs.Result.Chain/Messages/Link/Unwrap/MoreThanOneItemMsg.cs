using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Link.Unwrap
{
	/// <summary>
	/// Used in <see cref="ILink{TValue}.UnwrapSingle{TSingle}"/> when there is more than one item in the list
	/// </summary>
	public sealed class MoreThanOneItemMsg : IMsg { }
}
