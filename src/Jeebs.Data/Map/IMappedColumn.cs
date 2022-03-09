// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data.Map;

/// <summary>
/// Holds information about a mapped column
/// </summary>
public interface IMappedColumn : IColumn
{
	/// <summary>
	/// Entity Property
	/// </summary>
	PropertyInfo PropertyInfo { get; }
}
