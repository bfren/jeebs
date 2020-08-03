using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.AuditAsyncTests
{
	public interface IAuditAsync_Task0_Simple
	{
		Task AuditAsync_Returns_Original_Object();
		Task Successful_AuditAsync_Writes_To_Log();
		Task Unsuccessful_Audit_Captures_Exception();
	}
}