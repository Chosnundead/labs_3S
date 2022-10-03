SELECT Дата_поставки
FROM     Заказы
WHERE  (Дата_поставки >= CONVERT(DATETIME, '2010-01-01 00:00:00', 102))