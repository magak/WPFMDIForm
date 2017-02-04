DROP PROCEDURE [dbo].[sp_CalcAntenaForPeriod]
GO

CREATE PROCEDURE [dbo].[sp_CalcAntenaForPeriod](@period int)	
AS
BEGIN
	SET NOCOUNT ON;

	declare @uslId int
	
	select @uslId = id from dbo.������Set serv where serv.Id = 8
	
	select 
		flat.Id, flat.�����_��������, hm.�����, cast(1 as decimal(18,0)) as diff, serv.����� as �����,
		hm.Id as hid, serv.����� as val, 
		serv.����� * lg.��������_������ as lgota,
		serv.����� - 
			IsNull(serv.����� * lg.��������_������, 0) as total
	from dbo.��������Set flat
	join dbo.���Set hm on hm.Id = flat.���_Id
	join dbo.������Set serv on serv.Id = @uslId
	left join dbo.������Set lg on lg.������_Id = @uslId
END
GO


exec sp_CalcAntenaForPeriod @period = 14