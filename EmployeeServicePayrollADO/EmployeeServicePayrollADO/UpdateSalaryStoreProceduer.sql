use [Emp_Payroll]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [UpdateEmplyoeeSalary] 
	-- Add the parameters for the stored procedure here
	@EmpID int,
	@Name varchar(50),
	@BasicPay Money,
	@Deduction Money,
	@TaxablePay Money,
	@IncomeTax Money,
	@NetPay Money
AS
BEGIN
	SET NOCOUNT ON;
update Payroll Set BasicPay = @BasicPay, Deduction = @Deduction, TaxablePay = @TaxablePay, IncomeTax = @IncomeTax, NetPay = @NetPay where ID = @EmpID;

	select Employee.EmpID, Name,StartDate,Gender,PhoneNumber,Address,Department,BasicPay,Deduction,TaxablePay,IncomeTax,NetPay from Employee 
left join DepartmentTable on Employee.EmpID = DepartmentTable.ID left join Payroll on Employee.EmpID = Payroll.ID;
END
GO
