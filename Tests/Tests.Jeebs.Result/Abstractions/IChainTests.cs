﻿using System.Threading.Tasks;

namespace Tests.Jeebs.Result
{
	public interface IChainTests
	{
		Task R_ChainAsync_Returns_Ok();
		void R_Chain_Returns_Ok();
	}
}