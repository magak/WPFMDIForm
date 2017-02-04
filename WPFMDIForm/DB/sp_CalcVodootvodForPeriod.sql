DROP PROCEDURE [dbo].[sp_CalcVodootvodForPeriod]
GO

CREATE PROCEDURE [dbo].[sp_CalcVodootvodForPeriod](@period int)	
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @uslId int
	
	select @uslId = id from dbo.УслугаSet serv where serv.Водоотведение = 1

	declare @hvs as table(Id int, Номер_квартиры smallint, Адрес nvarchar(max), 
	diff decimal(18,0), Тариф decimal(18,0), hid int, val decimal(18,0), lgota decimal(18,0), total decimal(18,0) )

	insert into @hvs
	exec [dbo].[sp_CalcHVSForPeriod] @period = @period
	
	declare @gvs as table(Id int, Номер_квартиры smallint, Адрес nvarchar(max), 
	diff decimal(18,0), Тариф decimal(18,0), hid int, val decimal(18,0), lgota decimal(18,0), total decimal(18,0) )

	insert into @gvs
	exec [dbo].[sp_CalcGVSForPeriod] @period = @period
	
	select *, val-lgota as total from
	(
		select t2.*, case when soc.Id is null then t2.diff*ISNULL(lg.Значение_льготы,0)
			else
				case when t2.diff >= IsNUll(soc.Значение_нормы,0) then IsNUll(soc.Значение_нормы,0)*ISNULL(lg.Значение_льготы,0)
				else t2.diff*ISNULL(lg.Значение_льготы,0) end
			end as lgota
		from
		(
			select *, t1.Тариф*t1.diff as val from
			(
				select
					ISNULL(hvs.Id, gvs.Id) as id
					,ISNULL(hvs.Номер_квартиры, gvs.Номер_квартиры) as Номер_квартиры
					,ISNULL(hvs.Адрес, gvs.Адрес) as Адрес
					,ISNULL(hvs.diff,0)+ISNULL(gvs.diff,0) as diff
					,serv.Тариф as Тариф
					,ISNULL(hvs.hid, gvs.hid) as hid
				from @hvs hvs
				full outer join @gvs gvs on gvs.id = hvs.id
				join dbo.УслугаSet serv on serv.Id = @uslId
			) t1
		) t2
		left join dbo.Соц_нормаSet soc on soc.Услуга_Id = @uslId
		left join dbo.ЛьготаSet lg on lg.Услуга_Id = @uslId
	) t3
		
END
GO


exec sp_CalcVodootvodForPeriod @period = 14