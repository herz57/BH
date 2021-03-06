USE [BH]
GO
/****** Object:  StoredProcedure [dbo].[Play]    Script Date: 10/2/2021 1:37:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Play]
	@userId nvarchar(450),
	@machineId int,
	@ticketCost int,
	@ticketId int out
as
	begin try;
		begin transaction Play
			exec dbo.ValidateBeforePlay @userId, @machineId, @ticketCost;

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
					and PlayedOut = 0
			)
			insert into @wonTicket
			select top (1) * from allowed_tickets where allowed_tickets.TicketId >= rand() * (select max(allowed_tickets.TicketId) from allowed_tickets)

			update p set Balance = Balance - (select Cost from @wonTicket) + (select Win from @wonTicket)
			from dbo.Profiles p
			inner join dbo.AspNetUsers u on u.Id = p.UserId
			where u.Id = @userId

			update dbo.Tickets set PlayedOut = 1
			where TicketId = (select TicketId from @wonTicket)
			set @ticketId = (select TicketId from @wonTicket)
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

