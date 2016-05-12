using  Sample

------行列转换------
if object_id('tb') is not null drop table tb
go

create table tb
(姓名 varchar(10),
 课程 varchar(10),
 分数 int)

insert into tb values('张三','语文',74)
insert into tb values('张三','数学',83)
insert into tb values('张三','物理',93)
insert into tb values('李四','语文',74)
insert into tb values('李四','数学',84)
insert into tb values('李四','物理',94)
go

select * from tb
-------1. 使用SQL Server 2000静态SQL-----
select 姓名,
 max(case 课程 when'语文'then 分数 else 0 end)语文,
 max(case 课程 when'数学'then 分数 else 0 end)数学,
 max(case 课程 when '物理' then 分数 else 0 end)物理
from tb
group by 姓名

-------2. 使用SQL Server 2000动态SQL-----
declare @sql varchar(500)
set @sql='select 姓名'
select @sql=@sql+',max(case 课程 when '''+课程+''' then 分数 else 0 end)['+课程+']'
from(select distinct 课程 from tb ) a--同from tb group by课程，默认按课程名排序
set @sql=@sql+' from tb group by 姓名'
print(@sql)
exec(@sql)


-------行列合并-------
if object_id('tb2') is not null drop table tb2
go
create table tb2(id int, value varchar(10))
insert into tb2 values(1, 'aa')
insert into tb2 values(1, 'bb')
insert into tb2 values(2, 'aaa')
insert into tb2 values(2, 'bbb')
insert into tb2 values(2, 'ccc')
go
select * from tb2

-------1. 使用SQL Server 2005中-----
select id, [values]=stuff((select ','+[value] from tb2 t where id=tb2.id for xml path('')), 1, 1, '')
from tb2
group by id
-------2. 使用SQL Server 2005中用OUTER APPLY-----
SELECT * FROM(SELECT DISTINCT id FROM tb2)A OUTER APPLY(
  SELECT [values]= STUFF(REPLACE(REPLACE(
  (
  SELECT value FROM tb2 N
  WHERE id = A.id
  FOR XML AUTO
  ), '<N value="', ','), '"/>', ''), 1, 1, '')
)N

-------3. 使用合并函数-----
create function f_hb(@id int)
returns varchar(8000)
as
begin
  declare @str varchar(8000)
  set @str = ''
  select @str = @str + ',' + cast(value as varchar) from tb2 where id = @id
  set @str = right(@str , len(@str) - 1)
  return(@str)
End
go

select distinct id ,dbo.f_hb(id) as value from tb2