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
	public static class SalesERPDAL
	{
		private static SQLiteConnection sqlite = new SQLiteConnection("Data Source=F:/asp.net/ASP/SQLLiteBaza/test.db");
		private static object lockDatabase = new object();


		public static List<Employee> GetEmployees()
		{		
			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();
			lock (lockDatabase)
			{

				try
				{
					SQLiteCommand cmd;
					sqlite.Open();  //Initiate connection to the db
					cmd = sqlite.CreateCommand();
					cmd.CommandText = "SELECT * FROM Employee";  //set the passed query
					ad = new SQLiteDataAdapter(cmd);
					ad.Fill(dt); //fill the datasource

				}
				catch (SQLiteException ex)
				{
					//Add your exception code here.
				}
				finally
				{
					if (sqlite != null)
					{
						sqlite.Close();
					}
				}
			}
			return FillEmployeeData(dt);
		}

		public static void SaveEmployee(Employee e)
		{
			DataTable dt = new DataTable();

			lock (lockDatabase)
			{

				try
				{
					SQLiteCommand cmd;
					sqlite.Open();  //Initiate connection to the db
					cmd = sqlite.CreateCommand();
					cmd.CommandText = "INSERT INTO Employee(EmployeeId, FirstName, LastName, Salary) VALUES (@param1,@param2,@param3,@param4)";  //set the passed query
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add(new SQLiteParameter("@param1", e.EmployeeId));
					cmd.Parameters.Add(new SQLiteParameter("@param2", e.FirstName));
					cmd.Parameters.Add(new SQLiteParameter("@param3", e.LastName));
					cmd.Parameters.Add(new SQLiteParameter("@param4", e.Salary));
					cmd.ExecuteNonQuery();

				}
				catch (SQLiteException ex)
				{
					//Add your exception code here.
				}
				finally
				{
					if (sqlite != null)
					{
						sqlite.Close();
					}
				}
			}
		}

		private static List<Employee> FillEmployeeData(DataTable td)
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