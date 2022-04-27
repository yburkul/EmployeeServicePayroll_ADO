using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServicePayrollADO
{
    public class Program
    {
        static string connectionString = @"Data Source=DESKTOP-DNLCRQ5\SQLEXPRESS;Initial Catalog=Emp_Payroll;Integrated Security=True;";
        static SqlConnection sqlconnection = new SqlConnection(connectionString);
        public void EstablishConnection()
        {
            if (sqlconnection != null && sqlconnection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    sqlconnection.Open();
                }
                catch (Exception)
                {
                    throw new EmpException(EmpException.ExceptionType.Connection_Failed, "connection failed");

                }
            }
        }
        public void CloseConnection()
        {
            if (sqlconnection != null && sqlconnection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    sqlconnection.Close();
                }
                catch (Exception)
                {
                    throw new EmpException(EmpException.ExceptionType.Connection_Failed, "connection failed");
                }
            }
        }
        public static List<EmpPayroll> GetAllEmployeePayrollData()
        {
            List<EmpPayroll> empPayrollList = new List<EmpPayroll>();
            EmpPayroll empPayroll = new EmpPayroll();
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            string Spname = "dbo.GetAllEmployeePayrollData";
            using (sqlconnection)
            {
                SqlCommand sqlCommand = new SqlCommand(Spname, sqlconnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlconnection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        empPayroll.ID = dr.GetInt32(0);
                        empPayroll.Name = dr.GetString(1);
                        empPayroll.StartDate = dr.GetDateTime(2).Date;
                        empPayroll.Gender = (dr.GetString(3));
                        empPayroll.PhoneNumber = dr.IsDBNull(4) ? 0 : dr.GetInt64(4);
                        empPayroll.Address = dr.IsDBNull(5) ? "" : dr.GetString(5);
                        empPayroll.Department = dr.GetString(6);
                        empPayroll.BasicPay = dr.IsDBNull(7) ? 0 : dr.GetInt64(7);
                        empPayroll.Deduction = dr.IsDBNull(8) ? 0 : dr.GetInt32(8);
                        empPayroll.TaxablePay = dr.IsDBNull(9) ? 0 : dr.GetInt32(9);
                        empPayroll.IncomeTax = dr.IsDBNull(10) ? 0 : dr.GetInt32(10);
                        empPayroll.NetPay = dr.IsDBNull(11) ? 0 : dr.GetInt32(11);
                        empPayrollList.Add(empPayroll);
                        Console.WriteLine(empPayroll.ID + "," + empPayroll.Name + "," + empPayroll.StartDate + "," + empPayroll.Gender + "," + empPayroll.PhoneNumber + ","
                            + empPayroll.Address + "," + empPayroll.Department + "," + empPayroll.BasicPay + "," + empPayroll.Deduction + "," + empPayroll.TaxablePay + "," + empPayroll.IncomeTax + "," + empPayroll.NetPay);
                    }
                }
                sqlconnection.Close();
            }
            return empPayrollList;
        }

        public bool UpdateEmployeeSalary(EmpPayroll empPayroll)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("dbo.UpdateEmplyoeeSalary", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpID", empPayroll.ID);
                    command.Parameters.AddWithValue("@Name", empPayroll.Name);
                    command.Parameters.AddWithValue("@BasicPay", empPayroll.BasicPay);
                    command.Parameters.AddWithValue("@Deduction", empPayroll.Deduction);
                    command.Parameters.AddWithValue("@TaxablePay", empPayroll.TaxablePay);
                    command.Parameters.AddWithValue("@IncomeTax", empPayroll.IncomeTax);
                    command.Parameters.AddWithValue("@NetPay", empPayroll.NetPay);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                throw new EmpException(EmpException.ExceptionType.Salary_Not_Update, "Emplyoee Salary Not Updated");
                return false;
            }
        }
        public bool GetEmplyeeDataInDateRange(DateTime fromDate, DateTime toDate)
        {
            try
            {
                EmpPayroll empPayroll = new EmpPayroll();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("dbo.GetEmployeePayrollDataInDateRange", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            empPayroll.ID = dr.GetInt32(0);
                            empPayroll.Name = dr.GetString(1);
                            empPayroll.StartDate = dr.GetDateTime(2);
                            empPayroll.Gender = dr.GetString(3);
                            empPayroll.PhoneNumber = dr.GetInt64(4);
                            empPayroll.Address = dr.GetString(5);
                            empPayroll.Department = dr.GetString(6);
                            empPayroll.BasicPay = dr.GetInt64(7);
                            empPayroll.Deduction = dr.GetInt32(8);
                            empPayroll.TaxablePay = dr.GetInt32(9);
                            empPayroll.IncomeTax = dr.GetInt32(10);
                            empPayroll.NetPay = dr.GetInt32(11); ;
                            Console.WriteLine(empPayroll.ID + "," + empPayroll.Name + "," + empPayroll.StartDate + "," + empPayroll.Gender + "," + empPayroll.PhoneNumber + ","
                            + empPayroll.Address + "," + empPayroll.Department + "," + empPayroll.BasicPay + "," + empPayroll.Deduction + "," + empPayroll.TaxablePay + "," + empPayroll.IncomeTax + "," + empPayroll.NetPay);
                        }
                        return true;
                    }
                    connection.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddEmployee(EmpPayroll empPayroll)
        {
            try
            {
                using (sqlconnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.insertDetails", sqlconnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ID", empPayroll.ID);
                    sqlCommand.Parameters.AddWithValue("@Name", empPayroll.Name);
                    sqlCommand.Parameters.AddWithValue("@StartDate", empPayroll.StartDate);
                    sqlCommand.Parameters.AddWithValue("@Gender", empPayroll.Gender);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", empPayroll.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", empPayroll.Address);
                    sqlCommand.Parameters.AddWithValue("@Department", empPayroll.Department);
                    sqlCommand.Parameters.AddWithValue("@BasicPay", empPayroll.BasicPay);
                    sqlCommand.Parameters.AddWithValue("@Deduction", empPayroll.Deduction);
                    sqlCommand.Parameters.AddWithValue("@TaxablePay", empPayroll.TaxablePay);
                    sqlCommand.Parameters.AddWithValue("@IncomeTax", empPayroll.IncomeTax);
                    sqlCommand.Parameters.AddWithValue("@NetPay", empPayroll.NetPay);
                    Console.WriteLine(empPayroll.ID + "," + empPayroll.Name + "," + empPayroll.StartDate + "," + empPayroll.Gender + "," + empPayroll.PhoneNumber + ","
                            + empPayroll.Address + "," + empPayroll.Department + "," + empPayroll.BasicPay + "," + empPayroll.Deduction + "," + empPayroll.TaxablePay + "," + empPayroll.IncomeTax + "," + empPayroll.NetPay);
                    sqlconnection.Open();

                    var result = sqlCommand.ExecuteNonQuery();
                    sqlconnection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                    sqlconnection.Close();
                }
                sqlconnection.Close();
            }
            catch (Exception)
            {
                throw new EmpException(EmpException.ExceptionType.Details_Not_Coorect_Format, "Details is not correct format");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in Employee Service Payroll");
            Program program = new Program();
            int option = 0;
            do
            {
                Console.WriteLine("1: for EstablishConnection");
                Console.WriteLine("2: for CloseConnection");
                Console.WriteLine("3: for Get all the Emplyoee data");
                Console.WriteLine("4: for Get the Emplyoee Data in Date range");
                Console.WriteLine("5: for Add the Employee Data");
                Console.WriteLine("0: For Exit");
                option = int.Parse(Console.ReadLine()); 
                switch(option)
                {
                    case 1:
                        program.EstablishConnection();
                        break;
                    case 2:
                        program.CloseConnection();
                        break;
                    case 3:
                       GetAllEmployeePayrollData();
                        break;
                    case 4:
                        var fromDate = Convert.ToDateTime("2022-03-01");
                        var ToDate = Convert.ToDateTime("2022-04-01");
                        program.GetEmplyeeDataInDateRange(fromDate, ToDate);
                        break;
                    case 5:
                        EmpPayroll emp = new EmpPayroll();
                        Console.WriteLine("Enter The Name");
                        string name = Console.ReadLine();
                        emp.Name = name;
                        Console.WriteLine(" Emplyoee Join Date");
                        string date = Console.ReadLine();
                        emp.StartDate = Convert.ToDateTime(date);
                        Console.WriteLine("Enter a Gender");
                        string gender = Console.ReadLine();
                        emp.Gender = gender;
                        Console.WriteLine("Enter Phone number");
                        double Phone = Convert.ToInt64(Console.ReadLine());
                        emp.PhoneNumber = Phone;
                        Console.WriteLine("Enter a Address");
                        string address = Console.ReadLine();
                        emp.Address = address;
                        Console.WriteLine("Enter a Department");
                        string department = Console.ReadLine();
                        emp.Department = department;
                        Console.WriteLine("Enter a Basic Pay");
                        double basicpay = Convert.ToInt64(Console.ReadLine());
                        emp.BasicPay = basicpay;
                        Console.WriteLine("Enter a Deduction");
                        int Deduction = int.Parse(Console.ReadLine());
                        emp.Deduction = Deduction;
                        Console.WriteLine("Enter a Taxable Pay");
                        int taxablepay = int.Parse(Console.ReadLine());
                        emp.TaxablePay = taxablepay;
                        Console.WriteLine("Enter a Income Tax");
                        int incometax = int.Parse(Console.ReadLine());
                        emp.IncomeTax = incometax;
                        Console.WriteLine("Enter a NetPay");
                        int netpay = int.Parse(Console.ReadLine());
                        emp.NetPay = netpay;
                        program.AddEmployee(emp);
                        break;
                    case 0:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }                
            }
            while(option!=0);
        }
    }
}