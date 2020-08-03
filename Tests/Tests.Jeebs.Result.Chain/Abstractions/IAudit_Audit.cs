namespace Jeebs.AuditTests
{
	public interface IAudit_Audit
	{
		void Catches_Exception();
		void Returns_Original_Object();
		void Runs_Audit_Action();
	}
}