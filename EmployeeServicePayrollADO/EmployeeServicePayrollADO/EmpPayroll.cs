﻿using System;
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
        public DateTime StartDate { get; set; }
        public string Gender { get; set; }
        public double PhoneNumber { get; set; } 
        public string Address { get; set; }
        public string Department { get; set; }  
        public double BasicPay { get; set; }
        public int Deduction { get; set; }
        public int TaxablePay { get; set; }
        public int IncomeTax { get; set; }
        public int NetPay { get; set; }
    }
}