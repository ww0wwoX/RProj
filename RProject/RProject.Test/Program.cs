using RProject.CommonModels.Rights;
using RProject.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RProject.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			IRSequrity sequrity = new RSequrity();
			var hash = sequrity.CreateUserAccount(
				new UserData()
				{
					FirstName = "first",
					LastName ="last",
					Login = "login",
					Password = "pword"
				}
			);

			Console.WriteLine($"hash: {hash}");
			Console.WriteLine(sequrity.VerifyUser(hash, "qwerty"));
			Console.WriteLine(sequrity.VerifyUser(hash, "pword"));

			Console.ReadKey();
		}
	}
}
