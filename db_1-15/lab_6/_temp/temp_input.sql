
    begin tran
        insert ��������� values('���', '�����', 10234);
        begin tran
            update ������ set ������������_������ = '���' where �������� = '���';
            commit;
            if @@trancount > 0 rollback;
        select
            (select count(*) from ������ where �������� = '���') '������',
            (select count(*) from ��������� where ������������_����� = '���') '���������';
    