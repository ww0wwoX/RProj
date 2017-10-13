using RProject.CommonModels.Rights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RProject.Security
{
	public class RSequrity : IRSequriry
	{
		public void CreateUserAccount(UserData userData)
		{
			string passwordHash = GetPasswordHash(userData.Password);
			//https://stackoverflow.com/questions/4181198/how-to-hash-a-password

			//TODO

			throw new NotImplementedException();
		}

		protected string GetPasswordHash(string password)
		{
			const int saltLenght = 20;
			const int hashLenght = 32;
			const int iterations = 1000;

			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[saltLenght]);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
			byte[] hash = pbkdf2.GetBytes(hashLenght);

			byte[] hashBytes = new byte[saltLenght + hashLenght];
			Array.Copy(salt, 0, hashBytes, 0, saltLenght);
			Array.Copy(hash, 0, hashBytes, saltLenght, hashLenght);

			return Convert.ToBase64String(hashBytes);
		}
	}
}
