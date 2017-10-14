using RProject.CommonModels.Rights;

namespace RProject.Security
{
	public interface IRSequriry
	{
		void CreateUserAccount(UserData userData);
	}
}