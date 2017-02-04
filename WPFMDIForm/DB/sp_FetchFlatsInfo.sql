DROP PROCEDURE [dbo].[sp_FetchFlatsInfo]
GO

CREATE PROCEDURE [dbo].[sp_FetchFlatsInfo](@period int)	
AS
BEGIN
	SET NOCOUNT ON;
	
	select
		fl.Id,
		dwl.ФИО,
		hm.Адрес,
		cast(Месяц as int) as Месяц,
		cast(Год as int) as Год,
		right('0000000000'+cast(fl.Номер_квартиры as varchar(10)),10) as FLS,
		cast(fl.Номер_квартиры as varchar(10)) as Номер_квартиры
	from dbo.КвартираSet fl
	join dbo.ДомSet hm on hm.Id = fl.Дом_Id
	join dbo.КалендарьSet cal on cal.Id =@period
	left join 
		(select 
		ФИО, 
		Номер_паспорта, 
		Квартира_Id, 
		row_number() over(partition by Квартира_Id order by ФИО) as num 
		from dbo.ЖилецSet) as dwl on dwl.Квартира_Id = fl.Id	
	where dwl.num = 1
END
GO


exec sp_FetchFlatsInfo @period = 14