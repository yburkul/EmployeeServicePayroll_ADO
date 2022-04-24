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
    }
}