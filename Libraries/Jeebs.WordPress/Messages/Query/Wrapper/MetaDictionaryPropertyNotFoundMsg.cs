// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using Jeebs.WordPress;

namespace Jm.WordPress.Query.Wrapper
{
	/// <summary>
	/// See <see cref="QueryWrapper.GetMetaDictionary{TModel}"/>
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	public sealed class MetaDictionaryPropertyNotFoundMsg<TModel> : IMsg
	{
		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString() =>
			$"{typeof(MetaDictionary)} property cannot be found (model: {typeof(TModel)}).";
	}
}
