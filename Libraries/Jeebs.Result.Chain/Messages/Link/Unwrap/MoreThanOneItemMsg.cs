// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs;

namespace Jm.Link.Unwrap
{
	/// <summary>
	/// Used in <see cref="ILink{TValue}.UnwrapSingle{TSingle}"/> when there is more than one item in the list
	/// </summary>
	public sealed class MoreThanOneItemMsg : IMsg { }
}
