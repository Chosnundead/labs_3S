import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.forceFetch(
    1,
    """
    exec SP_HELPINDEX 'Заказы'
    """,
)
db.forceFetch(
    1,
    """
    CREATE table #EXPLRE
    (TIND int,
    TFIELD varchar(100)
    );
    GO
    SET nocount on;
    DECLARE @i int = 0;
    WHILE @i<1000
        begin
    INSERT #EXPLRE(TIND, TFIELD)
        values(floor(20000*rand()), replicate('строка', 10));
    IF(@i % 100 = 0)
        print @i;
    SET @i = @i + 1;
    end;
    GO
    SELECT * FROM #EXPLRE where TIND between 1500 and 2500 order by TIND
    GO
    checkpoint;
    DBCC DROPCLEANBUFFERS;
    GO
    CREATE clustered index #EXPLRE_CL on #EXPLRE(TIND asc)
    GO
    """,
)
db.forceFetch(
    2,
    """
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
     SELECT count(*)[количество строк] from #EX;
     SELECT * from #EX
     GO
      CREATE index #EX_NONCLU on #EX(TKEY, CC)
      GO
    SELECT * from #EX where TKEY > 1500 and CC < 4500;
     SELECT * from #EX order by TKEY, CC
     GO
     SELECT * from #EX where TKEY = 556 and CC > 3
    GO
    """,
)
db.forceFetch(
    3,
    """
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
    CREATE index #EX_TKEY_X on #EX(TKEY) INCLUDE (CC)
    GO
    SELECT CC from #EX where TKEY>15000
    GO
    """,
)
db.forceFetch(
    4,
    """
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
    SELECT TKEY from #EX where TKEY between 5000 and 19999;
    GO
    SELECT TKEY from #EX where TKEY>15000 and TKEY < 20000
    GO
    SELECT TKEY from #EX where TKEY=17000
    GO
    CREATE index #EX_WHERE on #EX(TKEY) where (TKEY>=15000 and TKEY < 20000);
    GO
    """,
)
db.forceFetch(
    5,
    """
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
    INSERT top(10000) #EX(TKEY, TF) select TKEY, TF from #EX;
    GO
    CREATE index #EX_TKEY ON #EX(TKEY);
    GO
    SELECT name [Индекс], avg_fragmentation_in_percent [Фрагментация (%)]
     FROM sys.dm_db_index_physical_stats(DB_ID(N'TEMPDB'),
     OBJECT_ID(N'#EX'), NULL, NULL, NULL) ss
     JOIN sys.indexes ii on ss.object_id = ii.object_id and ss.index_id = ii.index_id
     WHERE name is not null;
    GO
    ALTER index #EX_TKEY on #EX reorganize;
    GO
    ALTER index #EX_TKEY on #EX rebuild with (online = off);
    GO
    """,
)
db.forceFetch(
    6,
    """
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
    """,
)

del db
