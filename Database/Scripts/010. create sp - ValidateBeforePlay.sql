create procedure ValidateBeforePlay
	@profileId int,
	@machineId int,
	@ticketCost int
as
	if ((select m.IsLocked from Machines m where m.MachineId = @machineId) = 1)
		raiserror('machine is locked', 16, 1);

	if (@ticketCost not in (5, 10, 15))
		raiserror('invalid ticket cost input', 16, 1);

	if not exists (select * from Tickets t where t.MachineId = @machineId and t.Cost = @ticketCost and PlayedOut != 1)
		raiserror('tickets have been wasted for selected cost and machine', 16, 1);

	if (select Balance from Profiles p where p.ProfileId = @profileId) < @ticketCost
		raiserror('not enough balance', 16, 1);
go