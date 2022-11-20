import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.forceFetch(
    1,
    """
    DECLARE @tv char(20), @t char(300) = '';
    DECLARE zkTovar CURSOR
        for SELECT Наименование_товара from Заказы;
    OPEN ZkTovar;
    FETCH ZkTovar into @tv;
    print 'Заказанные товары';
    while @@fetch_status = 0
    begin
    set @t=rtrim(@tv) + ',' + @t;
    FETCH ZkTovar into @tv;
    end;
    print @t;
    CLOSE ZkTovar;
    """,
)
db.forceFetch(
    2,
    """
    DECLARE Tovary CURSOR LOCAL
     for SELECT Наименование, Цена from Товары;
    DECLARE @tv char(20), @cena real;
    OPEN Tovary;
    fetch Tovary into @tv, @cena;
     print '1. '+@tv+cast(@cena as varchar(6));
     go
    DECLARE @tv char(20), @cena real;
    fetch Tovary into @tv, @cena;
     print '2. '+@tv+cast(@cena as varchar(6));
     go 
    """,
)
db.forceFetch(
    2,
    """
    DECLARE Tovary CURSOR LOCAL
     for SELECT Наименование, Цена from Товары;
    DECLARE @tv char(20), @cena real;
    OPEN Tovary;
    fetch Tovary into @tv, @cena;
     print '1. '+@tv+cast(@cena as varchar(6));
     go
    DECLARE @tv char(20), @cena real;
    fetch Tovary into @tv, @cena;
     print '2. '+@tv+cast(@cena as varchar(6));
     go 
    """,
)
db.forceFetch(
    2,
    """
    DECLARE Tovary CURSOR GLOBAL
     for SELECT Наименование, Цена from Товары;
    DECLARE @tv char(20), @cena real;
    OPEN Tovary;
    fetch Tovary into @tv, @cena;
     print '1. '+@tv+cast(@cena as varchar(6));
     go
    DECLARE @tv char(20), @cena real;
    fetch Tovary into @tv, @cena;
     print '2. '+@tv+cast(@cena as varchar(6));
     close Tovary;
     deallocate Tovary;
     go 
    """,
)
db.forceFetch(
    3,
    """
    DECLARE @tid char(10), @tnm char(40), @tgn char(1);
    DECLARE Zakaz CURSOR LOCAL STATIC
    for SELECT Наименование_товара, Цена_продажи, Количество
     FROM dbo.Заказы where Заказчик = 'Луч';
    open Zakaz;
    print 'Количество строк : '+cast(@@CURSOR_ROWS as varchar(5));
     UPDATE Заказы set Количество = 5 where Наименование_товара = 'Стул';
    DELETE Заказы where Наименование_товара = 'Шкаф';
    INSERT Заказы (Номер_заказа, Наименование_товара, Цена_продажи,
     Количество, Дата_поставки, Заказчик)
     values (18,
    'Шкаф'
    , 340, 1, '2014-08-02',
    'Луч');
    FETCH Zakaz into @tid, @tnm, @tgn;
    while @@fetch_status = 0
     begin
     print @tid + ' '+ @tnm + ' '+ @tgn;
     fetch Zakaz into @tid, @tnm, @tgn;
     end;
     CLOSE Zakaz;
    """,
)
db.forceFetch(
    4,
    """
    DECLARE @tc int, @rn char(50);
     DECLARE Primer1 cursor local dynamic SCROLL
     for SELECT row_number() over (order by Наименование_товара) N,
     Наименование_товара FROM dbo.Заказы
     where Заказчик = 'Луч'
    OPEN Primer1;
    FETCH Primer1 into @tc, @rn;
    print 'следующая строка : ' + cast(@tc as varchar(3))+ rtrim(@rn);
    FETCH LAST from Primer1 into @tc, @rn;
    print 'последняя строка : ' + cast(@tc as varchar(3))+ rtrim(@rn);
     CLOSE Primer1;
    """,
)
db.forceFetch(
    "5 и 6",
    """
    DECLARE @tn char(20), @tc real, @tk int;
     DECLARE Primer2 cursor local dynamic
     for SELECT Наименование_товара, Цена_продажи, Количество FROM dbo.Заказы FOR UPDATE;
    OPEN Primer2;
    FETCH Primer2 into @tn, @tc, @tk;
    DELETE Заказы where CURRENT OF Primer2;
    FETCH Primer2 into @tn, @tc, @tk;
    UPDATE Заказы set Количество = Количество + 1
        Where CURRENT OF Primer2;
     CLOSE Primer2;
    """,
)

del db
