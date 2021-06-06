// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Xunit;
using static F.JsonF.Msg;

namespace F.JsonF_Tests.EnumeratedConverter_Tests
{
	public class ReadJson_Tests
	{
		[Fact]
		public void Deserialise_Returns_Object_With_Value()
		{
			// Arrange
			var value = Rnd.Str;
			var json = $"\"{value}\"";

			// Act
			var result = JsonF.Deserialise<EnumeratedTest0>(json);

			// Assert
			var some = Assert.IsType<Some<EnumeratedTest0>>(result);
			Assert.Equal(value, some.Value.ToString());
		}

		[Fact]
		public void Deserialise_Null_Returns_Object_With_Empty_Value()
		{
			// Arrange
			const string? json = JsonF.Empty;

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
			var id = Rnd.Int;
			var value = Rnd.Str;
			var json = $"{{ \"id\": {id}, \"enumeratedValue\": \"{value}\" }}";

			// Act
			var result = JsonF.Deserialise<EnumeratedWrapperTest0>(json);

			// Assert
			var wrapper = Assert.IsType<Some<EnumeratedWrapperTest0>>(result).Value;
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
			var result = JsonF.Deserialise<EnumeratedWrapperTest1>(json);

			// Assert
			var none = Assert.IsType<None<EnumeratedWrapperTest1>>(result);
			Assert.IsType<DeserialiseExceptionMsg>(none.Reason);
		}

		public class EnumeratedTest0 : Enumerated
		{
			public EnumeratedTest0(string name) : base(name) { }
		}

		public class EnumeratedWrapperTest0
		{
			public int Id { get; set; }

			public EnumeratedTest0 EnumeratedValue { get; set; } = new EnumeratedTest0(string.Empty);
		}

		public class EnumeratedTest1 : Enumerated
		{
			public EnumeratedTest1(string name) : base(name, false) { }
		}

		public class EnumeratedWrapperTest1
		{
			public EnumeratedTest1? EnumeratedValue { get; set; }
		}
	}
}
