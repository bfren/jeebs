// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.RExtensions_Tests
{
	public interface IAudit_Audit
	{
		void Catches_Exception();
		void Returns_Original_Object();
		void Runs_Audit_Action();
	}
}