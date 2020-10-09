using System;
using System.Collections.Generic;
using System.Text;

namespace F
{
	public static class Rand
	{
		public static string String
			=> StringF.Random(6);

		public static int Integer
			=> MathsF.RandomInt32(max: 1000);
	}
}
