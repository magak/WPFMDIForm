DROP PROCEDURE [dbo].[sp_CalcDomofonForPeriod]
GO

CREATE PROCEDURE [dbo].[sp_CalcDomofonForPeriod](@period int)	
AS
BEGIN
	SET NOCOUNT ON;

	declare @uslId int
	
	select @uslId = id from dbo.УслугаSet serv where serv.Вид_услуги = 'Домофон'
	
	select 
		flat.Id, flat.Номер_квартиры, hm.Адрес, cast(1 as decimal(18,0)) as diff, serv.Тариф as Тариф,
		hm.Id as hid, serv.Тариф as val, 
		serv.Тариф * lg.Значение_льготы as lgota,
		serv.Тариф - 
			IsNull(serv.Тариф * lg.Значение_льготы, 0) as total
	from dbo.КвартираSet flat
	join dbo.ДомSet hm on hm.Id = flat.Дом_Id
	join dbo.УслугаSet serv on serv.Id = @uslId
	left join dbo.ЛьготаSet lg on lg.Услуга_Id = @uslId
END
GO


exec sp_CalcDomofonForPeriod @period = 14