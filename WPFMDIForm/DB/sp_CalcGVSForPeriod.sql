DROP PROCEDURE [dbo].[sp_CalcGVSForPeriod]
GO

CREATE PROCEDURE [dbo].[sp_CalcGVSForPeriod](@period int)	
AS
BEGIN
	SET NOCOUNT ON;

	declare @gvsTab as Table(Id int, Номер_квартиры smallint, Адрес nvarchar(max), diff decimal(18,0), Тариф decimal(18,0), hid int)

	insert into @gvsTab
	select total.Id, total.Номер_квартиры, total.Адрес, total.diff, serv.Тариф, total.hid from
		(select t1.*, t1.val-t2.val as diff from
		(select flat.Id, flat.Номер_квартиры, h.Адрес, serv.Id as servId, h.Id as hid, sum(rdg.Значение_показания_кв) as val, @period as period
		from dbo.КвартираSet flat
			join dbo.ДомSet h on h.Id = flat.Дом_Id
			join dbo.СчетчикSet cnt on flat.Id = cnt.Квартира_Id
			join dbo.УслугаSet serv on serv.Id = cnt.Услуга_Id
			join dbo.Показания_квартирSet rdg on rdg.Счетчик_Код_счетчика = cnt.Код_счетчика
				and Календарь_Id = @period
		where serv.ГВС = 1
		group by flat.Id, flat.Номер_квартиры, h.Адрес, serv.Id, h.Id) as t1
		join
		(select flat.Id, flat.Номер_квартиры, h.Адрес, serv.Id as servId, h.Id as hid, sum(rdg.Значение_показания_кв) as val, @period-1 as period
		from dbo.КвартираSet flat
			join dbo.ДомSet h on h.Id = flat.Дом_Id
			join dbo.СчетчикSet cnt on flat.Id = cnt.Квартира_Id
			join dbo.УслугаSet serv on serv.Id = cnt.Услуга_Id
			join dbo.Показания_квартирSet rdg on rdg.Счетчик_Код_счетчика = cnt.Код_счетчика
				and Календарь_Id = @period-1
		where serv.ГВС = 1
		group by flat.Id, flat.Номер_квартиры, h.Адрес, serv.Id, h.Id) as t2 on t1.Id = t2.Id and t1.servId = t2.servId
	) as total
	join dbo.УслугаSet serv on total.servId = serv.Id
    
	--select * from @gvsTab

	insert into @gvsTab
	select emptyFlats.Id, emptyFlats.Номер_квартиры, emptyFlats.Адрес, cast(incCnt*tot.koef as int) as diff,
		cast(incCnt*tot.koef as int)*(SELECT Тариф FROM [dbo].[УслугаSet] where ГВС = 1) as val, emptyFlats.hid
	from
	(select fl.Id, fl.Номер_квартиры, h.Адрес, fl.Дом_Id as hid, count(inh.Id) as incCnt
		from dbo.КвартираSet fl
		join dbo.ДомSet h on h.Id = fl.Дом_Id
		join dbo.ЖилецSet inh on inh.Квартира_Id = fl.Id
		left join dbo.СчетчикSet cnt on cnt.Квартира_Id = fl.Id
		where cnt.Квартира_Id is null
		group by fl.Id, fl.Номер_квартиры, h.Адрес, fl.Дом_Id
	) as emptyFlats
	join
	(
		select t1.Id, (t1.Показание_ГВС - t2.Показание_ГВС - fVals.fvalDiff) / inh.inhCount as koef from 
		(	select h.Id, odu.Показание_ГВС from dbo.ДомSet h
			join dbo.Показания_ОДУSet odu on odu.Дом_Id = h.Id
			where odu.Календарь_Id = @period
		) as t1
		join 
		(	select h.Id, odu.Показание_ГВС from dbo.ДомSet h
			join dbo.Показания_ОДУSet odu on odu.Дом_Id = h.Id
			where odu.Календарь_Id = @period - 1
		) as t2 on t1.Id = t2.Id
		join
		(
			select fgvs.hid, sum(fgvs.diff) as fvalDiff
			from @gvsTab fgvs
			group by fgvs.hid
		) as fVals on fVals.hid = t1.Id
		join
		(
			select count(inh.Id) inhCount, fl.Дом_Id
			from dbo.КвартираSet fl
			join dbo.ЖилецSet inh on inh.Квартира_Id = fl.Id
			left join dbo.СчетчикSet cnt on cnt.Квартира_Id = fl.Id
			where cnt.Квартира_Id is null
			group by fl.Дом_Id
		) as inh on inh.Дом_Id = t1.Id
	) as tot on tot.Id = emptyFlats.hid


	select *, val-IsNull(lgota,0) as total from
	(
		select gvs.*, diff*Тариф as val,
			case when diff < lg.Значение_нормы then Тариф*maxLgota*diff else Тариф*maxLgota*Значение_нормы end as lgota
		from @gvsTab gvs
		left join
		(select max(Значение_льготы) maxLgota, k.Id, sc.Значение_нормы from dbo.ЛьготаSet l
			join dbo.ЖилецЛьгота zl on zl.Льгота_Id = l.Id
			join dbo.УслугаSet usl on usl.Id = l.Услуга_Id
			join dbo.Соц_нормаSet sc on sc.Услуга_Id = usl.Id
			join dbo.ЖилецSet z on z.Id = zl.Жилец_Id
			join dbo.КвартираSet k on k.Id = z.Квартира_Id
		where usl.ГВС = 1
		group by k.id, sc.Значение_нормы) as lg on lg.id = gvs.Id
	) as SuperTotal
END
GO


exec sp_CalcGVSForPeriod @period = 14