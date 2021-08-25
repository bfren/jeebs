// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Run <paramref name="ifSome"/> if <paramref name="option"/> is a <see cref="Some{T}"/>,
		/// and returns the original <paramref name="option"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="ifSome">Will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public static Option<T> IfSome<T>(Option<T> option, Action<T> ifSome) =>
			Catch(() =>
				{
					if (option is Some<T> some)
					{
						ifSome(some.Value);
					}

					return option;
				},
				DefaultHandler
			);
	}
}
