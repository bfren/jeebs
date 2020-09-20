using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F.JsonF_Tests
{
	public class ReadJson_Tests
	{
		[Fact]
		public void Deserialise_Enumerated_Returns_Object_With_Value()
		{
			// Arrange
			const string value = "18";
			var json = $"\"{value}\"";

			// Act
			var result = JsonF.Deserialise<EnumeratedTest0>(json);

			// Assert
			var some = Assert.IsType<Some<EnumeratedTest0>>(result);
			Assert.Equal(value, some.Value.ToString());
		}

		[Fact]
		public void Deserialise_Null_Enumerated_Returns_Object_With_Empty_Value()
		{
			// Arrange
			var json = JsonF.Empty;

			// Act
			var result = JsonF.Deserialise<EnumeratedTest0>(json);

			// Assert
			var value = Assert.IsType<Some<EnumeratedTest0>>(result).Value;
			Assert.Equal(string.Empty, value.ToString());
		}

		[Fact]
		public void Deserialise_Object_With_Enumerated_Property_Returns_Object()
		{
			// Arrange
			const int id = 18;
			const string value = "7";
			var json = $"{{ \"id\": {id}, \"enumeratedValue\": \"{value}\" }}";

			// Act
			var result = JsonF.Deserialise<WrapperTest0>(json);

			// Assert
			var wrapper = Assert.IsType<Some<WrapperTest0>>(result).Value;
			Assert.Equal(id, wrapper.Id);
			Assert.Equal(value, wrapper.EnumeratedValue.ToString());
		}

		[Theory]
		[InlineData(null)]
		[InlineData("\"\"")]
		[InlineData("\"  \"")]
		public void Deserialise_Object_With_Enumerated_Property_But_Null_Value_And_Disallow_Empty_Returns_None(string input)
		{
			// Arrange
			var json = $"{{ \"enumeratedValue\": {input} }}";

			// Act
			var result = JsonF.Deserialise<WrapperTest1>(json);

			// Assert
			var none = Assert.IsType<None<WrapperTest1>>(result);
			Assert.IsType<Jm.Util.Json.DeserialiseExceptionMsg>(none.Reason);
		}

		public class EnumeratedTest0 : Enumerated
		{
			public EnumeratedTest0(string name) : base(name) { }
		}

		public class WrapperTest0
		{
			public int Id { get; set; }

			public EnumeratedTest0 EnumeratedValue { get; set; } = new EnumeratedTest0(string.Empty);
		}

		public class EnumeratedTest1 : Enumerated
		{
			public EnumeratedTest1(string name) : base(name, false) { }
		}

		public class WrapperTest1
		{
			public EnumeratedTest1? EnumeratedValue { get; set; }
		}
	}
}
