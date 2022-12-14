import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.forceFetch(
    1,
    """
    insert into Заказчики (Наименование_фирмы, Адрес, Расчетный_счет) values ('Не_заказывал2', 'Минск, ул. Короля, 3', '123456')
    go
    select zk.Наименование_фирмы[Те кто не покупает]
    from Заказчики zk
    where zk.Наименование_фирмы not in(select zkt.Наименование_фирмы 
        from Заказчики zkt join Заказы zt
        on zkt.Наименование_фирмы = zt.Заказчик)
    """,
)

del db
