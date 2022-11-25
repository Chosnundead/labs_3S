import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.forceFetch(
    1,
    """
    DECLARE @i int = 1,
        @b varchar(4) = 'БГТУ',
        @c datetime = getdate();
    SELECT @i i, @b b, @c c
    """,
)
db.forceFetch(
    1,
    """
    DECLARE @h TABLE
        (num int identity(1,1),
        fil varchar(30) default 'XXX'
        );
    INSERT @h default values;
    SELECT * from @h;
    """,
)
db.forceFetch(
    2,
    """
    DECLARE @d numeric(5,2) = 4.7,@a char(2), @f float(4) = 1;
    SET @a='РБ';SET @f = 11.4+@f;
    print 'd=' + cast(@d as varchar(10));
    print 'a=' + cast(@a as varchar(10));
    print 'f=' + cast(@f as varchar(10));
    """,
)
db.forceFetch(
    2,
    """
    DECLARE @k int = (select count(*) from Заказы)
    print 'Количество :' + cast(@k as varchar(10));
    """,
)
db.forceFetch(
    2,
    """
    DECLARE @y1 numeric(8,3)=(select cast(sum(Цена_продажи)
        as numeric(8,3)) from Заказы), @y2 real, @y3 numeric(8,3), @y4 real
    IF @y1>1000
    begin
    SELECT @y2=(select cast(count(*) as numeric(8,3)) from Заказы),
        @y3 = (select cast(AVG(Цена_продажи)
            as numeric(8,3)) from Заказы)
    SET @y4 = (select cast(COUNT(*) as numeric(8,3)) from Заказы
        where Цена_продажи > @y3)
    SELECT @y1 'Общая сумма', @y2 'Количество', @y3 'Средняя цена',
    @y4 'Количество товаров с ценой выше средней'
    end
    else IF @y1>500 print'Общая сумма от 500 до 1000'
    else IF @y1>100 print'Общая сумма от 100 до 500'
    else print'Общая сумма < 100'
    """,
)
db.forceFetch(
    3,
    """
    print 'Округление : '+ cast(round(12345.12345, 2) as varchar(12));
    print 'Нижнее целое : '+ cast(floor(24.5) as varchar(12));
    print 'Возведение в степень: '+ cast(power(12.0, 2) as varchar(12));
    print 'Логарифм : '+ cast(log(144.0) as varchar(12));
    print 'Корень квадратный : '+ cast(sqrt(144.0) as varchar(12));
    print 'Экпонента : '+ cast(exp(4.96981) as varchar(12));
    print 'Абсолютное значение : '+ cast(abs(-5) as varchar(12));
    print 'Cинус : '+ cast(sin(pi()) as varchar(12));
    print 'Подстрока : '+ substring('1234567890', 3,2);
    print 'Удалить пробелы справа : '+ rtrim('12345 ') +'X';
    print 'Удалить пробелы слева : '+ 'X'+ ltrim(' 67890');
    print 'Нижний регистр : '+ lower ('ВЕРХНИЙ РЕГИСТР');
    print 'Верхний регистр : '+ upper ('нижний регистр');
    print 'Заменить : '+ replace('1234512345', '5', 'X');
    print 'Строка пробелов : '+ 'X'+ space(5) +'X';
    print 'Повторить строку : '+ replicate('12', 5);
    print 'Найти по шаблону : '+ cast (patindex ('%Y_Y%', '123456YxY7890') as
    varchar(5));
    DECLARE @t time(7) = sysdatetime(), @dt datetime = getdate();
    print 'Текущее время : '+ convert (varchar(12), @t);
    print 'Текущая дата : '+ convert (varchar(12), @dt, 103);
    print '+1 день : '+ convert(varchar(12), dateadd(d, 1, @dt), 103);
    """,
)
db.forceFetch(
    4,
    """
    DECLARE @a int = 1, @b float = 0.3, @x float, @y float;
    SET @x = TAN(@a*@a+1);
    IF(3*@x<@a*@b) SET @y=7*@a+@x;
    else
    SET @y=cos(@a)
    PRINT 'y=' + cast(@y as varchar(10));
    """,
)
db.forceFetch(
    5,
    """
    DECLARE @x int = (select count(*) FROM Заказы);
    IF (select count(*) FROM Заказы) > 20
    begin
    PRINT'Количество товаров больше 20';
    PRINT'Количество = ' + cast(@x as varchar(10));
    end;
    Else
    begin
    PRINT'Количество товаров больше 20';
    PRINT'Количество = ' + cast(@x as varchar(10));
    end;
    """,
)
db.forceFetch(
    6,
    """
    SELECT CASE
        when Цена_продажи between 0 and 10 then 'дешево'
        when Цена_продажи between 10 and 100 then 'нормально'
        when Цена_продажи between 100 and 500 then 'дорого'
        else 'очень дорого'
        end Цена, count(*)[Количество]
    FROM Заказы
    GROUP BY CASE
        when Цена_продажи between 0 and 10 then 'дешево'
        when Цена_продажи between 10 and 100 then 'нормально'
        when Цена_продажи between 100 and 500 then 'дорого'
        else 'очень дорого'
        end
    """,
)
db.forceFetch(
    7,
    """
    CREATE table #EXPLRE
    ( TIND int,
     TFIELD varchar(100)
     );
    GO
    SET nocount on;
    DECLARE @i int = 0;
    WHILE @i<1000
        begin
    INSERT #EXPLRE(TIND, TFIELD)
        values(floor(30000*rand()), replicate('строка', 10));
    IF(@i % 100 = 0)
        print @i;
    SET @i = @i + 1;
    end;
    GO
    """,
)
db.forceFetch(
    8,
    """
    DECLATE @x int = 1
    print @x + 1
    print @x + 2
    RETURN
    print @x + 3
    """,
)
db.forceFetch(
    9,
    """
    begin TRY
        UPDATE Заказы set Номер_заказа = '5'
            where Номер_заказа = '6'
    end try
    begin CATCH
        print ERROR_NUMBER()
        print ERROR_MESSAGE()
        print ERROR_LINE()
        print ERROR_PROCEDURE()
        print ERROR_SEVERITY()
        print ERROR_STATE()
    end catch
    """,
)


del db
