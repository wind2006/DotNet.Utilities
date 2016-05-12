--1、说明：复制表(只复制结构,源表名：a 新表名：b)

CREATE TABLE [dbo].[Copy1]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [s_Name] NVARCHAR(50) NULL, 
    [s_Age] INT NULL
)
CREATE TABLE [dbo].[Copy2]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [s_Name] NVARCHAR(50) NULL, 
    [s_Age] INT NULL
)
go
insert Copy1 values(1,'aa',18)
insert Copy1 values(2,'bb',19)
insert Copy1 values(3,'cc',20)
go
select * from Copy1
select * from Copy2

--1.1 得到表结构
select * from Copy1 where 1<>1
select top 0 * from Copy1
--1.2 使用into关键字
select * into Copy3 from Copy1 where 1<>1
select top 0 * into Copy4 from Copy1
--1.3 引申资料
/*
SQL SELECT INTO 语句可用于创建表的备份复件。
SELECT INTO 语句从一个表中选取数据，然后把数据插入另一个表中。
SELECT INTO 语句常用于创建表的备份复件或者用于对记录进行存档。
*/

--2、说明：拷贝表(拷贝数据,源表名：a 目标表名：b)
select * into Copy5 from Copy1

--3、说明：between的用法,between限制查询数据范围时包括了边界值,not between不包括
select * from Copy1 where s_Age between 18 and 19

--4、说明：随机取出2条数据
select top 2 *,NEWID() from Copy1 order by NEWID()

--5、说明：随机选择记录
select NEWID()
--5.1 引申资料
/*
因为newid()返回的是uniqueidentifier类型的唯一值。newid()每次产生的值都不一样，那么根据这样的值进行排序，每次的结果也是不一样的。
NEWID 对每台计算机返回的值各不相同。
uniqueidentifier中文含义“唯一的标识符”。
uniqueidentifier数据类型是16个字节的二进制值，应具有唯一性，必须与NEWID（）函数配合使用。
uniqueidentifier数据类型与identity自增不同，不会为插入的新行自动生成新的ID，新值由NEWID（）函数指定。
NEWID（）函数值会生成全球唯一的标识，标识由网卡号和CPU时钟组成，如：6F9619FF-8B86-D011-B42D-00C04FC964FF。

uniqueidentifier 数据类型具有下列缺点： 
1.值长且难懂。这使用户难以正确键入它们，并且更难记住。 
2.这些值是随机的，而且它们不支持任何使其对用户更有意义的模式。 
3.也没有任何方式可以决定生成 uniqueidentifier 值的顺序。它们不适用于那些依赖递增的键值的现有应用程序。 
4.当uniqueidentifier 为 16 字节时，其数据类型比其他数据类型（例如 4 字节的整数）大。这意味着使用 uniqueidentifier 键生成索引的速度相对慢于使用 int 键生成索引的速度。 
5.只对没有其他适用的数据类型的范围非常窄的方案使用 GUID。 
*/


--6、说明：删除重复记录
CREATE TABLE [dbo].[Repeat]
( 
    [r_Id] INT NOT NULL PRIMARY KEY, 
    [r_Name] NVARCHAR(50) NULL, 
    [r_Age] INT NULL
)
go
insert [Repeat] values(1,N'楚人游子',22)
insert [Repeat] values(2,N'楚人游子',22)
insert [Repeat] values(3,N'楚人游子',22)
insert [Repeat] values(4,N'楚人游子',21)
insert [Repeat] values(5,N'楚人游子',23)
go
select * from [Repeat]

--6.1 查询唯一不重复数据
select MAX(r_Id),r_Name from [Repeat]
group by r_Name,r_Age
--6.2 删除
delete from [Repeat] where r_Id not in
(select MAX(r_Id) from [Repeat]
group by r_Name,r_Age)

--7、说明：列出数据库里所有的表名
select name from sysobjects where type='U'

--8、说明：列出表里的所有的列
select name from syscolumns where id=object_id('Copy1')

--9、查询数据的最大排序问题（只能用一条语句写）
CREATE TABLE hard 
(
qu char (11) ,
co char (11) ,
je numeric(3, 0)
)
go
insert into hard values ('A','1',3)
insert into hard values ('A','2',4)
insert into hard values ('A','4',2)
insert into hard values ('A','6',9)
insert into hard values ('B','1',4)
insert into hard values ('B','2',5)
insert into hard values ('B','3',6)
insert into hard values ('C','3',4)
insert into hard values ('C','6',7)
insert into hard values ('C','2',3)
go

--9.1.1 先执行主查询
select * from hard a
ORDER BY qu,je DESC 
--9.1.2 将主查询参数传入相关子查询
select count(*) from hard b 
where b.qu='A' and b.je>=6
--9.1.3 综合
select * from hard a where (select count(*) from hard b 
where a.qu=b.qu and b.je>=a.je)<=2 
ORDER BY qu,je DESC 
--9.1.4 参考资料
/*
相关子查询实际上会执行N次（N取决与外部查询的行数），外部查询每执行一行，都会将对应行所用的参数传到子查询中.
当子查询作为计算列使用时，会针对外部查询的每一行，返回唯一的值。

（1）从外层查询中取出一个元组，将元组相关列的值传给内层查询。
（2）执行内层查询，得到子查询操作的值。
（3）外查询根据子查询返回的结果或结果集得到满足条件的行。
（4）然后外层查询取出下一个元组重复做步骤1-3，直到外层的元组全部处理完毕。 
*/ 
--9.2 其他实现
select qu,co,je 
 from (select ROW_NUMBER()over(PARTITION by qu  order by je desc)as row,*
           from hard)as t
 where row<=2;

