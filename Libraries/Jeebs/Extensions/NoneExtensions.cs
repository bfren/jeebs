using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public static class NoneExtensions
	{
		/// <summary>
		/// Add a reason for returning a None option type
		/// </summary>
		/// <typeparam name="T">Wrapped type</typeparam>
		/// <param name="none">None</param>
		/// <param name="reason">Reason for the None option</param>
		public static None<T> AddReason<T>(this None<T> none, string reason)
		{
			none.AddReason(reason);
			return none;
		}
	}
}
