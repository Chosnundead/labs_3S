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
    """
    SELECT min([Цена в рублях]) [Минимальная цена],
        max([Цена в рублях]) [Максимальная цена],
        count(*) [Количество товаров],
        avg([Цена в рублях]) [Средняя цена]
    FROM Товары
    """,
)

cur.close()
