-- this will not work for sqlite
-- sqlite doesn't support stored procedures
create procedure GetEmployeesByHireDate
       @HireDate datetime
as
begin
    set noncount on;
    select * from Employees 
    where HireDate >= @HireDate
    order by HireDate desc;
end;