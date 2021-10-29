create proc UnlockMachine
(
	@userId nvarchar(450),
	@machineId int
)
as  
update m set LockedByUserId = null
    from dbo.Machines m
    inner join dbo.AspNetUsers u on u.Id = m.LockedByUserId
    where u.Id = @userId and m.MachineId = @machineId