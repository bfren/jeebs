//using System;
//using Xunit;

//namespace Jeebs.Data.Clients.SqlServer
//{
//	public class SqlServerAdapterTests
//	{
//		[Theory]
//		[InlineData(null)]
//		[InlineData("")]
//		[InlineData("   ")]
//		public void Escape_NullOrWhitespace_ThrowsArgumentNullException(string input)
//		{
//			// Arrange
//			var adapter = new SqlServerAdapter();

//			// Act
//			Action result = () => adapter.Escape(input);

//			// Assert
//			Assert.Throws<ArgumentNullException>(result);
//		}

//		[Theory]
//		[InlineData("foo", "[foo]")]
//		[InlineData("[foo[", "[foo]")]
//		[InlineData("foo.bar", "[foo].[bar]")]
//		public void Escape_Name_ReturnsEscapedName(string input, string expected)
//		{
//			// Arrange
//			var adapter = new SqlServerAdapter();

//			// Act
//			var result = adapter.Escape(input);

//			// Assert
//			Assert.Equal(expected, result);
//		}

//		[Theory]
//		[InlineData("foo", "[foo]")]
//		[InlineData("[foo[", "[foo]")]
//		[InlineData("foo.bar", "[foo].[bar]")]
//		[InlineData("foo..bar", "[foo].[bar]")]
//		[InlineData("foo.   .bar", "[foo].[bar]")]
//		[InlineData("foo.bar.foo.bar", "[foo].[bar].[foo].[bar]")]
//		public void SplitAndEscape_Name_ReturnsSplitAndEscapedName(string input, string expected)
//		{
//			// Arrange
//			var adapter = new SqlServerAdapter();

//			// Act
//			var result = adapter.SplitAndEscape(input);

//			// Assert
//			Assert.Equal(expected, result);
//		}

//		[Theory]
//		[InlineData(new[] { "foo" }, "[foo[")]
//		[InlineData(new[] { "foo", "bar" }, "[foo].[bar]")]
//		[InlineData(new[] { "foo", "", null, "   ", "bar", "" }, "[foo].[bar]")]
//		public void EscapeAndJoin_Name_ReturnsEscapedAndJoinedName(string[] input, string expected)
//		{
//			// Arrange
//			var adapter = new SqlServerAdapter();

//			// Act
//			var result = adapter.EscapeAndJoin(input);

//			// Assert
//			Assert.Equal(expected, result);
//		}

//		[Fact]
//		public void CreateSingleAndReturnId()
//		{
//			// Arrange
//			var adapter = new SqlServerAdapter();
//			new FooTable(adapter);
//			const string expected = "INSERT INTO [foo] ([foo_bar0], [foo_bar1]) VALUES (@Bar0, @Bar1); SELECT LAST_INSERT_ID();";

//			// Act
//			var result = adapter.CreateSingleAndReturnId<Foo>();

//			// Assert
//			Assert.Equal(expected, result);
//		}

//		[Fact]
//		public void RetrieveSingleById()
//		{
//			// Arrange
//			var adapter = new SqlServerAdapter();
//			new FooTable(adapter);
//			new FooWithVersionTable(adapter);

//			const string expected = "SELECT [foo_id] AS 'Id', [foo_bar0] AS 'Bar0', [foo_bar1] AS 'Bar1' FROM [foo] WHERE [foo_id] = '1';";

//			// Act
//			var result = adapter.RetrieveSingleById<Foo>();

//			// Assert
//			Assert.Equal(expected, result);
//		}

//		[Fact]
//		public void UpdateSingle()
//		{
//			// Arrange
//			var adapter = new SqlServerAdapter();
//			new FooTable(adapter);
//			new FooWithVersionTable(adapter);
//			const string expected = "UPDATE [foo] SET [foo_bar0] = @Bar0, [foo_bar1] = @Bar1 WHERE [foo_id] = @Id;";
//			const string expected_with_version = "UPDATE [foo_with_version] SET [foo_bar0] = @Bar0, [foo_bar1] = @Bar1, [foo_version] = @Version " +
//				"WHERE [foo_id] = @Id AND [foo_version] = @Version - 1;";

//			// Act
//			var result = adapter.UpdateSingle<Foo>();
//			var result_with_version = adapter.UpdateSingle<FooWithVersion>();

//			// Assert
//			Assert.Equal(expected, result);
//			Assert.Equal(expected_with_version, result_with_version);
//		}

//		[Fact]
//		public void DeleteSingle()
//		{
//			// Arrange
//			var adapter = new SqlServerAdapter();
//			new FooTable(adapter);
//			new FooWithVersionTable(adapter);
//			const string expected = "DELETE FROM [foo] WHERE [foo_id] = @Id;";
//			const string expected_with_version = "DELETE FROM [foo_with_version] WHERE [foo_id] = @Id AND [foo_version] = @Version;";

//			// Act
//			var result = adapter.DeleteSingle<Foo>();
//			var result_with_version = adapter.DeleteSingle<FooWithVersion>();

//			// Assert
//			Assert.Equal(expected, result);
//			Assert.Equal(expected_with_version, result_with_version);
//		}

//		class Foo : IEntity
//		{
//			[Id]
//			public int Id { get; set; }

//			public string Bar0 { get; set; }

//			public string Bar1 { get; set; }
//		}

//		class FooWithVersion : Foo, IEntityWithVersion
//		{
//			[Version]
//			public long Version { get; set; }
//		}

//		class FooTable : Table<Foo>
//		{
//			public readonly string Id = "foo_id";

//			public readonly string Bar0 = "foo_bar0";

//			public readonly string Bar1 = "foo_bar1";

//			public FooTable(in IAdapter adapter) : base(adapter, "foo") { }
//		}

//		class FooWithVersionTable : Table<FooWithVersion>
//		{
//			public readonly string Id = "foo_id";

//			public readonly string Bar0 = "foo_bar0";

//			public readonly string Bar1 = "foo_bar1";

//			public readonly string Version = "foo_version";

//			public FooWithVersionTable(in IAdapter adapter) : base(adapter, "foo_with_version") { }
//		}
//	}
//}
