using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Jeebs.Result.Audit
{
	public interface IAudit_Action0_Simple
	{
		public void Audit_Returns_Original_Object();
		void Successful_Audit_Writes_To_Log();
		void Unsuccessful_Audit_Captures_Exception();
	}
}
