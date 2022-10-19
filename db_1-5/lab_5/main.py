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


cur.close()
