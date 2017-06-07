using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;
using System.Data.SQLite;
using System.Data;

namespace WebApplication1.DataAccessLayer
{
	public class SalesERPDAL
	{
		private SQLiteConnection sqlite;

		public SalesERPDAL()
		{
			//This part killed me in the beginning.  I was specifying "DataSource"
			//instead of "Data Source"
			sqlite = new SQLiteConnection("Data Source=F:/asp.net/ASP/SQLLiteBaza/test.db");

		}

		public List<Employee> selectQuery(string query)
		{
			
			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();

			try
			{
				SQLiteCommand cmd;
				sqlite.Open();  //Initiate connection to the db
				cmd = sqlite.CreateCommand();
				cmd.CommandText = query;  //set the passed query
				ad = new SQLiteDataAdapter(cmd);
				ad.Fill(dt); //fill the datasource
				
			}
			catch (SQLiteException ex)
			{
				//Add your exception code here.
			}
			sqlite.Close();
			return FillData(dt);
		}

		private List<Employee> FillData(DataTable td)
		{
			List<Employee> employee = new List<Employee>(td.Rows.Count);
			Employee emp = null;
			
			try
			{
				foreach (DataRow row in td.Rows)
				{
					emp = new Employee();

					emp.EmployeeId = Convert.ToInt32(row["EmployeeId"].ToString());
					emp.FirstName = row["FirstName"].ToString();
					emp.LastName = row["LastName"].ToString();
					emp.Salary = Convert.ToInt32(row["Salary"].ToString());

					employee.Add(emp);
				}
			}
			catch (Exception)
			{
				return new List<Employee>();
			}
			return employee;
		}
	}
}