using EmployeeServicePayrollADO;
using NUnit.Framework;
using System;

namespace EmplyoeePayrollTesting
{
    public class Tests
    {
        Program program;
        [SetUp]
        public void Setup()
        {
            program = new Program();
        }
        /// <summary>
        /// UC2 - Get the all Employee Payroll Data
        /// </summary>
        [Test]
        public void Get_AllEmployeePayrollData()
        {
            var actual = Program.GetAllEmployeePayrollData();
            Assert.AreEqual(3, actual.Count);
        }
        /// <summary>
        /// UC 3 - Update the Salary of Emplyoee
        /// </summary>
        [Test]
        public void UpdateEmployeeSalary_ShouldReturn_True_AfterUpdate()
        {
            bool expected = true;
            EmpPayroll empPayroll = new EmpPayroll();
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
    }
}