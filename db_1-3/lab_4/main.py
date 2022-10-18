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
    WHEN (dbo.Заказы.[Кол-во заказаного товара] between 1 and 2) then 'Несколько'
    WHEN (dbo.Заказы.[Кол-во заказаного товара] between 2 and 5) then 'Единица хранения'
    ELSE 'Много'
    END [Кол-во товара]
    FROM dbo.Товары INNER JOIN dbo.Заказы
    ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]
    ORDER BY dbo.Товары.[Номер товара]"""
)
for item in cur.fetchall():
    print(item)

cur.close()
