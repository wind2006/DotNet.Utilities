use PracticeDB
create table student
(
   s# int,
   sname nvarchar(32),
   sage int,
   ssex nvarchar(8)
)

create table course
(
   c# int,
   cname nvarchar(32),
   t# int
)

create table sc
(
    s# int,
    c# int,
    score int
)

create table teacher(
  t# int,
  tname nvarchar(32)
)


insert into Student select 1,N'刘一',18,N'男' union all
select 2,N'钱二',19,N'女' union all
select 3,N'张三',17,N'男' union all
select 4,N'李四',18,N'女' union all
select 5,N'王五',17,N'男' union all
select 6,N'赵六',19,N'女'
insert into Teacher select 1,N'叶平' union all
select 2,N'贺高' union all
select 3,N'杨艳' union all
select 4,N'周磊'
insert into Course select 1,N'语文',1 union all
select 2,N'数学',2 union all
select 3,N'英语',3 union all
select 4,N'物理',4
insert into SC
select 1,1,56 union all
select 1,2,78 union all
select 1,3,67 union all
select 1,4,58 union all
select 2,1,79 union all
select 2,2,81 union all
select 2,3,92 union all
select 2,4,68 union all
select 3,1,91 union all
select 3,2,47 union all
select 3,3,88 union all
select 3,4,56 union all
select 4,2,88 union all
select 4,3,90 union all
select 4,4,93 union all
select 5,1,46 union all
select 5,3,78 union all
select 5,4,53 union all
select 6,1,35 union all
select 6,2,68 union all
select 6,4,71

--1、查询'1'课程比'2'课程成绩高的所有学生的学号；
select a.s# from 
(select s#,score from dbo.sc where c#=1) a,
(select s#,score from dbo.sc where c#=2) b
where 
a.score>b.score and
a.s#=b.s#

--2、查询平均成绩大于60分的同学的学号和平均成绩
select s#,AVG(score) 
from dbo.sc
group by s#
having AVG(score)>60

--3、查询所有同学的学号、姓名、选课数、总成绩；
select s.s# as '学号',s.sname as '姓名',
count(c.c#) as '课程总数',sum(c.score) as '课程总分'
from dbo.student s
left join dbo.sc c 
on s.s#=c.s#
group by s.s#,s.sname

--4、查询姓'李'的老师的个数；
select count(distinct(tname)) from  teacher
where tname like '李%'

--5、查询没学过'叶平'老师课的同学的学号、姓名；
--5.1 查询'叶平'所教授的课程
select c.c#,c.cname,t.tname from dbo.teacher t 
left join dbo.course c
on t.t#=c.t#
where t.tname='叶平'
--5.2 查询学习'叶平'老师课程的学号
select distinct(sc.s#) from sc
inner join 
(select c.c#,c.cname,t.tname from dbo.teacher t 
left join dbo.course c
on t.t#=c.t#
where t.tname='叶平') tc 
on sc.c#=tc.c#
--5.3 查询所有学号不在(学习'叶平'老师课程的学号)中的学号
select st.s#,st.sname from student st
where st.s# not in
(select distinct(sc.s#) from sc
inner join 
(select c.c#,c.cname,t.tname from dbo.teacher t 
left join dbo.course c
on t.t#=c.t#
where t.tname='叶平') tc 
on sc.c#=tc.c#) 

--6.查询学过'1'并且也学过编号'2'课程的同学的学号,姓名;
--6.1 查询学过'1'的学号
select sc1.s# from sc sc1
where sc1.c#=1
--6.2 查询学过'2'的学号
select sc1.s# from sc sc1
where sc1.c#=2
--6.4 查询学过'1'并'2'的学号
select scc1.s# from 
(select sc1.s# from sc sc1
where sc1.c#=1) scc1,
(select sc1.s# from sc sc1
where sc1.c#=2) scc2
where scc1.s#=scc2.s#
--6.5 综合
select s.s#,s.sname from student s
right join 
(select scc1.s# from 
(select sc1.s# from sc sc1
where sc1.c#=1) scc1,
(select sc1.s# from sc sc1
where sc1.c#=2) scc2
where scc1.s#=scc2.s#) scc
on scc.s#=s.s#
--6.6 其他实现
select Student.S#,Student.Sname from Student,SC where Student.S#=SC.S# and SC.C#='001'
and exists( Select * from SC as SC_2 where SC_2.S#=SC.S# and SC_2.C#='002');

--7.查询学过'叶平'老师所教的所有课的同学的学号,姓名
--7.1 查询'叶平'老师所教授课程编号
select c.c# from teacher t left join course c
on t.t#=c.t# 
where t.tname='叶平'
--7.2 查询学习'叶平'老师课程的学生编号
select sc.s# from sc,
(select c.c# from teacher t left join course c
on t.t#=c.t# 
where t.tname='叶平') tc
where sc.c#=tc.c#
--7.3 综合
select st.s#,st.sname from student st
where st.s# in 
(select sc.s# from sc,
(select c.c# from teacher t left join course c
on t.t#=c.t# 
where t.tname='叶平') tc
where sc.c#=tc.c#)
-- 7.4 其他实现
select S#,Sname
from Student
where S# in (select S# from SC ,Course ,Teacher where SC.C#=Course.C# and Teacher.T#=Course.T# and Teacher.Tname='叶平' group by S# having count(SC.C#)=(select count(C#) from Course,Teacher where Teacher.T#=Course.T# and Tname='叶平'));

--8、查询课程编号'2'的成绩比课程编号'1'课程低的所有同学的学号、姓名；
select * from student where s#=
(select  sc1.s# from 
(select s#,score from sc where sc.c#=2) sc1,
(select s#,score from sc  where c#=1) sc2
where sc1.s#=sc2.s# and sc1.score<sc2.score)
--8.1 其他实现
Select S#,Sname from (select Student.S#,Student.Sname,score,(select score from SC SC_2 where SC_2.S#=Student.S# and SC_2.C#='002') score2 from Student,SC where Student.S#=SC.S# and C#='001') S_2 where score2 <score;

--9、查询所有课程成绩小于60分的同学的学号、姓名；
select s#,sname from student where s# in
(select distinct(s#) from sc 
where score<60)

--10、查询没有学全所有课的同学的学号、姓名；
select s.sname,count(sc.c#) from student s left join sc on s.s#=sc.s#
group by s.sname 
having count(sc.c#)<(select count(*) from course)
--10.1 其他实现
select Student.S#,Student.Sname
from Student,SC
where Student.S#=SC.S# group by Student.S#,Student.Sname having count(C#) <(select count(C#) from Course);

--11、查询至少有一门课与学号为'1'的同学所学相同的同学的学号和姓名；
select distinct sc.s#,Sname from Student,SC where Student.S#=SC.S# and SC.C# in (select C# from SC where S#='1');

--为管理岗位业务培训信息，建立3个表:
--  S (S#,SN,SD,SA) S#,SN,SD,SA 分别代表学号、学员姓名、所属单位、学员年龄
--　C (C#，CN) C#,CN 分别代表课程编号、课程名称
--　SC ( S#,C#,G ) S#,C#,G 分别代表学号、所选修的课程编号、学习成绩

Create table S
(
S# varchar(10),
SN varchar(20),
SD varchar(50),
SA int
)

Create table C
(
 C# varchar(10),
CN varchar(30),
)
Create table SC
(
 S# varchar(10),
C# varchar(10),
G varchar(6)
)

--a)使用标准SQL嵌套语句查询选修课程名称为’税收基础’的学员学号和姓名
select * from S where S# in
(
select sc.S# from SC 
left join 
C on sc.C#=c.C# 
where c.CN='税收基础'
)
--b)使用标准SQL嵌套语句查询选修课程编号为’C2’的学员姓名和所属单位
select * from S where s# in (select sc.S# from SC where sc.C#='C2')
--c)使用标准SQL嵌套语句查询不选修课程编号为’C5’的学员姓名和所属单位
select * from S where s# not in (select sc.S# from SC where sc.C#='C5')
--d)使用标准SQL嵌套语句查询选修全部课程的学员姓名和所属单位
select * from S where S# in (
select s# from SC left join
C on sc.C#=c.C#
group by SC.S#
having COUNT(distinct(sc.C#))=
(select COUNT(*) from C)
)
--e)查询选修了课程的学员人数
select COUNT(distinct(s#)) from SC;
--f)查询选修课程超过5门的学员学号和所属单位
select * from S where S# in(
select s# from SC 
group by sc.C#
having COUNT(distinct(sc.c#))>5
)


--表内容：
--2005-05-09 胜
--2005-05-09 胜
--2005-05-09 负
--2005-05-09 负
--2005-05-10 胜
--2005-05-10 负
--2005-05-10 负

--如果要生成下列结果, 该如何写sql语句?
--胜 负
--2005-05-09 2 2
--2005-05-10 1 2
create table Sport(rq varchar(10),shengfu nchar(1))
go
insert into Sport values('2005-05-09','胜')
insert into Sport values('2005-05-09','胜')
insert into Sport values('2005-05-09','负')
insert into Sport values('2005-05-09','负')
insert into Sport values('2005-05-10','胜')
insert into Sport values('2005-05-10','负')
insert into Sport values('2005-05-10','负')
go
select * from Sport
go
select rq,
       sum(case when shengfu='胜' then 1 else 0  end )'胜利',
       sum(case when shengfu='负' then 1 else 0 end)'失败' 
       from sport 
       group by rq

--先创建一个临时表，只有ID(员工编号)与name(员工名称)两个字段
create table yg
(
ID int primary key identity(1,1),
name varchar(50) not null
)

--然后往该表中插入员工姓名，包括两个重复姓名('张三'和'李明')
insert into yg(name) 
select '张三'
union all
select '张三'
union all
select '李明'
union all
select '李明'
union all
select '李四'
go
--接下来我们要如何查询出有重复姓名的'张三'和'李明'呢？利用group by与having条件即可
select name from yg group by name having count(name)>1
--重复的记录是有查询出来了，那么我们要如何删除其中重复的记录，只留下一条数据呢？
delete yg where ID in( select MAX(id) from yg group by name having count(name)>1)
select * from yg

--已知一个表的结构为：
---------------------
--姓名 科目 成绩
--张三 语文 20
--张三 数学 30
--张三 英语 50
--李四 语文 70
--李四 数学 60
--李四 英语 90

create table cj
(
  name nvarchar(20),
  kemu nvarchar(20),
  fengshu int
)
go
insert cj values('张三','语文',20)
insert cj values('张三','数学',30)
insert cj values('张三','英语',50)
insert cj values('李四','语文',70)
insert cj values('李四','数学',60)
insert cj values('李四','英语',90)
go
select * from cj

--怎样通过select语句把他变成以下结构：
--------------------------------------
--姓名 语文成绩 数学成绩 英语成绩
--张三      20          30          50
--李四      70          60          90
select name as '姓名',
      sum(case when kemu='语文' then fengshu else 0 end) as '语文成绩',
      sum(case when kemu='数学' then fengshu else 0 end) as '数学成绩',
      sum(case when kemu='数学' then fengshu else 0 end) as '数学成绩'
       from cj
       group by name
--姓名 语文成绩 数学成绩 英语成绩 平均成绩 总成绩
--张三      20          30          50          33         100
--李四      70          60          90          73          220

select name as '姓名',
      sum(case when kemu='语文' then fengshu else 0 end) as '语文成绩',
      sum(case when kemu='数学' then fengshu else 0 end) as '数学成绩',
      sum(case when kemu='数学' then fengshu else 0 end) as '数学成绩',
      avg(fengshu) as '平均成绩',
      SUM(fengshu) as '总成绩'
       from cj
       group by name
       order by name desc
