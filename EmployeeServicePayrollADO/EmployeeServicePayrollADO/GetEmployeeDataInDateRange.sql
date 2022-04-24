use [Emp_Payroll]
Go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [GetEmployeePayrollDataInDateRange] 
	-- Add the parameters for the stored procedure here
	@FromDate Date,
	@ToDate Date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Employee.EmpID, Name,StartDate,Gender,PhoneNumber,Address,Department,BasicPay,Deduction,TaxablePay,IncomeTax,NetPay from Employee 
left join DepartmentTable on Employee.EmpID = DepartmentTable.ID left join Payroll on Employee.EmpID = Payroll.ID where StartDate between @FromDate and @ToDate;
END
GO