// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSubstitute;
using Xunit;

namespace Jeebs.Mvc.Data.ModelBinding.StrongIdModelBinder_Tests;

public class BindModelAsync_Tests
{
	[Fact]
	public async Task ValueProvider_Result_Is_None_Returns_Original_ModelBindingResult()
	{
		// Arrange
		var modelName = F.Rnd.Str;

		var provider = Substitute.For<IValueProvider>();
		provider.GetValue(modelName).Returns(ValueProviderResult.None);

		var bindingResult = new ModelBindingResult();

		var context = Substitute.ForPartsOf<ModelBindingContext>();
		context.ModelName.Returns(modelName);
		context.Result = bindingResult;
		context.ValueProvider.Returns(provider);

		var binder = new StrongIdModelBinder<IdType>();

		// Act
		await binder.BindModelAsync(context);

		// Assert
		Assert.Equal(bindingResult, context.Result);
	}

	[Fact]
	public async Task Sets_Model_Value_Using_ValueProvider_Result()
	{
		// Arrange
		var modelName = F.Rnd.Str;
		var modelValue = new ValueProviderResult(F.Rnd.Str);

		var provider = Substitute.For<IValueProvider>();
		provider.GetValue(modelName).Returns(modelValue);

		var state = Substitute.ForPartsOf<ModelStateDictionary>();

		var context = Substitute.ForPartsOf<ModelBindingContext>();
		context.ModelName.Returns(modelName);
		context.ModelState.Returns(state);
		context.ValueProvider.Returns(provider);

		var binder = new StrongIdModelBinder<IdType>();

		// Act
		await binder.BindModelAsync(context);

		// Assert
		Assert.Equal(modelValue.FirstValue, state[modelName]?.AttemptedValue);
	}

	[Fact]
	public async Task ValueProvider_Result_Is_Not_Valid_Id_Sets_Result_Failed()
	{
		// Arrange
		var modelName = F.Rnd.Str;
		var modelValue = new ValueProviderResult(F.Rnd.Str);

		var provider = Substitute.For<IValueProvider>();
		provider.GetValue(modelName).Returns(modelValue);

		var state = Substitute.ForPartsOf<ModelStateDictionary>();

		var context = Substitute.ForPartsOf<ModelBindingContext>();
		context.ModelName.Returns(modelName);
		context.ModelState.Returns(state);
		context.ValueProvider.Returns(provider);

		var binder = new StrongIdModelBinder<IdType>();

		// Act
		await binder.BindModelAsync(context);

		// Assert
		Assert.False(context.Result.IsModelSet);
	}

	[Fact]
	public async Task ValueProvider_Result_Is_Valid_ULong_Sets_Result_Success_With_Id_Value()
	{
		// Arrange
		var modelName = F.Rnd.Str;
		var id = F.Rnd.Lng;
		var modelValue = new ValueProviderResult(id.ToString());

		var provider = Substitute.For<IValueProvider>();
		provider.GetValue(modelName).Returns(modelValue);

		var state = Substitute.ForPartsOf<ModelStateDictionary>();

		var context = Substitute.ForPartsOf<ModelBindingContext>();
		context.ModelName.Returns(modelName);
		context.ModelState.Returns(state);
		context.ValueProvider.Returns(provider);

		var binder = new StrongIdModelBinder<IdType>();

		// Act
		await binder.BindModelAsync(context);

		// Assert
		Assert.True(context.Result.IsModelSet);
		var model = Assert.IsType<IdType>(context.Result.Model);
		Assert.Equal(id, model.Value);
	}

	public sealed record class IdType : StrongId;
}
