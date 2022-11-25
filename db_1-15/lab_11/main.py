import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.forceFetch(
    1,
    """
    set nocount on
    if exists (select * from SYS.OBJECTS -- таблица X есть?
     where OBJECT_ID= object_id(N'DBO.X') )
    drop table X;
    declare @c int, @flag char = 'c'; -- commit или rollback?
    SET IMPLICIT_TRANSACTIONS ON -- включ. режим неявной транзакции
    CREATE table X(K int ); -- начало транзакции
    INSERT X values (1),(2),(3);
    set @c = (select count(*) from X);
    print 'количество строк в таблице X: ' + cast( @c as varchar(2));
    if @flag = 'c' commit; -- завершение транзакции: фиксация
     else rollback; -- завершение транзакции: откат
     SET IMPLICIT_TRANSACTIONS OFF -- выключ. режим неявной транзакции
    if exists (select * from SYS.OBJECTS -- таблица X есть?
     where OBJECT_ID= object_id(N'DBO.X') )
    print 'таблица X есть';
     else print 'таблицы X нет'
    """,
)
db.forceFetch(
    2,
    """
    begin try
        begin tran
            delete Товары where Наименование='Стол';
            insert Товары values('Стулчик', 15, 20);
            insert Товары values('Крутой телевизор', 500, 5);
            commit tran;
        end try
        begin catch
            print 'ошибка:' + case
            when error_number() = 2627 and patindex('%PK_Товары%', error_message()) > 0
            then 'дублирование товара'
            else 'неизвестная ошибка:' + cast(error_number() as varchar(5)) + error_message()
        end;
        if @@trancount > 0 rollback tran;
    end catch;
    """,
)
db.forceFetch(
    3,
    """
    declare @point varchar(32);
    begin try
        begin tran
            delete Товары where Наименование = 'Стол';
            set @point = 'p1'; save tran @point;
            insert Товары values('Стул', 15, 20);
            set @point = 'p2'; save tran @point;
            insert Товары values ('Телевизор', 500, 5);
            commit tran;
        end try
        begin catch
            print'ошибка:' + case when error_number() = 2627
                and patindex('%PK_Товары%', error_message()) > 0
                then 'дублирование товара'
                else 'неизвестная ошибка:' + cast(error_number() as varchar(5)) + error_message()
                end;
            if @@trancount > 0
                begin
                print'контрольная точка:' + @point;
                rollback tran @point;
                commit tran;
                end;
    end catch;
    """,
)
db.forceFetch(
    4,
    """
    set transaction isolation level READ UNCOMMITTED
    begin transaction
    -------------------------- t1 ------------------
    select @@SPID, 'insert Товары' 'результат'
    ,
    * from Товары
     where Наименование = 'Блокнот';
    select @@SPID, 'update Заказы' 'результат'
    , Наименование_товара,
     Цена_продажи from Заказы where Наименование_товара = 'Блокнот';
    commit;
    -------------------------- t2 -----------------
    begin transaction
    select @@SPID
    insert Товары values ('Блокнот'
    , 2, 80);
    update Заказы set Наименование_товара = 'Блокнот'
     where Наименование_товара = 'Стол'
    -------------------------- t1 --------------------
    -------------------------- t2 --------------------
    rollback;
    """,
)
db.forceFetch(
    5,
    """
        -- A ---
     set transaction isolation level READ COMMITTED
    begin transaction
    select count(*) from Заказы where Наименование_товара = 'Стул';
    
    
    select 'update Заказы' 'результат', count(*)
     from Заказы where Наименование_товара = 'Стул';
    commit;
    --- B ---
    begin transaction
    
     update Заказы set Наименование_товара = 'Стул'
     where Наименование_товара = 'Стол'
     commit;
    
    """,
)
db.forceFetch(
    6,
    """
    set transaction isolation level REPEATABLE READ
    begin transaction
    select Заказчик from Заказы where Наименование_товара = 'Стул';
    
    select case
     when Заказчик = 'Луч' then 'insert Заказы' else ' '
    end 'результат'
    , Заказчик from Заказы where Наименование_товара = 'Стул';
    commit;
    --- B ---
    begin transaction
    
     insert Заказы values (12,
    'Стул'
    , 78, 10, '01.12.2014',
    'Луч');
     commit;
    
    """,
)
db.forceFetch(
    7,
    """
    set transaction isolation level SERIALIZABLE
    begin transaction
    delete Заказы where Заказчик = 'Луч';
     insert Заказы values (14,
    'Стул'
    , 78, 10, '01.12.2014',
    'Луч');
     update Заказы set Заказчик = 'Луч' where Наименование_товара = 'Стул';
     select Заказчик from Заказы where Наименование_товара = 'Стул';
    -------------------------- t1 -----------------
    select Заказчик from Заказы where Наименование_товара = 'Стул';
    -------------------------- t2 ------------------
    commit;
    --- B ---
    begin transaction
    delete Заказы where Заказчик = 'Луч';
     insert Заказы values (14,
    'Стул'
    , 78, 10, '01.12.2014',
    'Луч');
     update Заказы set Заказчик = 'Луч' where Наименование_товара = 'Стул';
     select Заказчик from Заказы where Наименование_товара = 'Стул';
     -------------------------- t1 --------------------
     commit;
     select Заказчик from Заказы where Наименование_товара = 'Стул';
    """,
)
db.forceFetch(
    8,
    """
    begin tran
        insert Заказчики values('Луч', 'Минск', 10234);
        begin tran
            update Заказы set Наименование_товара = 'Луч' where Заказчик = 'Луч';
            commit;
            if @@trancount > 0 rollback;
        select
            (select count(*) from Заказы where Заказчик = 'Луч') 'Заказы',
            (select count(*) from Заказчики where Наименование_фирмы = 'Луч') 'Заказчики';
    """,
)

del db
