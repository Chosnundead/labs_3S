
    create function COUNT_Zakazy(@f varchar(20)) returns int
    as begin declare @rc int = 0;
    set @rc = (select count(Номер_заказа)
        from Заказы z join Заказчики zk
            on z.Заказчик = zk. Наименование_фирмы
                where Наименование_фирмы = @f);
    return @rc
    end;
    GO
    DECLARE @f int = dbo.COUNT_Zakazy('Луч');
    print 'количество заказов == ' + cast(@f as varchar(4));
    GO
    Select Наименование_фирмы, dbo.COUNT_Zakazy(Наименование_фирмы)
        from Заказчики;
    