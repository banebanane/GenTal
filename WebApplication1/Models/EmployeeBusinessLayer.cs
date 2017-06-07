using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.DataAccessLayer;

namespace WebApplication1.Models
{
	public class EmployeeBusinessLayer
	{
		public List<Employee> GetEmployees()
		{ 
		/*	List<Employee> employees = new List<Employee>();
			Employee emp = new Employee();
			emp.FirstName = "Johnson";
			emp.LastName = "Fernandes";
			emp.Salary = 14000;
			employees.Add(emp);

			emp = new Employee();
			emp.FirstName = "Michael";
			emp.LastName = "Jackson";
			emp.Salary = 16000;
			employees.Add(emp);

			emp = new Employee();
			emp.FirstName = "Robert";
			emp.LastName = "Pattinson";
			emp.Salary = 20000;
			employees.Add(emp);

			return employees;*/
			SalesERPDAL salesDal = new SalesERPDAL();
			return salesDal.selectQuery("SELECT * FROM Employee");
		}

	}
}