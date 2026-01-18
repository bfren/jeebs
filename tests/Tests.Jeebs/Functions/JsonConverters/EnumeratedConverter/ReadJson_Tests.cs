// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.JsonConverters.EnumeratedConverter_Tests;

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
		var ok = result.AssertOk();
		Assert.Equal(value, ok.ToString());
	}

	[Fact]
	public void Deserialise_Null_Returns_Object_With_Empty_Value()
	{
		// Arrange
		var json = JsonF.Empty;

		// Act
		var result = JsonF.Deserialise<EnumeratedTest0>(json);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(string.Empty, ok.ToString());
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
		var ok = result.AssertOk();
		Assert.Equal(id, ok.Id);
		Assert.Equal(value, ok.EnumeratedValue.ToString());
	}

	[Theory]
	[InlineData(null)]
	[InlineData("\"\"")]
	[InlineData("\"  \"")]
	public void Deserialise_Object_With_Enumerated_Property_But_Null_Value_And_Disallow_Empty_Returns_Fail(string? input)
	{
		// Arrange
		var json = $"{{ \"enumeratedValue\": {input} }}";

		// Act
		var result = JsonF.Deserialise<EnumeratedWrapperTest1>(json);

		// Assert
		var fail = result.AssertFail();
		Assert.NotNull(fail.Exception);
	}

	public record class EnumeratedTest0 : Enumerated
	{
		public EnumeratedTest0(string name) : base(name) { }
	}

	public class EnumeratedWrapperTest0
	{
		public int Id { get; set; }

		public EnumeratedTest0 EnumeratedValue { get; set; } = new EnumeratedTest0(string.Empty);
	}

	public record class EnumeratedTest1 : Enumerated
	{
		public EnumeratedTest1(string name) : base(name, false) { }
	}

	public class EnumeratedWrapperTest1
	{
		public EnumeratedTest1? EnumeratedValue { get; set; }
	}
}
