-- =============================================
-- Create date: <2012-9-12>
-- Description:    <高效分页存储过程，适用于Sql2005>
-- Notes:        <排序字段强烈建议建索引>
-- sosoft.cnblogs.com
-- =============================================
create Procedure [dbo].[proc_DataPage] 
 @tableName varchar(50),        --表名
 @fields varchar(1000) = '*',    --字段名(全部字段为*)
 @orderField varchar(1000),        --排序字段(必须!支持多字段)
 @sqlWhere varchar(1000) = Null,--条件语句(不用加where)
 @pageSize int,                    --每页多少条记录
 @pageIndex int = 1 ,            --指定当前为第几页
 @totalPage int output            --返回总页数
as
begin
    Begin Tran --开始事务
    Declare @sql nvarchar(4000);
    Declare @totalRecord int;   
    --计算总记录数
    if (@sqlWhere='' or @sqlWhere=NULL)
        set @sql = 'select @totalRecord = count(*) from ' + @tableName
    else
        set @sql = 'select @totalRecord = count(*) from ' + @tableName + ' where ' + @sqlWhere
    EXEC sp_executesql @sql,N'@totalRecord int OUTPUT',@totalRecord OUTPUT--计算总记录数       
    --计算总数量
    select @totalPage=CEILING((@totalRecord+0.0)/@PageSize)

    if (@sqlWhere='' or @sqlWhere=NULL)
        set @sql = 'Select * FROM (select ROW_NUMBER() Over(order by ' + @orderField + ') as rowId,' + @fields + ' from ' + @tableName 
    else
        set @sql = 'Select * FROM (select ROW_NUMBER() Over(order by ' + @orderField + ') as rowId,' + @fields + ' from ' + @tableName + ' where ' + @sqlWhere    
        
    --处理页数超出范围情况
    if @pageIndex<=0 
        Set @pageIndex = 1
    
    if @pageIndex>@totalPage
        Set @pageIndex = @totalPage

     --处理开始点和结束点
    Declare @startRecord int
    Declare @endRecord int
    
    set @startRecord = (@pageIndex-1)*@PageSize + 1
    set @endRecord = @startRecord + @pageSize - 1

    --继续合成sql语句
    set @sql = @sql + ') as ' + @tableName + ' where rowId between ' + Convert(varchar(50),@startRecord) + ' and ' +  Convert(varchar(50),@endRecord)
    --print @sql
    Exec(@sql)

--    print @totalRecord 
    If @@Error <> 0
      Begin
        RollBack Tran
        Return -1
      End
     Else
      Begin
        Commit Tran
        --print @totalRecord 
        Return @totalRecord ---返回记录总数
      End   
end