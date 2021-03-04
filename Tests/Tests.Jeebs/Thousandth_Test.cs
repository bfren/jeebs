using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public class Thousandth_Test
	{
		[Fact]
		public void A_Thousand_Tests_Begins_With_A_Single_Fact()
		{
			// Arrange

			// Act
			var seconds = new TimeSpan(0, 16, 40);
			var tenCubed = Math.Pow(10, 3);
			const int binary = 0b1111101000;
			const int hexadecimal = 0x3E8;
			var primeFactors = Math.Pow(2, 3) * Math.Pow(5, 3);
			var greek = (Gk.Π - Gk.III) * (Gk.Δ * Gk.ΠΔ);

			// Assert
			Assert.Equal(1000, seconds.TotalSeconds);
			Assert.Equal(1000, tenCubed);
			Assert.Equal(1000, binary);
			Assert.Equal(1000, hexadecimal);
			Assert.Equal(1000, primeFactors);
			Assert.Equal(Gk.Christ.Value, greek);
		}

		public class Gk
		{
			public static Gk III => new(3);

			public static Gk Π => new(5);

			public static Gk Δ => new(10);

			public static Gk ΠΔ => new(50);

			public static Gk Christ => new(1000);

			public static int operator -(Gk a, Gk b)
				=> a.Value - b.Value;

			public static int operator *(Gk a, Gk b)
				=> a.Value * b.Value;

			public int Value { get; }

			public Gk(int value)
				=> Value = value;
		}
	}
}
