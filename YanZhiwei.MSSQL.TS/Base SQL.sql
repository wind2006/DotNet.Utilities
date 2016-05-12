--1. 触发器
/*
触发器是用在对数据表的记录进行insert,update,delete时,会对数据表中相关联的数据表进行相应的操作,要对相应的表进行insert,update,delete中的哪一种或哪几种操作,这完全由我们的需求来定.

"中间表"的概念:
1>当我们对源表进行insert操作时,会产生一个inserted表,插入到源表中的记录会先放入到inserted表中,然后再最终插入到源表中;
2>当我们对源表进行update操作时,会产生deleted表和inserted表,先把更新的原始记录放入到deleted表,然后再将更新的记录放入到inserted表,最后才更新源表的相应记录;
3>当我们对源表进行删除操作时,会产生deleted表,先将要删除的记录放入到deleted表,最后才真正删除源表中相应记录.
对于这几种情况一定要非常了解,这样,进行触发时,我们关联其它表触发相应操作时,就非常简单了

触发器是一中『特殊的存储过程，主要是通过事件来触发而被执行的』。它可以『强化约束，来维护数据的完整性和一致性，可以跟踪数据库内的操作从而不允许未经许可的更新和变化』。可以联级运算。如，某表上的触发器上包含对另一个表的数据操作，而该操作又会导致该表触发器被触发。

触发器『对表进行插入、更新、删除的时候会自动执行的特殊存储过程』。触发器一般用在check约束更加复杂的约束上面。触发器和普通的存储过程的区别是：触发器是当对某一个表进行操作。诸如：update、insert、delete这些操作的时候，系统会自动调用执行该表上对应的触发器。SQL Server 2005中触发器可以分为两类：DML触发器和DDL触发器，其中DDL触发器它们会影响多种数据定义语言语句而激发，这些语句有create、alter、drop语句。

触发器有两个特殊的表：插入表（instered表）和删除表（deleted表）。这两张是逻辑表也是虚表。
这两张表的结果总是与被改触发器应用的表的结构相同。当触发器完成工作后，这两张表就会被删除。 
Inserted表的数据是插入或是修改后的数据，而deleted表的数据是更新前的或是删除的数据。 

SQL Server 2005中触发器可以分为两类：DML触发器和DDL触发器，其中DDL触发器它们会影响多种数据定义语言语句而激发，这些语句有create、alter、drop语句。  

--DML 触发器分为    
   --1、 after触发器（之后触发)   
   --      a、 insert触发器    
   --      b、 update触发器    
   --      c、 delete触发器    
   --PS：其中after触发器要求只有执行某一操作insert、update、delete之后触发器才被触发，且只能定义在表上   
   --  2、 instead of 触发器 （之前触发） 
   而instead of触发器表示并不执行其定义的操作（insert、update、delete）而仅是执行触发器本身。既可以在表上定义instead of触发器，也可以在视图上定义。
 
*/
use BaseDB
CREATE TABLE student2
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(50) NULL, 
    [age] NCHAR(10) NULL, 
    [class] NVARCHAR(50) NULL
)
go
CREATE TABLE student
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(50) NULL, 
    [age] NCHAR(10) NULL, 
    [class] NVARCHAR(50) NULL,
	starttime datetime null,
	endtime datetime null,
	inserttime datetime null
)
go
--1.1.1 创建Insert插入类型触发器    
create trigger trig_insert
on student2
for insert
as
declare @name nvarchar(50),@age int,@id int;
select @id=id,@name=name,@age=age from inserted;
set @name=@id+CONVERT(varchar,@id);
set @id=@id+1;
insert student values(@id,@name,@age,'inserted',GETDATE(),GETDATE(),GETDATE());
print '触发Insert'
go
--1.1.2 创建Insert插入类型触发器_测试验证
insert student2 values(3,'Yan',18,'inserting')
select * from student
select * from student2
go
--1.2.1  创建删除类型触发器  
create trigger trig_deleted
on student2
for delete
as
print '开始备份...'
select * into student2backup from deleted
go
--1.2.2  创建删除类型触发器_测试验证
delete student2 where Id=1
select * from student
select * from student2
select * from student2backup
go
--1.3.1 创建update类型的触发器
create trigger trig_update
on student2
for update 
as
declare @oldName nvarchar(50),@newName nvarchar(50);
select @oldName=name from deleted;
select @newName=name from inserted;
print '更新之前名称：'+@oldName;
print '更新之后名称:'+@newName;
go
--1.3.2 创建update类型的触发器_测试验证
update  student2 set name='Zhiwei' where Id=3
go
--1.4.1 创建instead of 触发器
create trigger trig_insteadof_deleted
on student2
instead of delete --insert ,update
as
declare @name nvarchar(50);
select @name=name from deleted;
print '已经删除:'+@name;
go
--1.4.2 创建instead of 触发器
delete student2 where id=3;
select * from student2;

--2.存储过程
/*
存储过程是一个预编译的SQL语句，优点是允许模块化的设计，就是说只需创建一次，以后在该程序中就可以调用多次。如果某次操作需要执行多次SQL，使用存储过程比单纯SQL语句执行要快。可以用一个命令对象来调用存储过程。
*/

--3.索引
/*
索引就一种特殊的查询表，数据库的搜索引擎可以利用它加速对数据的检索。它很类似与现实生活中书的目录，不需要查询整本书内容就可以找到想要的数据。索引可以是唯一的，创建索引允许指定单个列或者是多个列。缺点是它减慢了数据录入的速度，同时也增加了数据库的尺寸大小。

索引是一个数据结构，用来快速访问数据库表格或者视图里的数据。
在SQL Server里，它们有两种形式:聚集索引和非聚集索引。聚集索引在索引的叶级保存数据。这意味着不论聚集索引里有表格的哪个(或哪些)字段，这些字段都会按顺序被保存在表格。由于存在这种排序，所以每个表格只会有一个聚集索引。非聚集索引在索引的叶级有一个行标识符。这个行标识符是一个指向磁盘上数据的指针。它允许每个表格有多个非聚集索引。
*/

--4.维护数据库的完整性和一致性，你喜欢用触发器还是自写业务逻辑？为什么？
/*
尽可能使用约束，如check,主键，外键，非空字段等来约束，这样做效率最高，也最方便。其次是使用触发器，这种方法可以保证，无论什么业务系统访问数据库都可以保证数据的完整新和一致性。最后考虑的是自写业务逻辑，但这样做麻烦，编程复杂，效率低下。
*/

--5. 事物，锁
/*
事务就是被绑定在一起作为一个逻辑工作单元的SQL语句分组，如果任何一个语句操作失败那么整个操作就被失败，以后操作就会回滚到操作前状态，或者是上有个节点。为了确保要么执行，要么不执行，就可以使用事务。要将有组语句作为事务考虑，就需要通过ACID测试，即原子性，一致性，隔离性和持久性。
T-SQL语句在事务中"标记"这些点。
BEGIN TRAN:设置起始点。
•	COMMIT TRAN:使事务成为数据库中永久的、不可逆转的一部分。
•	ROLLBACK TRAN:本质上说想要忘记它曾经发生过。
•	SAVE TRAN:创建一个特定标记符，只允许部分回滚。

锁：在所以的DBMS中，锁是实现事务的关键，锁可以保证事务的完整性和并发性。与现实生活中锁一样，它可以使某些数据的拥有者，在某段时间内不能使用某些数据或数据结构。当然锁还分级别的。

*/

--6 视图，游标
/*
视图是一种虚拟的表，具有和物理表相同的功能。可以对视图进行增，改，查，操作，试图通常是有一个表或者多个表的行或列的子集。对视图的修改不影响基本表。它使得我们获取数据更容易，相比多表查询。

游标：是对查询出来的结果集作为一个单元来有效的处理。游标可以定在该单元中的特定行，从结果集的当前行检索一行或多行。可以对结果集当前行做修改。一般不使用游标，但是需要逐条处理数据的时候，游标显得十分重要。
*/

--7. NULL
/*
NULL这个值表示UNKNOWN(未知),它不表示空字符串；
假设您的SQL Server数据库里有ANSI_NULLS，当然在默认情况下会有，对NULL这个值的任何比较都会生产一个NULL值。您不能把任何值与一个 UNKNOWN值进行比较，并在逻辑上希望获得一个答案。您必须使用IS NULL操作符。
*/

--8. 主键，外键
/*
主键是表格里的(一个或多个)字段，只用来定义表格里的行;主键里的值总是唯一的。外键是一个用来建立两个表格之间关系的约束。这种关系一般都涉及一个表格里的主键字段与另外一个表格(尽管可能是同一个表格)里的一系列相连的字段。那么这些相连的字段就是外键。
*/

