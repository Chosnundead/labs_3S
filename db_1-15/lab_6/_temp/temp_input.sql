
    create function COUNT_Zakazy(@f varchar(20)) returns int
    as begin declare @rc int = 0;
    set @rc = (select count(�����_������)
        from ������ z join ��������� zk
            on z.�������� = zk. ������������_�����
                where ������������_����� = @f);
    return @rc
    end;
    GO
    DECLARE @f int = dbo.COUNT_Zakazy('���');
    print '���������� ������� == ' + cast(@f as varchar(4));
    GO
    Select ������������_�����, dbo.COUNT_Zakazy(������������_�����)
        from ���������;
    