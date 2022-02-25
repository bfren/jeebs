// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jeebs.Mvc.Data.ModelBinding;

/// <summary>
/// Creates <see cref="StrongIdModelBinder{T}"/>
/// </summary>
public sealed class StrongIdModelBinderProvider : IModelBinderProvider
{
	/// <summary>
	/// If the model type implements <see cref="IStrongId"/>, create <see cref="StrongIdModelBinder{T}"/>
	/// </summary>
	/// <param name="context">ModelBinderProviderContext</param>
	public IModelBinder? GetBinder(ModelBinderProviderContext context) =>
		GetBinderFromModelType(context.Metadata.ModelType);

	/// <summary>
	/// Get binder from the specified model type
	/// </summary>
	/// <param name="modelType">Model Type</param>
	internal static IModelBinder? GetBinderFromModelType(Type modelType)
	{
		// Return null if this is the wrong type
		if (!modelType.Implements<IStrongId>())
		{
			return null;
		}

		// The context ModelType is the StrongId type, which we pass to the binder as a generic constraint
		try
		{
			var binderType = typeof(StrongIdModelBinder<>).MakeGenericType(modelType);
			return Activator.CreateInstance(binderType) switch
			{
				IModelBinder binder =>
					binder,

				_ =>
					null
			};
		}
		catch (Exception)
		{
			return null;
		}
	}
}
