// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Data.Reflection.PropertyInfoExtensions_Tests;

public class IsReadonly_Tests
{
	[Fact]
	public void Property_With_IdAttribute_Returns_True()
	{
		// Arrange
		var @class = typeof(TestClassWithId).GetProperty(nameof(TestClassWithId.Id))!;
		var @record = typeof(TestRecordWithId).GetProperty(nameof(TestRecordWithId.Id))!;

		// Act
		var r0 = @class.IsReadonly();
		var r1 = record.IsReadonly();

		// Assert
		Assert.True(r0);
		Assert.True(r1);
	}

	[Fact]
	public void Property_With_VersionAttribute_Returns_True()
	{
		// Arrange
		var @class = typeof(TestClassWithIgnore).GetProperty(nameof(TestClassWithIgnore.Ignore))!;
		var @record = typeof(TestRecordWithIgnore).GetProperty(nameof(TestRecordWithIgnore.Ignore))!;

		// Act
		var r0 = @class.IsReadonly();
		var r1 = @record.IsReadonly();

		// Assert
		Assert.True(r0);
		Assert.True(r1);
	}

	[Fact]
	public void Property_Without_Attributes_Returns_False()
	{
		// Arrange
		var @class = typeof(TestClass).GetProperty(nameof(TestClass.Writeable))!;
		var @record = typeof(TestRecord).GetProperty(nameof(TestRecord.Writeable))!;

		// Act
		var r0 = @class.IsReadonly();
		var r1 = @record.IsReadonly();

		// Assert
		Assert.False(r0);
		Assert.False(r1);
	}

	public class TestClassWithId
	{
		[Id]
		public long Id { get; set; }
	}

	public record TestRecordWithId([property: Id] long Id);

	public class TestClassWithIgnore
	{
		[Ignore]
		public long Ignore { get; set; }
	}

	public record TestRecordWithIgnore([property: Ignore] long Ignore);

	public class TestClass
	{
		public long Writeable { get; set; }
	}

	public record TestRecord(long Writeable);
}
