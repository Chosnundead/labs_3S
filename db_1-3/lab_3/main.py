import pyodbc

connectString = (
    "Driver={SQL Server};"
    "Server=DESKTOP-HQ83LCC;"
    "Database=S_MyBase;"
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
cur.execute(
    """CREATE TABLE dbo.Клиенты (
        [Номер покупателя] INTEGER PRIMARY KEY,
        [Фамилия покупателя] TEXT NOT NULL,
        [Имя покупателя] TEXT NOT NULL,
        [Отчество покупателя] TEXT NOT NULL,
        Адрес TEXT NOT NULL,
        Телефон TEXT NOT NULL,
        [E-mail] TEXT NOT NULL
    )
    INSERT INTO dbo.Клиенты([Номер покупателя], [Фамилия покупателя], [Имя покупателя], [Отчество покупателя], Адрес, Телефон, [E-mail]) VALUES (1, N'Солодкий', N'Денис', N'Викторович', N'г.Минск', N'+375298750375', N'Denis@gmail.com')
    INSERT INTO dbo.Клиенты([Номер покупателя], [Фамилия покупателя], [Имя покупателя], [Отчество покупателя], Адрес, Телефон, [E-mail]) VALUES (2, N'Кравченко', N'Виталий', N'Андреевич', N'г.Витебск', N'+375335709822', N'TheOne@gmail.com')
    INSERT INTO dbo.Клиенты([Номер покупателя], [Фамилия покупателя], [Имя покупателя], [Отчество покупателя], Адрес, Телефон, [E-mail]) VALUES (3, N'Дацик', N'Тимофей', N'Борисович', N'г.Брест', N'+375178488821', N'TheIronMan@gmail.com')
    """
)
cur.commit()
cur.execute(
    """CREATE TABLE dbo.Товары (
        [Номер товара] INTEGER PRIMARY KEY,
        [Название товара] TEXT NOT NULL,
        [Кол-во товара на складе] INTEGER NOT NULL,
        [Цена в рублях] REAL NOT NULL,
        [Единицы измерения] TEXT NOT NULL
    )
    INSERT INTO dbo.Товары([Номер товара], [Название товара], [Кол-во товара на складе], [Цена в рублях], [Единицы измерения]) VALUES (1, N'Багет', 200, 1.8, N'штук')
    INSERT INTO dbo.Товары([Номер товара], [Название товара], [Кол-во товара на складе], [Цена в рублях], [Единицы измерения]) VALUES (2, N'Нож', 9, 20.85, N'штук')
    INSERT INTO dbo.Товары([Номер товара], [Название товара], [Кол-во товара на складе], [Цена в рублях], [Единицы измерения]) VALUES (3, N'Тапки', 21, 10.5, N'штук')
    """
)
cur.commit()
cur.execute(
    """CREATE TABLE dbo.Заказы (
        [Дата продажи] DATE PRIMARY KEY,
        [Номер покупателя] INTEGER FOREIGN KEY REFERENCES dbo.Клиенты([Номер покупателя]),
        [Номер товара] INTEGER FOREIGN KEY REFERENCES dbo.Товары([Номер товара]),
        [Кол-во заказаного товара] INTEGER NOT NULL,
        Скидка REAL NOT NULL
    )
    INSERT INTO dbo.Заказы([Дата продажи], [Номер покупателя], [Номер товара], [Кол-во заказаного товара], Скидка) VALUES ('2020-12-08', 1, 1, 1, 0.02)
    INSERT INTO dbo.Заказы([Дата продажи], [Номер покупателя], [Номер товара], [Кол-во заказаного товара], Скидка) VALUES ('2021-09-21', 2, 2, 2, 0.2)
    INSERT INTO dbo.Заказы([Дата продажи], [Номер покупателя], [Номер товара], [Кол-во заказаного товара], Скидка) VALUES ('2022-05-11', 3, 3, 10, 0.1)
    """
)
cur.commit()
cur.execute(
    """CREATE TABLE dbo.Скидки (
        [Номер покупателя] INTEGER PRIMARY KEY REFERENCES dbo.Клиенты([Номер покупателя]),
        Скидка REAL NOT NULL
    )
    INSERT INTO dbo.Скидки([Номер покупателя], Скидка) VALUES (1, 0.02)
    INSERT INTO dbo.Скидки([Номер покупателя], Скидка) VALUES (2, 0.2)
    INSERT INTO dbo.Скидки([Номер покупателя], Скидка) VALUES (3, 0.1)
    """
)
cur.commit()
connect.close()
