
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
    