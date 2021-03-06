// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs;

namespace Jm.Link.Unwrap
{
	/// <summary>
	/// Used in <see cref="ILink{TValue}.UnwrapSingle{TSingle}"/> when the type requested doesn't match the containing list
	/// </summary>
	public sealed class IncorrectTypeMsg : IMsg { }
}
