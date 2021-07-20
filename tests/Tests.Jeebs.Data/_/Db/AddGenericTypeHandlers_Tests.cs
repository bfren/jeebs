// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Data;
using Dapper;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Db_Tests
{
	public class AddGenericTypeHandlers_Tests
	{
		[Fact]
		public void HandlerType_Does_Not_Contain_Generic_Parameters_Does_Nothing()
		{
			// Arrange
			var handlerType = typeof(InvalidHandlerWithoutGenericParameter);
			var addTypeHandler = Substitute.For<Db.AddTypeHandler>();

			// Act
			Db.AddGenericTypeHandlers<string>(handlerType, addTypeHandler);

			// Assert
			addTypeHandler.DidNotReceive().Invoke(Arg.Any<Type>(), Arg.Any<SqlMapper.ITypeHandler>());
		}

		[Fact]
		public void HandlerType_Does_Not_Implement_ITypeHandler_Does_Nothing()
		{
			// Arrange
			var handlerType = typeof(InvalidHandlerWithGenericParameter<>);
			var addTypeHandler = Substitute.For<Db.AddTypeHandler>();

			// Act
			Db.AddGenericTypeHandlers<string>(handlerType, addTypeHandler);

			// Assert
			addTypeHandler.DidNotReceive().Invoke(Arg.Any<Type>(), Arg.Any<SqlMapper.ITypeHandler>());
		}

		[Fact]
		public void Calls_AddTypeHandler_For_Each_Implementing_Property_Type()
		{
			// Arrange
			var handlerType = typeof(Handler<>);
			var addTypeHandler = Substitute.For<Db.AddTypeHandler>();

			// Act
			Db.AddGenericTypeHandlers<CustomBaseType>(handlerType, addTypeHandler);

			// Assert
			addTypeHandler.Received().Invoke(typeof(CustomType0), Arg.Any<SqlMapper.ITypeHandler>());
			addTypeHandler.Received().Invoke(typeof(CustomType1), Arg.Any<SqlMapper.ITypeHandler>());
		}

		public class InvalidHandlerWithoutGenericParameter { }

		public class InvalidHandlerWithGenericParameter<T> { }

		public class Handler<T> : SqlMapper.ITypeHandler
			where T : CustomBaseType
		{
			public object Parse(Type destinationType, object value) =>
				throw new NotImplementedException();

			public void SetValue(IDbDataParameter parameter, object value) =>
				throw new NotImplementedException();
		}

		public abstract class CustomBaseType { }

		public class CustomType0 : CustomBaseType { }

		public class CustomType1 : CustomBaseType { }

		public class Test0
		{
			public CustomType0 Foo0 { get; set; } = new();

			public int Bar0 { get; set; }
		}

		public class Test1
		{
			public string Foo1 { get; set; } = string.Empty;

			public CustomType1 Bar1 { get; set; } = new();
		}
	}
}
