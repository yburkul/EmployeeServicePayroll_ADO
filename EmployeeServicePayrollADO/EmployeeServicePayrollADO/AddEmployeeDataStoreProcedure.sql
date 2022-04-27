USE [Emp_Payroll]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter procedure insertDetails
(@ID int,
@Name varchar(50),
@StartDate date,
@Gender varchar(1),
@PhoneNumber bigint,
@Address varchar(50),
@Department varchar(50),
@BasicPay bigint,
@Deduction bigint,
@TaxablePay bigint,
@IncomeTax bigint,
@NetPay bigint
)
As
Begin
declare @new_identity int = 0

Insert into Employee(Name,StartDate,Gender,PhoneNumber,Address) Values (@Name,@StartDate,@Gender,@PhoneNumber,@Address);
select @new_identity = @@IDENTITY
SET IDENTITY_INSERT Payroll ON
insert into Payroll(Payroll.ID,BasicPay,Deduction,TaxablePay,IncomeTax,NetPay) values (@new_identity,@BasicPay,@Deduction,@TaxablePay,@IncomeTax,@NetPay);
SET IDENTITY_INSERT Payroll OFF;
insert into DepartmentTable (DepartmentTable.ID,Department)values(@new_identity,@Department);
End

select Employee.EmpID, Name,StartDate,Gender,PhoneNumber,Address,Department,BasicPay,Deduction,TaxablePay,IncomeTax,NetPay from Employee 
left join DepartmentTable on Employee.EmpID = DepartmentTable.ID left join Payroll on Employee.EmpID = Payroll.ID;
