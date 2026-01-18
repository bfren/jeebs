// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Comes from https://martinfowler.com/eaaDev/Range.html.
/// </summary>
/// <typeparam name="T">Range value type</typeparam>
public interface IRange<T>
{
	/// <summary>
	/// Range start
	/// </summary>
	T Start { get; }

	/// <summary>
	/// Range finish
	/// </summary>
	T Finish { get; }

	/// <summary>
	/// The length of the range
	/// </summary>
	int Length { get; }

	/// <summary>
	/// Whether or not the range includes the specified value
	/// </summary>
	/// <param name="value"></param>
	bool Includes(T value);

	/// <summary>
	/// Whether or not the range includes the specified range of values
	/// </summary>
	/// <param name="value">IRange</param>
	bool Includes(IRange<T> value);

	/// <summary>
	/// Whether or not the range overlaps the specified range
	/// </summary>
	/// <param name="value">IRange</param>
	bool Overlaps(IRange<T> value);
}
