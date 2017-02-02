DROP PROCEDURE [dbo].[sp_CalcGVSForPeriod]
GO

CREATE PROCEDURE [dbo].[sp_CalcGVSForPeriod](@period int)	
AS
BEGIN
	SET NOCOUNT ON;

	declare @gvsTab as Table(Id int, �����_�������� smallint, ����� nvarchar(max), diff decimal(18,0), ����� decimal(18,0), hid int)

	insert into @gvsTab
	select total.Id, total.�����_��������, total.�����, total.diff, serv.�����, total.hid from
		(select t1.*, t1.val-t2.val as diff from
		(select flat.Id, flat.�����_��������, h.�����, serv.Id as servId, h.Id as hid, sum(rdg.��������_���������_��) as val, @period as period
		from dbo.��������Set flat
			join dbo.���Set h on h.Id = flat.���_Id
			join dbo.�������Set cnt on flat.Id = cnt.��������_Id
			join dbo.������Set serv on serv.Id = cnt.������_Id
			join dbo.���������_�������Set rdg on rdg.�������_���_�������� = cnt.���_��������
				and ���������_Id = @period
		where serv.��� = 1
		group by flat.Id, flat.�����_��������, h.�����, serv.Id, h.Id) as t1
		join
		(select flat.Id, flat.�����_��������, h.�����, serv.Id as servId, h.Id as hid, sum(rdg.��������_���������_��) as val, @period-1 as period
		from dbo.��������Set flat
			join dbo.���Set h on h.Id = flat.���_Id
			join dbo.�������Set cnt on flat.Id = cnt.��������_Id
			join dbo.������Set serv on serv.Id = cnt.������_Id
			join dbo.���������_�������Set rdg on rdg.�������_���_�������� = cnt.���_��������
				and ���������_Id = @period-1
		where serv.��� = 1
		group by flat.Id, flat.�����_��������, h.�����, serv.Id, h.Id) as t2 on t1.Id = t2.Id and t1.servId = t2.servId
	) as total
	join dbo.������Set serv on total.servId = serv.Id
    
	--select * from @gvsTab

	insert into @gvsTab
	select emptyFlats.Id, emptyFlats.�����_��������, emptyFlats.�����, cast(incCnt*tot.koef as int) as diff,
		cast(incCnt*tot.koef as int)*(SELECT ����� FROM [dbo].[������Set] where ��� = 1) as val, emptyFlats.hid
	from
	(select fl.Id, fl.�����_��������, h.�����, fl.���_Id as hid, count(inh.Id) as incCnt
		from dbo.��������Set fl
		join dbo.���Set h on h.Id = fl.���_Id
		join dbo.�����Set inh on inh.��������_Id = fl.Id
		left join dbo.�������Set cnt on cnt.��������_Id = fl.Id
		where cnt.��������_Id is null
		group by fl.Id, fl.�����_��������, h.�����, fl.���_Id
	) as emptyFlats
	join
	(
		select t1.Id, (t1.���������_��� - t2.���������_��� - fVals.fvalDiff) / inh.inhCount as koef from 
		(	select h.Id, odu.���������_��� from dbo.���Set h
			join dbo.���������_���Set odu on odu.���_Id = h.Id
			where odu.���������_Id = @period
		) as t1
		join 
		(	select h.Id, odu.���������_��� from dbo.���Set h
			join dbo.���������_���Set odu on odu.���_Id = h.Id
			where odu.���������_Id = @period - 1
		) as t2 on t1.Id = t2.Id
		join
		(
			select fgvs.hid, sum(fgvs.diff) as fvalDiff
			from @gvsTab fgvs
			group by fgvs.hid
		) as fVals on fVals.hid = t1.Id
		join
		(
			select count(inh.Id) inhCount, fl.���_Id
			from dbo.��������Set fl
			join dbo.�����Set inh on inh.��������_Id = fl.Id
			left join dbo.�������Set cnt on cnt.��������_Id = fl.Id
			where cnt.��������_Id is null
			group by fl.���_Id
		) as inh on inh.���_Id = t1.Id
	) as tot on tot.Id = emptyFlats.hid


	select *, val-IsNull(lgota,0) as total from
	(
		select gvs.*, diff*����� as val,
			case when diff < lg.��������_����� then �����*maxLgota*diff else �����*maxLgota*��������_����� end as lgota
		from @gvsTab gvs
		left join
		(select max(��������_������) maxLgota, k.Id, sc.��������_����� from dbo.������Set l
			join dbo.����������� zl on zl.������_Id = l.Id
			join dbo.������Set usl on usl.Id = l.������_Id
			join dbo.���_�����Set sc on sc.������_Id = usl.Id
			join dbo.�����Set z on z.Id = zl.�����_Id
			join dbo.��������Set k on k.Id = z.��������_Id
		where usl.��� = 1
		group by k.id, sc.��������_�����) as lg on lg.id = gvs.Id
	) as SuperTotal
END
GO


exec sp_CalcGVSForPeriod @period = 14