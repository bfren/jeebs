using System.Threading.Tasks;

namespace Jeebs_old.AuditAsync
{
	public interface IAuditAsync_Task1_Switch
	{
		Task StartAsync_AuditSwitchAsync_Catches_Exception_Adds_Message();
		Task StartAsync_AuditSwitchAsync_DoesNot_Run_Error_When_Ok();
		Task StartAsync_AuditSwitchAsync_DoesNot_Run_Error_When_OkV();
		Task StartAsync_AuditSwitchAsync_DoesNot_Run_OkV_When_Error();
		Task StartAsync_AuditSwitchAsync_DoesNot_Run_OkV_When_Ok();
		Task StartAsync_AuditSwitchAsync_DoesNot_Run_Ok_When_Error();
		Task StartAsync_AuditSwitchAsync_DoesNot_Run_Ok_When_OkV();
		Task StartAsync_AuditSwitchAsync_Returns_Original_Object_Without_Parameters();
		Task StartAsync_AuditSwitchAsync_Returns_Original_Object_With_Parameters();
		Task StartAsync_AuditSwitchAsync_Runs_Error_When_Error();
		Task StartAsync_AuditSwitchAsync_Runs_OkV_When_OkV();
		Task StartAsync_AuditSwitchAsync_Runs_Ok_When_Ok();
		Task StartAsync_AuditSwitchAsync_Runs_Unknown_When_Unknown();
		Task StartSync_AuditSwitchAsync_Catches_Exception_Adds_Message();
		Task StartSync_AuditSwitchAsync_DoesNot_Run_Error_When_Ok();
		Task StartSync_AuditSwitchAsync_DoesNot_Run_Error_When_OkV();
		Task StartSync_AuditSwitchAsync_DoesNot_Run_OkV_When_Error();
		Task StartSync_AuditSwitchAsync_DoesNot_Run_OkV_When_Ok();
		Task StartSync_AuditSwitchAsync_DoesNot_Run_Ok_When_Error();
		Task StartSync_AuditSwitchAsync_DoesNot_Run_Ok_When_OkV();
		Task StartSync_AuditSwitchAsync_Returns_Original_Object_Without_Parameters();
		Task StartSync_AuditSwitchAsync_Returns_Original_Object_With_Parameters();
		Task StartSync_AuditSwitchAsync_Runs_Error_When_Error();
		Task StartSync_AuditSwitchAsync_Runs_OkV_When_OkV();
		Task StartSync_AuditSwitchAsync_Runs_Ok_When_Ok();
		Task StartSync_AuditSwitchAsync_Runs_Unknown_When_Unknown();
	}
}