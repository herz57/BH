USE [BH]
GO
/****** Object:  StoredProcedure [dbo].[LockMachine]    Script Date: 10/2/2021 4:22:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[LockMachine]
	@userId nvarchar(450),
	@domainType int,
	@machineId int output
as 
	declare @selectedMachineId int;

	select top(1) @machineId = ag.MachineId from 
	(
		select t.Cost, m.MachineId from dbo.Machines m 
		inner join dbo.Tickets t on t.MachineId = m.MachineId
		where m.DomainType = @domainType and t.PlayedOut = 0 and m.LockedByUserId is null
		group by t.Cost, m.MachineId
	) ag
	group by ag.MachineId
	order by count(ag.MachineId) desc

	if (@machineId is null)
		raiserror('there are not available machines', 16, 1);

	update dbo.Machines set LockedByUserId = @userId where MachineId = @machineId