// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace F
{
	public static partial class Rnd
	{
		/// <summary>
		/// Random Guid function
		/// </summary>
		public static partial class GuidF
		{
			/// <summary>
			/// Return a secure random Guid
			/// </summary>
			public static Guid Get() =>
				new(ByteF.Get(16));
		}
	}
}
