using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServicePayrollADO
{
    public class EmpPayroll
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string Gender { get; set; }
        public double Phone { get; set; } 
        public string Address { get; set; }
        public string Department { get; set; }  
        public double Salary { get; set; }
        public int Deductions { get; set; }
        public int Taxable_Pay { get; set; }
        public int Income_Tax { get; set; }
        public int Net_Pay { get; set; }
    }
}
