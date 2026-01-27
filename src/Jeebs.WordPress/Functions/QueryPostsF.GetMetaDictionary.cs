// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Reflection;
using Jeebs.Reflection;

namespace Jeebs.WordPress.Functions;

public static partial class QueryPostsF
{
	/// <summary>
	/// Get the Meta Dictionary Info for <typeparamref name="TModel"/>.
	/// </summary>
	/// <typeparam name="TModel">Post Model type.</typeparam>
	internal static Result<Meta<TModel>> GetMetaDictionary<TModel>()
	{
		// Get meta dictionary property for model type
		var metaDictionary = from m in typeof(TModel).GetProperties()
							 where m.PropertyType == typeof(MetaDictionary)
							 select m;

		// If MetaDictionary is not defined return none
		if (!metaDictionary.Any())
		{
			return R.Fail("MetaDictionary property not found for model '{Type}'.", typeof(TModel).Name)
				.Lvl(Wrap.Logging.LogLevel.Debug)
				.Ctx(nameof(QueryPostsF), nameof(GetMetaDictionary));
		}

		// Throw an error if there are multiple MetaDictionaries
		if (metaDictionary.Count() > 1)
		{
			return R.Fail("Multiple MetaDictionary properties found for model '{Type}'.", typeof(TModel).Name)
				.Ctx(nameof(QueryPostsF), nameof(GetMetaDictionary));
		}

		return new Meta<TModel>(metaDictionary.Single());
	}

	/// <summary>
	/// Meta Info alias.
	/// </summary>
	/// <typeparam name="TModel">Post Model type.</typeparam>
	public sealed class Meta<TModel> : PropertyInfo<TModel, MetaDictionary>
	{
		/// <summary>
		/// Create object.
		/// </summary>
		/// <param name="info">PropertyInfo.</param>
		public Meta(PropertyInfo info) : base(info) { }
	}
}
