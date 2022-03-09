// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Random;

public static partial class Rnd
{
	public static partial class GuidF
	{
		/// <summary>
		/// Return a secure random Guid
		/// </summary>
		public static Guid Get() =>
			new(ByteF.Get(16));
	}
}