using System;

namespace EmployeeServicePayrollADO
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in Employee Service Payroll");
            EmpDetails empDetails = new EmpDetails();
            empDetails.EstablishConnection();
            empDetails.CloseConnection();
        }
    }
}