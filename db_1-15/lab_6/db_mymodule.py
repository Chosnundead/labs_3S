import pyodbc
import os
from pathlib import Path


def wprint(text: str):
    print(f"\033[31m{text}\033[0m")
    pass


def iwprint(text: str):
    print(f"\033[1m\033[31m{text}\033[0m")
    pass


class Db:
    __CONNECTING_STRING = (
        "Driver={SQL Server};"
        "Server=DESKTOP-HQ83LCC;"
        "Database=master;"
        "Trusted_Connection=yes;"
    )

    def __init__(self, newDatabaseName=None) -> None:
        self._connect = pyodbc.connect(self.__CONNECTING_STRING)
        self._cur = self._connect.cursor()
        try:
            self._cur.execute("USE master")
            self._cur.commit()
            if newDatabaseName:
                try:
                    self._cur.execute(f"CREATE DATABASE {newDatabaseName}")
                    self._cur.commit()
                except:
                    wprint("Данная таблица существует, вхожу...")
                self._cur.execute(f"USE {newDatabaseName}")
                self._cur.commit()
                self._DATABASE_NAME = newDatabaseName
            else:
                self._cur.execute("USE S_MyBASE")
                self._cur.commit()
        except:
            raise ("Ошибка на этапе подключения!")
        print(f"\033[1m\033[31mПодключение прошло успешно!\033[0m")
        pass

    def fetch(self, number: int, string=None, description=None):
        print(f"\033[1m\nЗадание №{number}:\033[0m")
        if string != None and isinstance(string, str):
            self._cur.execute(string)
            try:
                for item in self._cur.fetchall():
                    print(item)
            except Exception as err:
                wprint("Ошибка при запросе в базу...")
                print("Ошибочный запрос: {0}".format(string.replace("\n", "")[:21]))
                print("Ошибка: {0}".format(err.args))
        if description != None and isinstance(description, str):
            print(f"{description}.")
        print()
        pass

    def _tryForTempFile(self, text: str):
        with open(f"{Path(__file__).parents[0]}\\_temp\\temp_input.sql", "w+") as f:
            f.write(text)
        pass

    def forceFetch(self, numberOfTask: int, query="", description=""):
        try:
            self._tryForTempFile("test")
        except:
            os.mkdir(f"{Path(__file__).parents[0]}\\_temp")
            wprint("Ошибка при получении папки _temp, создаю новую...")
            self._tryForTempFile("test")

        print(f"\033[1m\nЗадание №{numberOfTask}:\033[0m")
        if query != "" and isinstance(query, str):
            self._tryForTempFile(query)
            os.system(
                f"sqlcmd -d {self._DATABASE_NAME} -f 1251 -i {Path(__file__).parents[0]}\\_temp\\temp_input.sql -o {Path(__file__).parents[0]}\\_temp\\temp_output.txt"
            )
            with open(
                f"{Path(__file__).parents[0]}\\_temp\\temp_output.txt", "r+"
            ) as f:
                for item in f.readlines():
                    print(item)
            open(f"{Path(__file__).parents[0]}\\_temp\\temp_output.txt", "w+").close()
        if description != "" and isinstance(description, str):
            print(f"{description}.")
        print()
        pass

    def goFetch(self, fetch: str):
        try:
            self._cur.execute(fetch)
            self._cur.commit()
        except Exception as err:
            wprint("Ошибка при запросе в базу...")
            print("Ошибочный запрос: {0}".format(fetch.replace("\n", "")[:21]))
            print("Ошибка: {0}".format(err.args))
        pass

    def __del__(self) -> None:
        try:
            self._cur.close()
        except:
            raise Exception("Подключение не обнаружено!")
        print(f"\033[1m\033[31mПодключение успешно закрыто!\033[0m")
        pass
