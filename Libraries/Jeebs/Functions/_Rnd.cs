using System;
using System.Collections.Generic;
using System.Text;

namespace F
{
	public static class Rnd
	{
		public static string String { get => StringF.Random(6); }

		public static int Integer
			=> MathsF.RandomInt32(max: 1000);
	}
}
