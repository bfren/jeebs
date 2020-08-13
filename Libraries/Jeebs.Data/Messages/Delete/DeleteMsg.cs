﻿using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Data
{
	/// <summary>
	/// Delete success message
	/// </summary>
	public class DeleteMsg : IMsg
	{
		/// <summary>
		/// Entity Type
		/// </summary>
		protected readonly Type type;

		/// <summary>
		/// Entity ID
		/// </summary>
		protected readonly long? id;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="type">POCO type</param>
		/// <param name="id">POCO id</param>
		public DeleteMsg(Type type, long id)
			=> (this.type, this.id) = (type, id);

		/// <summary>
		/// Output success message
		/// </summary>
		public override string ToString()
			=> $"Deleted '{type}' with ID '{id}'.";
	}
}
