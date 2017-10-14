using RProject.CommonModels.Rights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RProject.Security
{
	public class RSequrity : IRSequrity
	{
		const int SALT_LENGHT = 20;
		const int HASH_LENGHT = 60;
		const int HASH_ITERATIONS = 1000;

		public string CreateUserAccount(UserData userData)
		{
			return GetPasswordHash(userData.Password);
			//TODO
		}

		public bool VerifyUser(string passwordHash, string password)
		{
			byte[] hashBytes = Convert.FromBase64String(passwordHash);
			byte[] salt = new byte[SALT_LENGHT];

			Array.Copy(hashBytes, 0, salt, 0, SALT_LENGHT);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HASH_ITERATIONS);
			byte[] hash = pbkdf2.GetBytes(HASH_LENGHT);

			for (int i = SALT_LENGHT; i < HASH_LENGHT; i++)
			{
				if (hashBytes[i + SALT_LENGHT] != hash[i])
				{
					return false;
				}
			}

			return true;
		}

		protected string GetPasswordHash(string password)
		{
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[SALT_LENGHT]);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, HASH_ITERATIONS);
			byte[] hash = pbkdf2.GetBytes(HASH_LENGHT);

			byte[] hashBytes = new byte[SALT_LENGHT + HASH_LENGHT];
			Array.Copy(salt, 0, hashBytes, 0, SALT_LENGHT);
			Array.Copy(hash, 0, hashBytes, SALT_LENGHT, HASH_LENGHT);

			return Convert.ToBase64String(hashBytes);
		}
	}
}
