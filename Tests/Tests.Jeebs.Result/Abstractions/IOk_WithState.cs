// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.ROk_Tests
{
	public interface IOk_WithState
	{
		void Returns_Ok_With_State();
		void Returns_Ok_With_State_And_Keeps_Messages();
		void Returns_Ok_With_State_And_Keeps_Value();
	}
}