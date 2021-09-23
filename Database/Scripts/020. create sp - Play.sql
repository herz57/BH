create procedure Play
	@profileId int,
	@machineId int,
	@ticketCost int
as
	begin transaction
		declare @wonTicket table
		(
			TicketId int,
			MachineId int,
			Cost int,
			Win int,
			PlayedOut bit,
			Symbols varchar(max)
		);

		with allowed_tickets
		as
		(
			select * from dbo.Tickets 
			where MachineId = @machineId
				and Cost = @ticketCost
				and PlayedOut != 1
		)
		insert into @wonTicket
		select top (1) * from allowed_tickets where allowed_tickets.TicketId >= rand() * (select max(allowed_tickets.TicketId) from allowed_tickets)

		update dbo.Profiles
		set Balance = Balance - (select Cost from @wonTicket) + (select Win from @wonTicket)
		where ProfileId = @profileId

		update dbo.Tickets
		set PlayedOut = 1
		where TicketId = (select TicketId from @wonTicket)
	
		select Win, Symbols from @wonTicket
	commit;
go