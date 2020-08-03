using System.Threading.Tasks;

namespace Jeebs_old.AuditAsync
{
	public interface IAuditAsync_Task0_Simple
	{
		Task StartAsync_AuditAsync_Returns_Original_Object();
		Task StartAsync_Successful_AuditAsync_Writes_To_Log();
		Task StartAsync_Unsuccessful_Audit_Captures_Exception();
		Task StartSync_AuditAsync_Returns_Original_Object();
		Task StartSync_Successful_AuditAsync_Writes_To_Log();
		Task StartSync_Unsuccessful_Audit_Captures_Exception();
	}
}