USE [BH]
GO
/****** Object:  StoredProcedure [dbo].[Play]    Script Date: 9/26/2021 3:51:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Play]
	@profileId int,
	@machineId int,
	@ticketCost int
as
	begin try;
		begin transaction
			exec dbo.ValidateBeforePlay @profileId, @machineId, @ticketCost;

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

			update dbo.Profiles set Balance = Balance - (select Cost from @wonTicket) + (select Win from @wonTicket)
			where ProfileId = @profileId

			update dbo.Tickets set PlayedOut = 1
			where TicketId = (select TicketId from @wonTicket)
	
			select * from Tickets t where t.TicketId = (select TicketId from @wonTicket)
		commit transaction Play;
	end try
	begin catch;
		rollback
		declare 
			@ErrorMessage  nvarchar(max), 
			@ErrorSeverity int, 
			@ErrorState    int;

		select 
			@ErrorMessage = error_message(), 
			@ErrorSeverity = error_severity(), 
			@ErrorState = error_state();
		
		    raiserror(@ErrorMessage, @ErrorSeverity, @ErrorState);
	end catch;

