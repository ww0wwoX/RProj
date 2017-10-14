using RProject.CommonModels.Rights;

namespace RProject.Security
{
	public interface IRSequrity
	{
		string CreateUserAccount(UserData userData);

		bool VerifyUser(string passwordHash, string password);
	}
}