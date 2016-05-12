use Northwind
select  top 10 * from  Products where ProductID  not in (select top 30 ProductID  from dbo.Products)
go
select * from 
(select ROW_NUMBER() over(order by ProductID) as ID,* from dbo.Products)
temp
where temp.id between 31 and 40
