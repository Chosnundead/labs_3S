import sys

sys.path.insert(1, "D:\Documents\Code\labs_3S\db_1-15\lab_6")

import db_mymodule

db = db_mymodule.Db("ПРОДАЖИ")

db.forceFetch(
    1,
    """
    select p.Наименование 'Наименование_товара', p.Цена 'Цена_товара',
        t.Цена_продажи 'Цена продажи' from Товары p join Заказы t
            on p.Наименование = t.Наименование_товара
            where t.Заказчик = 'Луч' for xml RAW('Заказчик'),
    root('Список_товаров'), elements;
    """,
)
db.forceFetch(
    2,
    """
    select [Заказчик].Заказчик [Заказчик],
        [Товар].Наименование [Наименование_товара],
        [Товар].Цена [Цена_товара]
        from Товары[Товар] join Заказы[Заказчик]
        on [Товар].Наименование = [Заказчик].Наименование_товара
            where [Заказчик].Заказчик in ('Луч', 'Белвест')
        order by [Заказчик] for xml AUTO,
        root('Список_товаров'), elements;
    GO
    select [Заказчик].Заказчик [Заказчик],
        [Товар].Наименование [Наименование_товара],
        [Товар].Цена [Цена_товара]
        from Товары[Товар] join Заказы[Заказчик]
        on [Товар].Наименование = [Заказчик].Наименование_товара
            where [Заказчик].Заказчик in ('Луч', 'Белвест')
        order by [Заказчик] for xml PATH('Заказчик'),
        root('Список_товаров'), elements;
    """,
)
db.forceFetch(
    3,
    """
     declare @h int = 0,
     @x varchar(2000) = ' <?xml version="1.0" encoding="windows-1251" ?>
     <товары>
     <товар="стол" цена="40" количество="5" />
     <товар="стул" цена="10" количество="3" />
     <товар="шкаф" цена="400" количество="1" />
     </товары>';
     exec sp_xml_preparedocument @h output, @x;
     select * from openxml(@h, '/товары/товар'
    , 0)
     with([товар] nvarchar(20), [цена] real, [количество] int )
     exec sp_xml_removedocument @h; 
    GO
    select * from openxml(@h, '/товары/товар'
    , 0)
     with([товар] nvarchar(20), [цена] real, [количество] int ) 
     GO
      insert Товары select [товар], [цена], [количество]
     from openxml(@h, '/товары/товар', 0)
     with([товар] nvarchar(20), [цена] real, [количество] int )
    """,
)
db.forceFetch(
    4,
    """
    create table Поставщики
    ( Организация nvarchar(50) primary key,
    Адрес xml
    );
    GO
    insert into Поставщики (Организация, Адрес)
     values ('Пинскдрев', '<адрес> <страна>Беларусь</страна>
     <город>Пинск</город> <улица>Кирова</улица>
     <дом>52</дом> </адрес>');
    insert into Поставщики (Организация, Адрес)
     values ('Минскдрев', '<адрес> <страна>Беларусь</страна>
     <город>Минск</город> <улица>Кальварийская</улица>
     <дом>35</дом> </адрес>');
     GO
     update Поставщики
     set Адрес = '<адрес> <страна>Беларусь</страна>
     <город>Минск</город> <улица>Кальварийская</улица>
     <дом>45</дом> </адрес>'
     where Адрес.value('(/адрес/дом)[1]','varchar(10)') = 35;
     GO
     select Организация,
     Адрес.value('(/адрес/страна)[1]','varchar(10)') [страна],
     Адрес.query('/адрес') [адрес]
     from Поставщики; 
    """,
)

del db

db = db_mymodule.Db("UNIVER")

db.forceFetch(
    5,
    """
    create xml schema collection Student as
    N'<?xml version="1.0" encoding="utf-16" ?>
    <xs:schema attributeFormDefault="unqualified"
     elementFormDefault="qualified"
     xmlns:xs="http://www.w3.org/2001/XMLSchema">
     <xs:element name="студент">
     <xs:complexType><xs:sequence>
     <xs:element name="паспорт" maxOccurs="1" minOccurs="1">
     <xs:complexType>
     <xs:attribute name="серия" type="xs:string" use="required" />
     <xs:attribute name="номер" type="xs:unsignedInt" use="required"/>
     <xs:attribute name="дата" use="required" >
     <xs:simpleType> <xs:restriction base ="xs:string">
     <xs:pattern value="[0-9]{2}.[0-9]{2}.[0-9]{4}"/>
     </xs:restriction> </xs:simpleType>
     </xs:attribute> </xs:complexType>
     </xs:element>
     <xs:element maxOccurs="3" name="телефон" type="xs:unsignedInt"/>
     <xs:element name="адрес"> <xs:complexType><xs:sequence>
     <xs:element name="страна" type="xs:string" />
     <xs:element name="город" type="xs:string" />
     <xs:element name="улица" type="xs:string" />
     <xs:element name="дом" type="xs:string" />
     <xs:element name="квартира" type="xs:string" />
     </xs:sequence></xs:complexType> </xs:element>
     </xs:sequence></xs:complexType>
     </xs:element>
    </xs:schema>';
    GO
    drop table STUDENT;
    go
    create table STUDENT
    ( IDSTUDENT integer identity(1000,1) primary key,
     IDGROUP integer foreign key references GROUPS(IDGROUP),
     NAME nvarchar(100),
     BDAY date,
     STAMP timestamp,
     INFO xml(STUDENT),
     FOTO varbinary
     );
    """,
)

del db
