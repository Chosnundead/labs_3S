
    CREATE table #EX
    ( TKEY int,
     CC int identity(1, 1),
     TF varchar(100));
    GO
    set nocount on;
     declare @i int = 0;
     while @i < 20000 -- добавление в таблицу 20000 строк
     begin
     INSERT #EX(TKEY, TF) values(floor(30000*RAND()), replicate('строка '
    , 10));
     set @i = @i + 1;
     end;
    GO
     CREATE index #EX_TKEY on #EX(TKEY)
     with (fillfactor = 65);
     INSERT top(50)percent INTO #EX(TKEY, TF)
     SELECT TKEY, TF FROM #EX;
    GO
    SELECT name [Индекс], avg_fragmentation_in_percent [Фрагментация (%)]
     FROM sys.dm_db_index_physical_stats(DB_ID(N'TEMPDB'),
     OBJECT_ID(N'#EX'), NULL, NULL, NULL) ss JOIN sys.indexes ii
     ON ss.object_id = ii.object_id and ss.index_id = ii.index_id
     WHERE name is not null;
    GO
    