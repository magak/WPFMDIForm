DROP PROCEDURE [dbo].[sp_CalcSOIDForPeriod]
GO

CREATE PROCEDURE [dbo].[sp_CalcSOIDForPeriod](@period int)	
AS
BEGIN
	SET NOCOUNT ON;

	declare @uslId int
	
	select @uslId = id from dbo.УслугаSet serv where serv.Содерж_дома = 1
	
	select 
		flat.Id, flat.Номер_квартиры, hm.Адрес, flat.Площадь_квартиры as diff, serv.Тариф as Тариф,
		hm.Id as hid, flat.Площадь_квартиры * serv.Тариф as val, 
		flat.Площадь_квартиры * serv.Тариф * lg.Значение_льготы as lgota,
		flat.Площадь_квартиры * serv.Тариф - 
			IsNull(flat.Площадь_квартиры * serv.Тариф * lg.Значение_льготы, 0) as total
	from dbo.КвартираSet flat
	join dbo.ДомSet hm on hm.Id = flat.Дом_Id
	join dbo.УслугаSet serv on serv.Id = @uslId
	left join dbo.ЛьготаSet lg on lg.Услуга_Id = @uslId
END
GO


exec sp_CalcSOIDForPeriod @period = 14