using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
	public class Customer
	{
		public string Customername { get; set; }
		public string Address { get; set; }

		public override string ToString()
		{
			return string.Join(" , ", Customername, Address);
		}
	}

    public class TestController : Controller
    {
        public Customer GetCustomer()
		{
			Customer c = new Customer();
			c.Customername = "Customer 1";
			c.Address = "Address 1";
			return c;
		}

		public ActionResult GetView()
		{
			EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();

			EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
			List<Employee> employees = empBal.GetEmployees();

			List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

			foreach (Employee emp in employees)
			{
				EmployeeViewModel empViewModel = new EmployeeViewModel();
				empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
				empViewModel.Salary = emp.Salary.ToString("C");
				if (emp.Salary > 15000)
				{
					empViewModel.SalaryColor = "yellow";
				}
				else
				{
					empViewModel.SalaryColor = "green";
				}

				empViewModels.Add(empViewModel);
			}
			employeeListViewModel.Employees = empViewModels;
			employeeListViewModel.UserName = "Admin";
			return View("MyView", employeeListViewModel);

		}
    }
}