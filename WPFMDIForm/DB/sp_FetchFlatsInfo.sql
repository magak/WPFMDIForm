DROP PROCEDURE [dbo].[sp_FetchFlatsInfo]
GO

CREATE PROCEDURE [dbo].[sp_FetchFlatsInfo](@period int)	
AS
BEGIN
	SET NOCOUNT ON;
	
	select
		fl.Id,
		dwl.���,
		hm.�����,
		cast(����� as int) as �����,
		cast(��� as int) as ���,
		right('0000000000'+cast(fl.�����_�������� as varchar(10)),10) as FLS,
		cast(fl.�����_�������� as varchar(10)) as �����_��������
	from dbo.��������Set fl
	join dbo.���Set hm on hm.Id = fl.���_Id
	join dbo.���������Set cal on cal.Id =@period
	left join 
		(select 
		���, 
		�����_��������, 
		��������_Id, 
		row_number() over(partition by ��������_Id order by ���) as num 
		from dbo.�����Set) as dwl on dwl.��������_Id = fl.Id	
	where dwl.num = 1
END
GO


exec sp_FetchFlatsInfo @period = 14