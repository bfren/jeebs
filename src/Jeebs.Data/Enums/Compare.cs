// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Enums;

/// <summary>
/// Compare Operators
/// </summary>
public enum Compare
{
	/// <summary>
	/// Equal
	/// </summary>
	Equal = 1 << 0,

	/// <summary>
	/// Not Equal
	/// </summary>
	NotEqual = 1 << 1,

	/// <summary>
	/// Like
	/// </summary>
	Like = 1 << 2,

	/// <summary>
	/// Less Than
	/// </summary>
	LessThan = 1 << 3,

	/// <summary>
	/// Less Than or Equal
	/// </summary>
	LessThanOrEqual = 1 << 4,

	/// <summary>
	/// More Than
	/// </summary>
	MoreThan = 1 << 5,

	/// <summary>
	/// More Than or Equal
	/// </summary>
	MoreThanOrEqual = 1 << 6,

	/// <summary>
	/// In
	/// </summary>
	In = 1 << 7,

	/// <summary>
	/// Not In
	/// </summary>
	NotIn = 1 << 8,

	/// <summary>
	/// Is (e.g. for NULL)
	/// </summary>
	Is = 1 << 9,

	/// <summary>
	/// Is Not (e.g. for NULL)
	/// </summary>
	IsNot = 1 << 10,
}
