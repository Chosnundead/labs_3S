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


def fetch(number, string=None, description=None):
    global cur
    print(f"Задание №{number}:")
    if string != None and isinstance(string, str):
        cur.execute(string)
        for item in cur.fetchall():
            print(item)
    if description != None and isinstance(description, str):
        print(f"{description}.")
    print()


fetch(
    1,
    """SELECT dbo.Товары.[Название товара], dbo.Заказы.[Дата продажи], dbo.Товары.[Цена в рублях]
      FROM dbo.Заказы, dbo.Товары, dbo.Клиенты
      WHERE dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]
      AND dbo.Клиенты.[Номер покупателя] = dbo.Заказы.[Номер покупателя]
      AND dbo.Клиенты.[Фамилия покупателя] IN(SELECT dbo.Клиенты.[Фамилия покупателя]
        FROM dbo.Клиенты
        WHERE(dbo.Клиенты.Адрес LIKE '%Минск'))""",
)
fetch(
    2,
    """SELECT dbo.Товары.[Название товара], dbo.Заказы.[Дата продажи], dbo.Товары.[Цена в рублях]
      FROM dbo.Заказы INNER JOIN dbo.Товары
      ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]
      WHERE dbo.Заказы.[Номер покупателя] IN(SELECT dbo.Клиенты.[Номер покупателя]
        FROM dbo.Клиенты
        WHERE(dbo.Клиенты.Адрес LIKE '%Минск'))""",
)
fetch(
    3,
    """SELECT dbo.Товары.[Название товара], dbo.Заказы.[Дата продажи], dbo.Товары.[Цена в рублях]
      FROM dbo.Заказы INNER JOIN dbo.Товары
      ON dbo.Товары.[Номер товара] = dbo.Заказы.[Номер товара]
      INNER JOIN dbo.Клиенты
      ON dbo.Заказы.[Номер покупателя] = dbo.Клиенты.[Номер покупателя]
        WHERE(dbo.Клиенты.Адрес LIKE '%Минск')""",
)
fetch(
    4,
    """SELECT TOP(1) dbo.Товары.[Название товара], dbo.Товары.[Цена в рублях]
      FROM dbo.Товары
      ORDER BY dbo.Товары.[Цена в рублях] DESC""",
)
fetch(
    5,
    """SELECT dbo.Товары.[Название товара]
      FROM dbo.Товары
      WHERE NOT EXISTS (SELECT *
        FROM dbo.Заказы
            WHERE dbo.Заказы.[Номер товара] = dbo.Товары.[Номер товара])""",
    "Такие элементы не были найдены",
)
fetch(
    6,
    """SELECT TOP(1)
      (SELECT AVG(dbo.Товары.[Цена в рублях])
      FROM dbo.Товары
      WHERE dbo.Товары.[Название товара] LIKE 'Нож'),
      (SELECT AVG(dbo.Товары.[Цена в рублях])
      FROM dbo.Товары
      WHERE dbo.Товары.[Название товара] LIKE 'Багет')
      FROM dbo.Товары
      """,
)
fetch(
    7,
    """SELECT dbo.Товары.[Название товара], dbo.Товары.[Цена в рублях]
      FROM dbo.Товары
      WHERE dbo.Товары.[Цена в рублях] >= ALL (SELECT dbo.Товары.[Цена в рублях]
        FROM dbo.Товары
        WHERE dbo.Товары.[Название товара] LIKE 'Тапки')""",
)
fetch(
    8,
    """SELECT dbo.Товары.[Название товара], dbo.Товары.[Цена в рублях]
      FROM dbo.Товары
      WHERE dbo.Товары.[Цена в рублях] > ANY (SELECT dbo.Товары.[Цена в рублях]
        FROM dbo.Товары
        WHERE dbo.Товары.[Название товара] LIKE 'Тапки')""",
)
fetch(9, description="Выполнено")


cur.close()
