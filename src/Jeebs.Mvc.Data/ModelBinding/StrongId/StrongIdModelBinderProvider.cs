// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jeebs.Mvc.Data.ModelBinding
{
	/// <summary>
	/// Creates <see cref="StrongIdModelBinder{T}"/>
	/// </summary>
	public sealed class StrongIdModelBinderProvider : IModelBinderProvider
	{
		/// <summary>
		/// If the model type implements <see cref="StrongId"/>, create <see cref="StrongIdModelBinder{T}"/>
		/// </summary>
		/// <param name="context">ModelBinderProviderContext</param>
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{
			// Return null if this is the wrong type
			if (!context.Metadata.ModelType.Implements<StrongId>())
			{
				return null;
			}

			// The context ModelType is the StrongId type, which we pass to the binder as a generic constraint
			try
			{
				var binderType = typeof(StrongIdModelBinder<>).MakeGenericType(context.Metadata.ModelType);
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
}
