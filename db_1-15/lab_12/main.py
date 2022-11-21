import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.forceFetch(
    1,
    """
    CREATE PROCEDURE PrZakazy
    as
    begin
        declare @k int = (select count(*) from Заказы);
        select * from Заказы;
        return @k;
    end;
    GO
    declare @z int = 0;
    EXEC @z = PrZakazy;
    print 'кол-во товаров = ' + cast(@z as varchar(3));
    """,
)
db.forceFetch(
    2,
    """
    alter procedure PrZakazy @p varchar(20), @c int output
    as begin
    declare @z int = (select count(*) from Заказы);
    print 'параметры: @p = ' + @p + ', @c = ' + cast(@c as varchar(3));
    select * from Заказы where Заказчик = @p;
    set @c = @@rowcount;
    return @z;
    end;
    GO
    declare @l int = 0, @r int = 0, @o varchar(20);
    exec @l = PrZakazy @p = 'Луч', @c = @r output;
    print('Кол-во товаров всего = ' + cast(@l as varchar(3)));
    print('Кол-во товаров, заказанных фирмой = ' + cast(@o as varchar(3)) + '=' + cast(@r as varchar(3)));
    """,
)
db.forceFetch(
    3,
    """
    alter procedure PrZakazy @p varchar(20)
    as begin
    declare @k int = (select count(*) from Заказы);
    select * from Заказы where Заказчик = @p;
    end;
    GO
    CREATE table #Zk
    ( Номер_заказа int primary key,
    Наименование_товара nvarchar(50),
    Цена_продажи real,
    Количество int,
    Дата_поставки date,
    Заказчик nvarchar(50))
    GO
    INSERT #Zk exec PrZakazy @p = 'Zte';
    INSERT #Zk exec PrZakazy @p = 'Белвест';
    GO
    select * from #Zk
    """,
)
db.forceFetch(
    4,
    """
    CREATE PROCEDURE TovaryInsert
        @t NVARCHAR(50), @cn REAL, @kl INT = null
    as declare @rc int = 1;
    begin try
        insert into Товары(Наименование, Цена, Количество)
            values (@t, @cn, @kl)
        return @rc;
    end try
    begin catch
        print 'номер ошибки : ' + cast(error_number() as varchar(6));
        print 'сообщение : ' + error_message();
        print 'уровень : ' + cast(error_severity() as varchar(6));
        print 'метка : ' + cast(error_state() as varchar(8));
        print 'номер строки : ' + cast(error_line() as varchar(8));
        if  error_procedure() is not null
        print 'имя процедуры : ' + error_procedure();
        return -1;
    end catch;
    GO
    declare @rc int;
    exec @rc = TovaryInsert @t = 'Планшет', @cn = 160, @kl = 90;
    print 'код ошибки : ' + cast(@rc as varchar(3));
    """,
)
db.forceFetch(
    5,
    """
    create procedure Zkz_REPORT @p CHAR(50)
     as
     declare @rc int = 0;
    begin try
     declare @tv char(20), @t char(300) = ' ';
     declare ZkTov CURSOR for
     select Наименование_товара from Заказы where Заказчик = @p;
     if not exists (select Наименование_товара
     from Заказы where Заказчик = @p)
     raiserror('ошибка'
    , 11, 1);
     else
     open ZkTov;
    fetch ZkTov into @tv;
    print 'Заказанные товары';
    while @@fetch_status = 0 
    begin
     set @t = rtrim(@tv) + ', ' + @t;
     set @rc = @rc + 1;
     fetch ZkTov into @tv;
    end;
    print @t;
    close ZkTov;
     return @rc;
     end try
     begin catch
     print 'ошибка в параметрах'
     if error_procedure() is not null
    print 'имя процедуры : ' + error_procedure();
     return @rc;
     end catch;
    GO
    declare @rc int;
    exec @rc = Zkz_REPORT @p = 'Луч';
    print 'количество товаров = ' + cast(@rc as varchar(3));
    """,
)
db.forceFetch(
    6,
    """
    create procedure TovaryInsert_X
     @a int, @b NVARCHAR(50), @c REAL, @d INT = null,
     @e date, @f NVARCHAR(50)
    as declare @rc int=1;
    begin try
     set transaction isolation level SERIALIZABLE;
     begin tran
     insert into Заказы (Номер_заказа, Наименование_товара,
     Цена_продажи, Количество, Дата_поставки, Заказчик)
     values (@a, @b, @c, @d, @e, @f)
     exec @rc=TovaryInsert @b, @c, @d;
     commit tran;
     return @rc;
    end try
    begin catch
     print 'номер ошибки : ' + cast(error_number() as varchar(6));
     print 'сообщение : ' + error_message();
     print 'уровень : ' + cast(error_severity() as varchar(6));
     print 'метка : ' + cast(error_state() as varchar(8));
     print 'номер строки : ' + cast(error_line() as varchar(8));
     if error_procedure() is not null 
     print 'имя процедуры : ' + error_procedure();
     if @@trancount > 0 rollback tran ;
     return -1;
    end catch;
    GO
    declare @rc int;
    exec @rc = TovaryInsert_X @a = 20, @b = 'Стол'
    , @c = 78,
    @d = 10, @e = '01.12.2014', @f = 'Луч';
    print 'код ошибки=' + cast(@rc as varchar(3)); 
    """,
)

del db
