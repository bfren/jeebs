// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jeebs.Mvc.Data.ModelBinding
{
	/// <summary>
	/// Binds an ID to a StrongID
	/// </summary>
	/// <typeparam name="T">StrongID type</typeparam>
	public sealed class StrongIdModelBinder<T> : IModelBinder
		where T : StrongId, new()
	{
		/// <summary>
		/// Get value and attempt to parse as a long
		/// </summary>
		/// <param name="bindingContext">ModelBindingContext</param>
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			// Get the value from the context
			var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (valueProviderResult == ValueProviderResult.None)
			{
				return Task.CompletedTask;
			}

			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

			// Get the value and attempt to parse it as a long
			bindingContext.Result = ulong.TryParse(valueProviderResult.FirstValue, out ulong id) switch
			{
				true =>
					success(id),

				false =>
					success(0)
			};

			return Task.CompletedTask;

			// Set the model value using the parsed ID
			static ModelBindingResult success(ulong id) =>
				ModelBindingResult.Success(new T { Value = id });
		}
	}
}
