using RProject.CommonModels.Rights;
using RProject.DataHelper;
using RProject.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Linq;

namespace RProject.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			#region VerifyUser
			//IRSequrity sequrity = new RSequrity();
			//var hash = sequrity.CreateUserAccount(
			//	new UserData()
			//	{
			//		FirstName = "first",
			//		LastName = "last",
			//		Login = "login",
			//		Password = "pword"
			//	}
			//);

			//Console.WriteLine($"hash: {hash}");
			//Console.WriteLine(sequrity.VerifyUser(hash, "qwerty"));
			//Console.WriteLine(sequrity.VerifyUser(hash, "pword"));
			#endregion

			IDbHelper helper = new DbHelper();
			Console.WriteLine(helper.Test());




			Console.ReadKey();
		}
	}
}
