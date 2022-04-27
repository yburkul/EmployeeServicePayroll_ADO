using EmployeeServicePayrollADO;
using NUnit.Framework;
using System;

namespace EmplyoeePayrollTesting
{
    public class Tests
    {
        Program program;
        EmpPayroll empPayroll;
        [SetUp]
        public void Setup()
        {
            program = new Program();
            empPayroll = new EmpPayroll();
        }
        /// <summary>
        /// TC - Get the all Employee Payroll Data
        /// </summary>
        [Test]
        public void Get_AllEmployeePayrollData()
        {
            var actual = Program.GetAllEmployeePayrollData();
            Assert.AreEqual(3, actual.Count);
        }
        /// <summary>
        /// TC - Update the Salary of Emplyoee
        /// </summary>
        [Test]
        public void UpdateEmployeeSalary_ShouldReturn_True_AfterUpdate()
        {
            bool expected = true;
            empPayroll.ID = 1;
            empPayroll.Name = "Yogesh";
            empPayroll.BasicPay = 3000000;
            empPayroll.Deduction = 10000;
            empPayroll.TaxablePay = 7000;
            empPayroll.IncomeTax = 5000;
            empPayroll.NetPay = 2978000;
            bool result = program.UpdateEmployeeSalary(empPayroll);
            Assert.AreEqual(expected, result);
        }
        /// <summary>
        /// TC- Get Employee Data In Date Range
        /// </summary>
        [Test]
        public void Given_DateRange_GetEmployeePayrollData()
        {
            bool expected = true;
            var fromDate = Convert.ToDateTime("2022-03-01");
            var ToDate = Convert.ToDateTime("2022-04-01");
            bool result = program.GetEmplyeeDataInDateRange(fromDate, ToDate);
            Assert.AreEqual(expected, result);
        }
        /// <summary>
        /// TC - Add the Employee Data 
        /// </summary>
        [Test]
        public void AddEmployeeData()
        {
            bool expected = true;
            empPayroll.Name = "Nikita";
            empPayroll.StartDate = Convert.ToDateTime("2022-03-09");
            empPayroll.Gender = "F";
            empPayroll.PhoneNumber = 889900766;
            empPayroll.Address = "Pune";
            empPayroll.Department = "IT";
            empPayroll.BasicPay = 40000;
            empPayroll.Deduction = 1000;
            empPayroll.TaxablePay = 1000;
            empPayroll.IncomeTax = 1000;
            empPayroll.NetPay = 37000;
            bool result = program.AddEmployee(empPayroll);
            Assert.AreEqual(expected, result);
        }
    }
}