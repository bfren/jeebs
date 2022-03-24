// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Id;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jeebs.Mvc.Data.ModelBinding.StrongIdModelBinderProvider_Tests;

public class GetBinderFromModelType_Tests
{
	[Fact]
	public void ModelType_Does_Not_Inherit_RouteId_Returns_Null()
	{
		// Arrange
		var type = typeof(RandomType);

		// Act
		var result = StrongIdModelBinderProvider.GetBinderFromModelType(type);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void ModelType_Inherits_RouteId_Returns_RouteIdModelBinder()
	{
		// Arrange
		var type = typeof(IdType);

		// Act
		var result = StrongIdModelBinderProvider.GetBinderFromModelType(type);

		// Assert
		Assert.IsAssignableFrom<IModelBinder>(result);
		Assert.IsType<StrongIdModelBinder<IdType>>(result);
	}

	public sealed record class RandomType;

	public sealed record class IdType : StrongId;
}
