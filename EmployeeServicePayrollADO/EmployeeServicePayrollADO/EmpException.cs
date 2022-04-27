using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServicePayrollADO
{
    public class EmpException : Exception
    {
        ExceptionType exceptionType;
        public enum ExceptionType
        {
            Connection_Failed, Salary_Not_Update, Details_Not_Coorect_Format
        }
        public EmpException(ExceptionType exceptionType, string message) : base(message)
        {
            this.exceptionType = exceptionType;
        }
    }
}