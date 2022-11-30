import os
import pathlib

os.system(
    f'start cmd.exe /C "npm start --prefix {pathlib.Path(__file__).parents[0]}\\front_end"'
)
os.system(
    f'start cmd.exe /C "python {pathlib.Path(__file__).parents[0]}\\back_end\\manage.py runserver"'
)
# npm start --prefix path/to/your/app
