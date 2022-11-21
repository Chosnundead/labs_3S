import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.forceFetch(
    "1, 2 и 3",
    """
    Create table TR_Tov
    (
        ID int identity,
        ST varchar(20) check (ST in ('INS', 'DEL', 'UPD')),
        TRN varchar(50),
        C varchar(300)
    )
    GO
    CREATE TRIGGER TRIG_Tov_Ins
        on Товары after INSERT
    as declare @a1 varchar(20), @a2 real, @a3 int, @in varchar(300);
    print'Операция вставки'
    set @a1 = (select [Наименование] form INSERTED);
    set @a2 = (select [Цена] from INSERTED);
    set @a3 = (select [Количество] from INSERTED);
    set @in = @a1 + '' + cast(@a2 as varchar(20)) + '' + cast(@a3 as
    varchar(20));
    insert into TR_Tov(ST, TRN, C) values('INS', 'TRIG_Tov_Ins', @in);
    return;
    GO
    insert into Товары(Наименование, Цена, Количество)
     values('Планшетv2'
    , 140, 20);
    GO
    select * from TR_Tov
    """,
)
db.forceFetch(
    4,
    """
    create trigger TRIG_Tov on Товары after INSERT, DELETE, UPDATE
    as declare @a1 varchar(20), @a2 real, @a3 int, @in varchar(300);
    declare @ins int = (select count(*) from inserted),
     @del int = (select count(*) from deleted);
    if @ins > 0 and @del = 0
    begin
     print 'Событие: INSERT';
     set @a1 = (select [Наименование] from INSERTED);
     set @a2 = (select [Цена] from INSERTED);
     set @a3 = (select [Количество] from INSERTED);
     set @in = @a1+' '+cast(@a2 as varchar(20))+' '+cast(@a3 as varchar(20));
     insert into TR_Tov(ST, TRN, C) values('INS', 'TRIG_Tov', @in);
    end;
    else
    if @ins = 0 and @del > 0
    begin
    print 'Событие: DELETE';
     set @a1 = (select [Наименование] from deleted);
     set @a2 = (select [Цена] from deleted);
     set @a3 = (select [Количество] from deleted);
     set @in = @a1+' '+cast(@a2 as varchar(20))+' '+cast(@a3 as varchar(20));
     insert into TR_Tov(ST, TRN, C) values('DEL', 'TRIG_Tov', @in);
    end;
    else
    if @ins > 0 and @del > 0
    begin
     print 'Событие: UPDATE';
     set @a1 = (select [Наименование] from inserted);
     set @a2 = (select [Цена] from inserted);
     set @a3 = (select [Количество] from inserted);
     set @in = @a1+' '+cast(@a2 as varchar(20))+' '+cast(@a3 as varchar(20));
     set @a1 = (select [Наименование] from deleted);
     set @a2 = (select [Цена] from deleted);
     set @a3 = (select [Количество] from deleted);
     set @in = @a1+' '+cast(@a2 as varchar(20))+' '+cast(@a3 as
    varchar(20))+' '+@in;
     insert into TR_Tov(ST, TRN, C) values('UPD', 'TRIG_Tov', @in);
    end;
    return; 
    GO
    insert into Товары(Наименование, Цена, Количество)
    values('Стол'
    , 140, 20);
     delete from Товары where Наименование = 'Стол';
     update Товары set Количество = 20 where Наименование = 'Стул'; 
    """,
)
db.forceFetch(
    5,
    """
    alter table Товары add constraint Цена check(Цена >= 15)
    go
    update Товары set Цена = 10 where Наименование = 'Стул';
    """,
)
db.forceFetch(
    6,
    """
    create trigger AUD_AFTER_UPDA on Товары after UPDATE
     as print 'AUD_AFTER_UPDATE_A';
    return;
    go
    create trigger AUD_AFTER_UPDB on Товары after UPDATE
     as print 'AUD_AFTER_UPDATE_B';
     return;
    go
    create trigger AUD_AFTER_UPDC on Товары after UPDATE
     as print 'AUD_AFTER_UPDATE_C';
    return;
    go 
    select t.name, e.type_desc
     from sys.triggers t join sys.trigger_events e
     on t.object_id = e.object_id
     where OBJECT_NAME(t.parent_id) = 'Товары' and
     e.type_desc = 'UPDATE' ;
     exec SP_SETTRIGGERORDER @triggername = 'AUD_AFTER_UPDC',
     @order = 'First', @stmttype = 'UPDATE';
    exec SP_SETTRIGGERORDER @triggername = 'AUD_AFTER_UPDA',
     @order = 'Last', @stmttype = 'UPDATE';
    """,
)
db.forceFetch(
    7,
    """
    create trigger Tov_Tran
        on Товары after INSERT, DELETE, UPDATE
            as declare @c int = (select sum(Количество) from Товары);
    if(@c > 2000)
    begin
        raiserror('Общая количество товаров не может быть >2000', 10, 1);
        rollback;
    end;
    return;
    GO
    update Товары set Количество = 1990
     where Наименование = 'Стол'
    GO
    """,
)
db.forceFetch(
    8,
    """
    CREATE TRIGGER Tov_INSTEAD_OF
        on Товары instead of DELETE
            as raiserror(N'Удаление запрещено', 10, 1)
    return
    GO
    delete from Товары where Наименование = 'Стол'; 
    """,
)
db.forceFetch(
    9,
    """
    create trigger DDL_PRODAJI on database
     for DDL_DATABASE_LEVEL_EVENTS as
     declare @t varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/EventType)[1]', 'varchar(50)');
     declare @t1 varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]', 'varchar(50)');
     declare @t2 varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectType)[1]', 'varchar(50)');
     if @t1 = 'Товары'
    begin
     print 'Тип события: '+@t;
     print 'Имя объекта: '+@t1;
     print 'Тип объекта: '+@t2;
     raiserror( N'операции с таблицей Товары запрещены', 16, 1);
     rollback;
     end;
    GO
     alter table Товары Drop Column Количество;
    """,
)

del db
