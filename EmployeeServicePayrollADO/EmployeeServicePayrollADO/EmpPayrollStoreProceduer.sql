Use [Emp_Payroll]
Go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [GetAllEmployeePayrollData]
AS
BEGIN
	SELECT Employee.EmpID, Name,StartDate,Gender,PhoneNumber,Address,Department,BasicPay,Deduction,TaxablePay,IncomeTax,NetPay from Employee 
left join DepartmentTable on Employee.EmpID = DepartmentTable.ID left join Payroll on Employee.EmpID = Payroll.ID;
END
GO