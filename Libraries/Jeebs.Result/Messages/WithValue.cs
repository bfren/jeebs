using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	public class WithValue<T> : IMessage
	{
		public T Val { get; }

		public WithValue(T value) => Val = value;

		public override string ToString() => Val?.ToString() ?? base.ToString();
	}
}
