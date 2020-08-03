using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.AuditAsyncTests
{
	public interface IAuditAsync_Task1_Switch
	{
		Task AuditSwitchAsync_Catches_Exception_Adds_Message();
		Task AuditSwitchAsync_DoesNot_Run_Error_When_Ok();
		Task AuditSwitchAsync_DoesNot_Run_Error_When_OkV();
		Task AuditSwitchAsync_DoesNot_Run_OkV_When_Error();
		Task AuditSwitchAsync_DoesNot_Run_OkV_When_Ok();
		Task AuditSwitchAsync_DoesNot_Run_Ok_When_Error();
		Task AuditSwitchAsync_DoesNot_Run_Ok_When_OkV();
		Task AuditSwitchAsync_Returns_Original_Object_Without_Parameters();
		Task AuditSwitchAsync_Returns_Original_Object_With_Parameters();
		Task AuditSwitchAsync_Runs_Error_When_Error();
		Task AuditSwitchAsync_Runs_OkV_When_OkV();
		Task AuditSwitchAsync_Runs_Ok_When_Ok();
	}
}