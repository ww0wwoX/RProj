using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace RProject.DataHelper
{
	public class DbHelper : IDbHelper
	{
		const int COMMAND_TIMEOUT_IN_SECONDS = 4 * 60 * 60; // seconds
		static readonly string connectionString = "";

		public DbHelper()
		{

		}

		static DbHelper()
		{
			connectionString = ConfigurationManager.ConnectionStrings["RConnection"].ConnectionString;
		}

		public string Test()
		{
			return connectionString;
		}
	}
}
