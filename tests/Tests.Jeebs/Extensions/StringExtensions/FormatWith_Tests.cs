// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.StringExtensions_Tests;

public class FormatWith_Tests
{
	public class With_Invalid_Format_String
	{
		[Theory]
		[InlineData(null!)]
		[InlineData("")]
		[InlineData(" ")]
		public void Returns_Empty_String(string? input)
		{
			// Arrange

			// Act
			var result = F.Format(input!, Rnd.Int);

			// Assert
			Assert.Empty(result);
		}
	}

	public class With_Numbered_Placeholders
	{
		public class With_Single_Value_Source
		{
			[Fact]
			public void Replaces_With_Value()
			{
				// Arrange
				var format = "{0}/";
				var value = Rnd.Guid;

				// Act
				var result = format.FormatWith(value);

				// Assert
				Assert.Equal($"{value}/", result);
			}
		}

		public class With_Object_Source
		{
			[Fact]
			public void Returns_Formatted_String_With_Unmatched_Placeholders()
			{
				// Arrange
				const string format = "{0} , {1} , {2}";
				var values = new { zero = Rnd.Int, one = Rnd.Int, two = Rnd.Int };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{values} , {{1}} , {{2}}", result);
			}
		}

		public class With_Array_Source
		{
			[Fact]
			public void Replaces_With_Ordered_Values()
			{
				// Arrange
				const string format = "{0} , {1} , {2}";
				var v0 = Rnd.Int;
				var v1 = Rnd.Int;
				var v2 = Rnd.Int;
				var values = new[] { v0, v1, v2 };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{v0} , {v1} , {v2}", result);
			}

			[Fact]
			public void Replaces_With_Unordered_Values()
			{
				// Arrange
				const string format = "{1} , {0} , {2}";
				var v0 = Rnd.Int;
				var v1 = Rnd.Int;
				var v2 = Rnd.Int;
				var values = new[] { v0, v1, v2 };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{v1} , {v0} , {v2}", result);
			}

			[Fact]
			public void Replaces_Multiple_Values()
			{
				// Arrange
				const string format = "{1} , {0} , {2} , {0}";
				var v0 = Rnd.Int;
				var v1 = Rnd.Int;
				var v2 = Rnd.Int;
				var values = new[] { v0, v1, v2 };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{v1} , {v0} , {v2} , {v0}", result);
			}

			[Fact]
			public void Replaces_With_Formatted_Values()
			{
				// Arrange
				const string format = "{0:00} , {1:0.00} , {2:0,000.0}";
				var v0 = Rnd.Int;
				var v1 = Rnd.Int;
				var v2 = Rnd.Int;
				var values = new[] { v0, v1, v2 };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{v0:00} , {v1:0.00} , {v2:0,000.0}", result);
			}
		}
	}

	public class With_Named_Placeholders
	{
		public class With_Single_Value_Source
		{
			[Fact]
			public void Returns_Format()
			{
				// Arrange
				var format = "{bar}/";

				// Act
				var result = format.FormatWith(Rnd.Lng);

				// Assert
				Assert.Equal(format, result);
			}
		}

		public class With_Object_Source
		{
			[Fact]
			public void Replaces_With_Values()
			{
				// Arrange
				const string format = "{zero} , {@one} , {two}";
				var zero = Rnd.Int;
				var one = Rnd.Int;
				var two = Rnd.Int;
				var values = new { one, two, zero };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{zero} , {one} , {two}", result);
			}

			[Fact]
			public void Replaces_With_Formatted_Values()
			{
				// Arrange
				const string format = "{zero:00} , {one:0.00} , {two:0,000.0}";
				var zero = Rnd.Int;
				var one = Rnd.Int;
				var two = Rnd.Int;
				var values = new { one, two, zero };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{zero:00} , {one:0.00} , {two:0,000.0}", result);
			}

			[Fact]
			public void Replaces_Multiple_Values()
			{
				// Arrange
				const string format = "{two} , {zero} , {@one} , {two} , {one}";
				var zero = Rnd.Int;
				var one = Rnd.Int;
				var two = Rnd.Int;
				var values = new { one, two, zero };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{two} , {zero} , {one} , {two} , {one}", result);
			}
		}

		public class With_Array_Source
		{
			[Fact]
			public void Replaces_With_Values()
			{
				// Arrange
				const string format = "{zero} , {one} , {two:0.0}";
				var zero = Rnd.Int;
				var one = Rnd.Str;
				var two = (double)Rnd.Int;
				var values = new object[] { zero, one, two };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{zero} , {one} , {two:0.0}", result);
			}

			[Fact]
			public void Replaces_With_Formatted_Values()
			{
				// Arrange
				const string format = "{zero:00} , {one:0.00} , {2:0,000.0}";
				var zero = Rnd.Int;
				var one = Rnd.Int;
				var two = Rnd.Int;
				var values = new object[] { zero, one, two };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{zero:00} , {one:0.00} , {two:0,000.0}", result);
			}
		}
	}

	public class With_Mixed_Placeholders
	{
		public class With_Object_Source
		{
			[Fact]
			public void Returns_Formatted_String_With_Unmatched_Placeholders()
			{
				// Arrange
				const string format = "{zero} , {0} , {1}";
				var zero = Rnd.Int;
				var one = Rnd.Int;
				var two = Rnd.Int;
				var values = new { zero, one, two };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{zero} , {{0}} , {{1}}", result);
			}
		}

		public class With_Array_Source
		{
			[Fact]
			public void Replaces_With_Values()
			{
				// Arrange
				const string format = "{zero} , {0} , {1}";
				var zero = Rnd.Int;
				var v0 = Rnd.Int;
				var v1 = Rnd.Int;
				var values = new[] { zero, v0, v1 };

				// Act
				var result = format.FormatWith(values);

				// Assert
				Assert.Equal($"{zero} , {v0} , {v1}", result);
			}
		}
	}
}
