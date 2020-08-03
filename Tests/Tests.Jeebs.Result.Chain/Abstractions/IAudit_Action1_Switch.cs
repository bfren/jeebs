using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.AuditTests
{
	public interface IAudit_Action1_Switch
	{
		void AuditSwitch_Catches_Exception_Adds_Message();
		void AuditSwitch_DoesNot_Run_Error_When_Ok();
		void AuditSwitch_DoesNot_Run_Error_When_OkV();
		void AuditSwitch_DoesNot_Run_OkV_When_Error();
		void AuditSwitch_DoesNot_Run_OkV_When_Ok();
		void AuditSwitch_DoesNot_Run_Ok_When_Error();
		void AuditSwitch_DoesNot_Run_Ok_When_OkV();
		void AuditSwitch_Returns_Original_Object_Without_Parameters();
		void AuditSwitch_Returns_Original_Object_With_Parameters();
		void AuditSwitch_Runs_Error_When_Error();
		void AuditSwitch_Runs_OkV_When_OkV();
		void AuditSwitch_Runs_Ok_When_Ok();
	}
}