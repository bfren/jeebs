// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace F
{
	public static partial class Rnd
	{
		public static partial class BooleanF
		{
			/// <summary>
			/// Returns a random true or false value
			/// </summary>
			public static bool Get() =>
				NumberF.GetInt64(0, 1) switch
				{
					0 =>
						false,

					_ =>
						true
				};
		}
	}
}
