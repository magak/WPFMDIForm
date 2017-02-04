DROP PROCEDURE [dbo].[sp_CalcSOIDForPeriod]
GO

CREATE PROCEDURE [dbo].[sp_CalcSOIDForPeriod](@period int)	
AS
BEGIN
	SET NOCOUNT ON;

	declare @uslId int
	
	select @uslId = id from dbo.������Set serv where serv.������_���� = 1
	
	select 
		flat.Id, flat.�����_��������, hm.�����, flat.�������_�������� as diff, serv.����� as �����,
		hm.Id as hid, flat.�������_�������� * serv.����� as val, 
		flat.�������_�������� * serv.����� * lg.��������_������ as lgota,
		flat.�������_�������� * serv.����� - 
			IsNull(flat.�������_�������� * serv.����� * lg.��������_������, 0) as total
	from dbo.��������Set flat
	join dbo.���Set hm on hm.Id = flat.���_Id
	join dbo.������Set serv on serv.Id = @uslId
	left join dbo.������Set lg on lg.������_Id = @uslId
END
GO


exec sp_CalcSOIDForPeriod @period = 14