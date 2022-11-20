import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.fetch(
    1,
    """
    SELECT Наименование_товара [Товар],
        Цена_продажи [Цена продажи],
        Дата_поставки [Дата] from Заказы;
    """,
)
db.goFetch(
    """
    CREATE VIEW [Заказанные товары]
        as select Наименование_товара [Товар],
        Цена_продажи [Цена продажи],
        Дата_поставки [Дата] from Заказы;
    """,
)
db.fetch(
    1,
    """
    SELECT Наименование_товара [Товар],
        Цена_продажи [Цена продажи],
        Дата_поставки [Дата] from Заказы;
    """,
)
db.fetch(
    1,
    """
    SELECT * from [Заказанные товары]
    """,
)
db.fetch(
    1,
    """
    SELECT * from [Заказанные товары] order by [Дата]
    """,
)
db.goFetch(
    """
    ALTER VIEW [Заказанные товары]
        as select Наименование_товара [Товар],
            Цена_продажи [Цена продажи],
            Дата_поставки [Дата],
            Количество [Количество] FROM Заказы;
    """
)
db.goFetch(
    """
    DROP VIEW [Заказанные товары];
    """
)
db.goFetch(
    """
    CREATE VIEW [Сравнение цен]
        as SELECT zk.Наименование_товара [Товар],
            tv.Цена [Исходная цена],
            zk.Цена_продажи [Цена продажи]
                FROM Заказы zk join Товары tv
                ON zk.Наименование_товара = tv.Наименование;
    """
)
db.goFetch(
    """
    CREATE VIEW Дорогие_товары(Товар, Цена, Количество)
        as select Наименование, Цена, Количество from Товары
            where Цена>200;
    """
)
db.fetch(
    3,
    """
    SELECT * from Дорогие_товары     
    """,
)
db.goFetch(
    """
    INSERT Дорогие_товары values('Диванv2', 300, 3)
    INSERT Дорогие_товары values('Шкафv2', 150, 7)
    """
)
db.goFetch(
    """
    ALTER VIEW Дорогие_товары(Товар, Цена, Количество)
        as select Наименование, Цена, Количество from Товары
            where Цена>200 WITH CHECK OPTION;
    """
)
db.goFetch(
    """
    INSERT Дорогие_товары values('Столv2', 80, 9)
    """
)
db.goFetch(
    """
    CREATE VIEW Дорогие_товары(Товар, Цена, Количество)
        as select TOP 150 Наименование, Цена, Количество FROM Товары
            ORDER BY Наименование;
    """
)
db.goFetch(
    """
    ALTER VIEW [Сравнение цен] WITH SCHEMABINDING
    as SELECT zk.Наименование_товара [Товар],
        tv.Цена [Исходная цена],
        zk.Цена_продажи [Цена продажи]
        FROM dbo.Заказы zk join dbo.Товары tv
            ON zk.Наименование_товара = tv.Наименование;
    """
)

del db
