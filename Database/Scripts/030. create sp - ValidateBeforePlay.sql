create procedure ValidateBeforePlay
	@userId nvarchar(450),
	@machineId int,
	@ticketCost int
as
	if (not exists (select * from dbo.Machines m where m.MachineId = @machineId and m.LockedByUserId = @userId))
		throw 51000, 'Machine is not locked by current usere', 0;

	if (@ticketCost not in (5, 10, 15))
		throw 51000, 'Invalid ticket cost input', 0;

	if not exists (select * from Tickets t where t.MachineId = @machineId and t.Cost = @ticketCost and PlayedOut = 0)
		throw 51000, 'Tickets have been wasted for selected cost and machine', 0;

	if ((select p.Balance from Profiles p inner join dbo.AspNetUsers u on p.UserId = u.Id where u.Id = @userId) < @ticketCost)
		throw 51000, 'Not enough balance', 0;
go