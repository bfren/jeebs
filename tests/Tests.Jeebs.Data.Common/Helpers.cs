// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data;

public static class Helpers
{
	public static PropertyInfo CreateInfoFromAlias() =>
		CreateInfoFromAlias(Rnd.Str);

	public static PropertyInfo CreateInfoFromAlias(string alias)
	{
		var info = Substitute.ForPartsOf<PropertyInfo>();
		info.Name
			.Returns(alias);

		return info;
	}
}
