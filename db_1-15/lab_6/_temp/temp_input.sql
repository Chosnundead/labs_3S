
    begin tran
        insert Заказчики values('Луч', 'Минск', 10234);
        begin tran
            update Заказы set Наименование_товара = 'Луч' where Заказчик = 'Луч';
            commit;
            if @@trancount > 0 rollback;
        select
            (select count(*) from Заказы where Заказчик = 'Луч') 'Заказы',
            (select count(*) from Заказчики where Наименование_фирмы = 'Луч') 'Заказчики';
    