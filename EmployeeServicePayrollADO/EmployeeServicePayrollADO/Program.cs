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
                    SqlCommand command = new SqlCommand("UpdateEmplyoeeSalary", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
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
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in Employee Service Payroll");
            Program program = new Program();
            program.EstablishConnection();
            program.CloseConnection();
            GetAllEmployeePayrollData();
        }
    }
}