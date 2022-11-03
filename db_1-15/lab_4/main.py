import pyodbc

connectString = (
    "Driver={SQL Server};"
    "Server=DESKTOP-HQ83LCC;"
    "Database=master;"
    "Trusted_Connection=yes;"
)
connect = pyodbc.connect(connectString)
cur = connect.cursor()

try:
    cur.execute("USE master")
    cur.commit()
    cur.execute("USE S_MyBASE")
    cur.commit()
except:
    print("База данных [S_MyBASE] не существует.")

print("\nЗадание №1:")
cur.execute(
    """SELECT dbo.Товары.[Название товара], dbo.Заказы.[Кол-во заказаного товара]
    FROM dbo.Товары INNER JOIN dbo.Заказы
    ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №2:")
cur.execute(
    """SELECT dbo.Товары.[Название товара], dbo.Заказы.[Кол-во заказаного товара]
    FROM dbo.Товары INNER JOIN dbo.Заказы
    ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара] AND dbo.Товары.[Название товара] LIKE '%аге%'"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №3:")
cur.execute(
    """SELECT dbo.Товары.[Название товара], dbo.Заказы.[Кол-во заказаного товара]
    FROM dbo.Товары, dbo.Заказы
    WHERE dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №4:")
cur.execute(
    """SELECT dbo.Товары.[Название товара],
    CASE
    WHEN (dbo.Заказы.[Кол-во заказаного товара] BETWEEN 1 AND 2) THEN 'Несколько'
    WHEN (dbo.Заказы.[Кол-во заказаного товара] BETWEEN 2 AND 5) THEN 'Единица хранения'
    ELSE 'Много'
    END [Кол-во товара]
    FROM dbo.Товары INNER JOIN dbo.Заказы
    ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]
    ORDER BY dbo.Товары.[Номер товара]"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №5:")
cur.execute(
    """SELECT dbo.Товары.[Название товара],
    CASE
    WHEN (dbo.Заказы.[Кол-во заказаного товара] BETWEEN 1 AND 2) THEN 'Несколько'
    WHEN (dbo.Заказы.[Кол-во заказаного товара] BETWEEN 2 AND 5) THEN 'Единица хранения'
    ELSE 'Много'
    END [Кол-во товара]
    FROM dbo.Товары INNER JOIN dbo.Заказы
    ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]
    ORDER BY (CASE
        WHEN(dbo.Заказы.[Кол-во заказаного товара] BETWEEN 1 AND 2) THEN 3
        WHEN(dbo.Заказы.[Кол-во заказаного товара] BETWEEN 2 AND 5) THEN 2
        ELSE 1
        END
    )"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №6:")
cur.execute(
    """SELECT ISNULL(dbo.Товары.[Название товара], '***'), dbo.Заказы.[Кол-во заказаного товара]
    FROM dbo.Товары LEFT OUTER JOIN dbo.Заказы
    ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №7:")
cur.execute(
    """SELECT ISNULL(dbo.Товары.[Название товара], '***') AS 'Наименование товара', dbo.Заказы.[Кол-во заказаного товара]
    FROM dbo.Товары RIGHT OUTER JOIN dbo.Заказы
    ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №8:")
print("\n1 Запрос:")
cur.execute(
    """SELECT * FROM dbo.Товары at FULL OUTER JOIN dbo.Заказы aa
    ON aa.[Номер товара] = at.[Номер товара]
    ORDER BY aa.[Номер товара], at.[Номер товара]"""
)
for item in cur.fetchall():
    print(item)
print("\n2 Запрос:")
cur.execute(
    """SELECT COUNT(*) FROM dbo.Товары at FULL OUTER JOIN dbo.Заказы aa
    ON aa.[Номер товара] = at.[Номер товара]
    WHERE aa.[Дата продажи] IS NULL"""
)
for item in cur.fetchall():
    print(item)
print("\n3 Запрос:")
cur.execute(
    """SELECT * FROM dbo.Товары at FULL OUTER JOIN dbo.Заказы aa
    ON aa.[Номер товара] = at.[Номер товара]
    WHERE aa.[Дата продажи] IS NOT NULL"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №9:")
cur.execute(
    """SELECT dbo.Товары.[Название товара], dbo.Товары.[Цена в рублях], dbo.Заказы.Скидка
    FROM dbo.Заказы CROSS JOIN dbo.Товары
    WHERE dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]"""
)
for item in cur.fetchall():
    print(item)

print("\nЗадание №10:\nВыполнено.")

cur.close()
