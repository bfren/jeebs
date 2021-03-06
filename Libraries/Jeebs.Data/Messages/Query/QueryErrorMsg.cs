// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jm.Data
{
	/// <summary>
	/// Message about an error that has occurred during a data query
	/// </summary>
	public sealed class QueryErrorMsg : WithValueMsg<string>
	{
		internal QueryErrorMsg(string error) : base(error) { }
	}
}
