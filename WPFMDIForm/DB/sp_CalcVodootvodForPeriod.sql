DROP PROCEDURE [dbo].[sp_CalcVodootvodForPeriod]
GO

CREATE PROCEDURE [dbo].[sp_CalcVodootvodForPeriod](@period int)	
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @uslId int
	
	select @uslId = id from dbo.������Set serv where serv.������������� = 1

	declare @hvs as table(Id int, �����_�������� smallint, ����� nvarchar(max), 
	diff decimal(18,0), ����� decimal(18,0), hid int, val decimal(18,0), lgota decimal(18,0), total decimal(18,0) )

	insert into @hvs
	exec [dbo].[sp_CalcHVSForPeriod] @period = @period
	
	declare @gvs as table(Id int, �����_�������� smallint, ����� nvarchar(max), 
	diff decimal(18,0), ����� decimal(18,0), hid int, val decimal(18,0), lgota decimal(18,0), total decimal(18,0) )

	insert into @gvs
	exec [dbo].[sp_CalcGVSForPeriod] @period = @period
	
	select *, val-lgota as total from
	(
		select t2.*, case when soc.Id is null then t2.diff*ISNULL(lg.��������_������,0)
			else
				case when t2.diff >= IsNUll(soc.��������_�����,0) then IsNUll(soc.��������_�����,0)*ISNULL(lg.��������_������,0)
				else t2.diff*ISNULL(lg.��������_������,0) end
			end as lgota
		from
		(
			select *, t1.�����*t1.diff as val from
			(
				select
					ISNULL(hvs.Id, gvs.Id) as id
					,ISNULL(hvs.�����_��������, gvs.�����_��������) as �����_��������
					,ISNULL(hvs.�����, gvs.�����) as �����
					,ISNULL(hvs.diff,0)+ISNULL(gvs.diff,0) as diff
					,serv.����� as �����
					,ISNULL(hvs.hid, gvs.hid) as hid
				from @hvs hvs
				full outer join @gvs gvs on gvs.id = hvs.id
				join dbo.������Set serv on serv.Id = @uslId
			) t1
		) t2
		left join dbo.���_�����Set soc on soc.������_Id = @uslId
		left join dbo.������Set lg on lg.������_Id = @uslId
	) t3
		
END
GO


exec sp_CalcVodootvodForPeriod @period = 14