create procedure ValidateBeforePlay
	@userId nvarchar(450),
	@machineId int,
	@ticketCost int
as
	if (not exists (select * from dbo.Machines m where m.MachineId = @machineId and m.LockedByUserId = @userId))
		raiserror('machine is not locked by current user', 16, 1);

	if (@ticketCost not in (5, 10, 15))
		raiserror('invalid ticket cost input', 16, 1);

	if not exists (select * from Tickets t where t.MachineId = @machineId and t.Cost = @ticketCost and PlayedOutDate is null)
		raiserror('tickets have been wasted for selected cost and machine', 16, 1);

	if ((select p.Balance from Profiles p inner join dbo.AspNetUsers u on p.UserId = u.Id where u.Id = @userId) < @ticketCost)
		raiserror('not enough balance', 16, 1);
go