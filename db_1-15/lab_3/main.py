import pyodbc
import pathlib
import os

PATH_TO_FOLDER = pathlib.Path(__file__).parents[0]
connectString = (
    "Driver={SQL Server};"
    "Server=DESKTOP-HQ83LCC;"
    "Database=master;"
    "Trusted_Connection=yes;"
)
connect = pyodbc.connect(connectString)

cur = connect.cursor()
try:
    cur.execute("DROP TABLE Скидки")
    cur.commit()
except:
    print("Таблица [Скидки] не существует.")
try:
    cur.execute("DROP TABLE Заказы")
    cur.commit()
except:
    print("Таблица [Заказы] не существует.")
try:
    cur.execute("DROP TABLE Товары")
    cur.commit()
except:
    print("Таблица [Товары] не существует.")
try:
    cur.execute("DROP TABLE Клиенты")
    cur.commit()
except:
    print("Таблица [Клиенты] не существует.")
try:
    cur.execute("USE master")
    cur.commit()
    cur.execute("DROP DATABASE S_MyBASE")
    cur.commit()
except:
    print("База данных [S_MyBASE] не существует.")

if input("exit?(y/n)\n") == "y":
    raise Exception("Программа завершена!")

# cur.execute(
#     '''
#             create table dbo.Employee
# (
# 	EmployeeId int primary key,
# 	EmployeeName nvarchar(128) not null,
# 	EmployeeAge int not null
# )

# -- Заполним таблицу Employee данными.
# insert into dbo.Employee(EmployeeId, EmployeeName, EmployeeAge) values (1, N'John Smith', 22)
# insert into dbo.Employee(EmployeeId, EmployeeName, EmployeeAge) values (2, N'Hilary White', 22)
# insert into dbo.Employee(EmployeeId, EmployeeName, EmployeeAge) values (3, N'Emily Brown', 22)

# create table dbo.Position
# (
# 	PositionId int primary key,
# 	PositionName nvarchar(64) not null
# )

# -- Заполним таблицу Position данными.
# insert into dbo.Position(PositionId, PositionName) values(1, N'IT-director')
# insert into dbo.Position(PositionId, PositionName) values(2, N'Programmer')
# insert into dbo.Position(PositionId, PositionName) values(3, N'Engineer')

# -- Заполним таблицу EmployeesPositions данными.
# create table dbo.EmployeesPositions
# (
# 	PositionId int foreign key references dbo.Position(PositionId),
# 	EmployeeId int foreign key references dbo.Employee(EmployeeId),
# 	primary key(PositionId, EmployeeId)
# )

# insert into dbo.EmployeesPositions(EmployeeId, PositionId) values (1, 1)
# insert into dbo.EmployeesPositions(EmployeeId, PositionId) values (1, 2)
# insert into dbo.EmployeesPositions(EmployeeId, PositionId) values (2, 3)
# insert into dbo.EmployeesPositions(EmployeeId, PositionId) values (3, 3)'''
# )
cur.execute("""USE master""")
cur.commit()
cur.execute(
    f"""CREATE DATABASE S_MyBASE
    ON PRIMARY
    (name = N'S_MyBASE_mdf', filename = N'{PATH_TO_FOLDER}\\S_MyBASE_mdf.mdf',
    size = 10240Kb, maxsize = UNLIMITED, filegrowth = 1024Kb),
    (name = N'S_MyBASE_ndf', filename = N'{PATH_TO_FOLDER}\\S_MyBASE_ndf.ndf',
    size = 10240Kb, maxsize = 1Gb, filegrowth = 25%),
    FILEGROUP fg1
    (name = N'S_MyBASE_fg1_1', filename = N'{PATH_TO_FOLDER}\\S_MyBASE_fg1-1.ndf',
    size = 10240Kb, maxsize = 1Gb, filegrowth = 25%),
    (name = N'S_MyBASE_fg1_2', filename = N'{PATH_TO_FOLDER}\\S_MyBASE_fg1-2.ndf',
    size = 10240Kb, maxsize = 1Gb, filegrowth = 25%)
    LOG ON
    (name = N'S_MyBASE_log', filename = N'{PATH_TO_FOLDER}\\S_MyBASE_log.ldf',
    size = 10240Kb, maxsize=2048Gb, filegrowth=10%)
    """
)
cur.commit()
cur.execute("""USE S_MyBASE""")
cur.commit()
cur.execute(
    """CREATE TABLE dbo.Клиенты (
        [Номер покупателя] INTEGER PRIMARY KEY,
        [Фамилия покупателя] VARCHAR(25) NOT NULL,
        [Имя покупателя] TEXT NOT NULL,
        [Отчество покупателя] TEXT NOT NULL,
        Адрес TEXT NOT NULL,
        Телефон TEXT NOT NULL,
        [E-mail] TEXT NOT NULL
    ) ON fg1
    INSERT INTO dbo.Клиенты([Номер покупателя], [Фамилия покупателя], [Имя покупателя], [Отчество покупателя], Адрес, Телефон, [E-mail]) VALUES (1, N'Солодкий', N'Денис', N'Викторович', N'г.Минск', N'+375298750375', 'Denis@gmail.com')
    INSERT INTO dbo.Клиенты([Номер покупателя], [Фамилия покупателя], [Имя покупателя], [Отчество покупателя], Адрес, Телефон, [E-mail]) VALUES (2, N'Кравченко', N'Виталий', N'Андреевич', N'г.Витебск', N'+375335709822', 'TheOne@gmail.com')
    INSERT INTO dbo.Клиенты([Номер покупателя], [Фамилия покупателя], [Имя покупателя], [Отчество покупателя], Адрес, Телефон, [E-mail]) VALUES (3, N'Дацик', N'Тимофей', N'Борисович', N'г.Брест', N'+375178488821', 'TheIronMan@gmail.com')
    INSERT INTO dbo.Клиенты([Номер покупателя], [Фамилия покупателя], [Имя покупателя], [Отчество покупателя], Адрес, Телефон, [E-mail]) VALUES (4, N'Кирпиченко', N'Тимофей', N'Альгертович', N'г.Минск', N'+375121218821', 'TheIron@gmail.com')
    INSERT INTO dbo.Клиенты([Номер покупателя], [Фамилия покупателя], [Имя покупателя], [Отчество покупателя], Адрес, Телефон, [E-mail]) VALUES (5, N'Усов', N'Владислав', N'Адольфович', N'г.Брест', N'+375178453821', 'TheBest@mail.ru')
    """
)
cur.commit()
cur.execute(
    """CREATE TABLE dbo.Товары (
        [Номер товара] INTEGER PRIMARY KEY,
        [Название товара] TEXT NOT NULL,
        [Кол-во товара на складе] INTEGER NOT NULL,
        [Цена в рублях] REAL NOT NULL
    ) ON fg1"""
)
cur.commit()
cur.execute("""ALTER TABLE dbo.Товары ADD [Единицы измерения] TEXT NOT NULL""")
cur.commit()
cur.execute(
    """
    INSERT INTO dbo.Товары([Номер товара], [Название товара], [Кол-во товара на складе], [Цена в рублях], [Единицы измерения]) VALUES (1, N'Багет', 200, 1.8, N'штук')
    INSERT INTO dbo.Товары([Номер товара], [Название товара], [Кол-во товара на складе], [Цена в рублях], [Единицы измерения]) VALUES (2, N'Нож', 9, 20.85, N'штук')
    INSERT INTO dbo.Товары([Номер товара], [Название товара], [Кол-во товара на складе], [Цена в рублях], [Единицы измерения]) VALUES (3, N'Тапки', 21, 10.5, N'штук')
    INSERT INTO dbo.Товары([Номер товара], [Название товара], [Кол-во товара на складе], [Цена в рублях], [Единицы измерения]) VALUES (4, N'Розетка', 666, 15, N'штук')
    INSERT INTO dbo.Товары([Номер товара], [Название товара], [Кол-во товара на складе], [Цена в рублях], [Единицы измерения]) VALUES (5, N'Пистолет', 2, 1000, N'штук')
    """
)
cur.commit()
cur.execute(
    """CREATE TABLE dbo.Заказы (
        [Дата продажи] DATE PRIMARY KEY,
        [Номер покупателя] INTEGER NOT NULL FOREIGN KEY REFERENCES dbo.Клиенты([Номер покупателя]),
        [Номер товара] INTEGER NOT NULL FOREIGN KEY REFERENCES dbo.Товары([Номер товара]),
        [Кол-во заказаного товара] INTEGER NOT NULL,
        Скидка REAL NOT NULL
    ) ON fg1
    INSERT INTO dbo.Заказы([Дата продажи], [Номер покупателя], [Номер товара], [Кол-во заказаного товара], Скидка) VALUES ('2020-12-08', 1, 1, 1, 0.02)
    INSERT INTO dbo.Заказы([Дата продажи], [Номер покупателя], [Номер товара], [Кол-во заказаного товара], Скидка) VALUES ('2021-09-21', 2, 2, 2, 0.2)
    INSERT INTO dbo.Заказы([Дата продажи], [Номер покупателя], [Номер товара], [Кол-во заказаного товара], Скидка) VALUES ('2022-05-11', 3, 3, 10, 0.1)
    INSERT INTO dbo.Заказы([Дата продажи], [Номер покупателя], [Номер товара], [Кол-во заказаного товара], Скидка) VALUES ('2022-06-21', 4, 4, 5, 0.3)
    INSERT INTO dbo.Заказы([Дата продажи], [Номер покупателя], [Номер товара], [Кол-во заказаного товара], Скидка) VALUES ('2022-09-19', 5, 5, 1, 0)
    """
)
cur.commit()
cur.execute(
    """CREATE TABLE dbo.Скидки (
        [Номер покупателя] INTEGER PRIMARY KEY REFERENCES dbo.Клиенты([Номер покупателя]),
        Скидка REAL NOT NULL
    ) ON fg1
    INSERT INTO dbo.Скидки([Номер покупателя], Скидка) VALUES (1, 0.02)
    INSERT INTO dbo.Скидки([Номер покупателя], Скидка) VALUES (2, 0.2)
    INSERT INTO dbo.Скидки([Номер покупателя], Скидка) VALUES (3, 0.1)
    INSERT INTO dbo.Скидки([Номер покупателя], Скидка) VALUES (4, 0.3)
    INSERT INTO dbo.Скидки([Номер покупателя], Скидка) VALUES (5, 0)
    """
)
cur.commit()


def printSaleTable():
    cur.execute("""SELECT * FROM dbo.Скидки""")
    print("\n\t\t===Скидки===\n[Номер покупателя]\tСкидка")
    counter = 0
    for item in cur.fetchall():
        print(f"{item[0]}\t\t\t{round(item[1] * 100)}%")
        counter += 1
    print(f"Итого: {counter} элемента(ов).")
    cur.execute(
        """SELECT * FROM dbo.Скидки
        WHERE Скидка >= 0.1"""
    )
    print(f"Из них {len(cur.fetchall())} скидка(ок) более 10%.")


printSaleTable()
cur.execute("""UPDATE dbo.Скидки SET Скидка = Скидка / 2""")
cur.commit()
printSaleTable()
cur.execute(
    """SELECT DISTINCT * FROM dbo.Заказы
    WHERE [Дата продажи]
    BETWEEN '2021-01-01' AND '2022-01-01'"""
)
print("\nЗадание №6:")
for item in cur.fetchall():
    print(item)
connect.close()


print("\nЗадание №8:")
os.system('sqlcmd -d S_MyBASE -Q "SELECT * FROM dbo.Скидки"')
