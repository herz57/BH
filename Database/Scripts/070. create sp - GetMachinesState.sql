create proc GetMachinesState
as  
	declare @lockedMachinesCount int = (select count(*) from dbo.Machines m where m.LockedByUserId is not null);
	declare @availableMachinesCount int = (select count(*) from dbo.Machines m where m.LockedByUserId is null);
	select @lockedMachinesCount as LockedMachinesCount, @availableMachinesCount as AvailableMachinesCount;